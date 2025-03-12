using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Linq;
using System.Data;

namespace Ruletka
{
    public partial class Game : Form
    {
        private DbHandler dbHandler = new DbHandler();
        private string[] images = { "bar", "bell", "cherry", "lemon", "plum", "seven", "diamond" };
        private Random random = new Random();
        private double bid;
        private int loggedInUser;
        private double balance;
        private bool isSpinning = false;
        private object[,] ostatnioZagrane = new object[8, 3];

        public Game(int loggedInUser)
        {
            InitializeComponent();
            setImages();
            setLeaderboard();
            getLastPlayed();

            bidError.Text = "";

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.loggedInUser = loggedInUser;
            balance = dbHandler.GetBalance(loggedInUser);
            label1.Text = "Saldo: " + balance.ToString("F2") + " PLN";
            label4.Text = dbHandler.GetUsername(loggedInUser);
            dbHandler.DisplayWinRatio(dbHandler.GetUsername(loggedInUser), winRatio);
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            this.Hide();
            main.ShowDialog();
            this.Close();
        }

        private async void spinBtn_Click(object sender, EventArgs e)
        {
            await gameStartAsync();
        }

        private void spin()
        {
            Image[] tempImagesFirstRow = new Image[3];
            string[] tempTagsFirstRow = new string[3];
            Image[] tempImagesSecondRow = new Image[3];
            string[] tempTagsSecondRow = new string[3];
            PictureBox pictureBox;
            int randomIndex;

            for (int i = 1; i <= 3; i++)
            {
                pictureBox = (PictureBox)this.Controls.Find("pictureBox" + i, true)[0];
                tempImagesFirstRow[i - 1] = pictureBox.Image;
                tempTagsFirstRow[i - 1] = pictureBox.Tag?.ToString();

                pictureBox = (PictureBox)this.Controls.Find("pictureBox" + (i + 3), true)[0];
                tempImagesSecondRow[i - 1] = pictureBox.Image;
                tempTagsSecondRow[i - 1] = pictureBox.Tag?.ToString();
            }

            for (int i = 1; i <= 3; i++)
            {
                randomIndex = random.Next(0, images.Length);
                string imagePath = Path.Combine("imgs", images[randomIndex] + ".png");

                if (File.Exists(imagePath))
                {
                    try
                    {
                        pictureBox = (PictureBox)this.Controls.Find("pictureBox" + i, true)[0];
                        pictureBox.Image = Image.FromFile(imagePath);
                        pictureBox.Tag = images[randomIndex];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Błąd ładowania obrazu: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Obraz nie istnieje: " + imagePath);
                }
            }

            for (int i = 4; i <= 6; i++)
            {
                pictureBox = (PictureBox)this.Controls.Find("pictureBox" + i, true)[0];
                int tempIndex = i - 4;
                pictureBox.Image = tempImagesFirstRow[tempIndex];
                pictureBox.Tag = tempTagsFirstRow[tempIndex];
            }

            for (int i = 7; i <= 9; i++)
            {
                pictureBox = (PictureBox)this.Controls.Find("pictureBox" + i, true)[0];
                int tempIndex = i - 7;
                pictureBox.Image = tempImagesSecondRow[tempIndex];
                pictureBox.Tag = tempTagsSecondRow[tempIndex];
            }
        }

        private void checkWin(double bid)
        {
            PictureBox pictureBox;
            string[] imageNames = new string[9];

            for (int i = 1; i <= 9; i++)
            {
                pictureBox = (PictureBox)this.Controls.Find("pictureBox" + i, true)[0];
                imageNames[i - 1] = pictureBox.Tag?.ToString();
            }

            double totalWin = CalculateTotalWin(imageNames, bid);

            if (totalWin > 0)
            {
                dbHandler.UpdateGames("wins", loggedInUser);
                PlayWinSound();
                balance += totalWin;
                dbHandler.UpdateBalance(loggedInUser, totalWin, '+');
                label1.Text = "Saldo: " + balance.ToString("F2") + " PLN";
            }
            else
            {
                dbHandler.UpdateGames("loses", loggedInUser);
            }
            updateLastPlayed(totalWin > 0, totalWin > 0 ? totalWin : -bid);
        }

        private double CalculateTotalWin(string[] imageNames, double bid)
        {
            var lines = new[]
            {
                new[] {0, 1, 2}, // Pierwszy wiersz
                new[] {3, 4, 5}, // Drugi wiersz
                new[] {6, 7, 8}, // Trzeci wiersz
                new[] {0, 3, 6}, // Lewa kolumna
                new[] {1, 4, 7}, // Środkowa kolumna
                new[] {2, 5, 8}, // Prawa kolumna
                new[] {0, 4, 8}, // Przekątna 1
                new[] {2, 4, 6}  // Przekątna 2
            };

            double totalWin = 0;

            foreach (var line in lines)
            {
                string symbol = CheckRowSymbol(imageNames, line[0], line[1], line[2]);
                if (symbol != null)
                {
                    totalWin += bid * GetMultiplier(symbol);
                }
            }

            return totalWin;
        }

        private string CheckRowSymbol(string[] imageNames, int i1, int i2, int i3)
        {
            if (string.IsNullOrEmpty(imageNames[i1])) return null;
            return (imageNames[i1] == imageNames[i2] && imageNames[i2] == imageNames[i3])
                   ? imageNames[i1]
                   : null;
        }

        private double GetMultiplier(string symbol)
        {
            switch (symbol.ToLower())
            {
                case "diamond": return 10.0;
                case "seven": return 5.0;
                case "bar": return 3.0;
                case "bell": return 4.0;
                case "plum": return 2.0;
                case "cherry": return 2.0;
                case "lemon": return 1.5;
                default: return 0.0;
            }
        }

        private async Task gameStartAsync()
        {
            if (isSpinning)
            {
                MessageBox.Show("Funkcja spin jest już wykonywana. Poczekaj na zakończenie.");
                return;
            }

            isSpinning = true;
            spinBtn.Enabled = false;

            try
            {
                bidError.Text = "";

                if (!double.TryParse(textBox1.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out bid))
                {
                    bidError.Text = "Nieprawidłowa stawka";
                    return;
                }

                if (bid <= 0)
                {
                    bidError.Text = "Stawka musi być większa niż 0";
                    return;
                }

                if (bid > balance)
                {
                    bidError.Text = "Nie masz wystarczających środków";
                    return;
                }

                SoundPlayer simpleSound = new SoundPlayer(Path.Combine("sounds", "tick.wav"));
                int spins = random.Next(6, 15);

                balance -= bid;
                dbHandler.UpdateBalance(loggedInUser, bid, '-');
                label1.Text = "Saldo: " + balance.ToString("F2") + " PLN";
                setLeaderboard();

                for (int i = 0; i < spins; i++)
                {
                    simpleSound.Play();
                    spin();
                    await Task.Delay(200);
                }

                checkWin(bid);
                setLeaderboard();
                dbHandler.DisplayWinRatio(dbHandler.GetUsername(loggedInUser), winRatio);
            }
            finally
            {
                isSpinning = false;
                spinBtn.Enabled = true;
            }
        }

        private void getLastPlayed()
        {
            for (int i = 0; i < 8; i++)
            {
                ostatnioZagrane[i, 0] = this.Controls.Find("ostatnia" + (i + 1), true).FirstOrDefault();
                ostatnioZagrane[i, 1] = false;
                ostatnioZagrane[i, 2] = 0.0;

                Label label = (Label)ostatnioZagrane[i, 0];
                if (label != null)
                {
                    label.Text = "";
                }
            }
        }

        private void updateLastPlayed(bool win, double amount)
        {
            for (int i = 7; i > 0; i--)
            {
                ostatnioZagrane[i, 1] = ostatnioZagrane[i - 1, 1];
                ostatnioZagrane[i, 2] = ostatnioZagrane[i - 1, 2];
            }

            ostatnioZagrane[0, 1] = win;
            ostatnioZagrane[0, 2] = amount;

            for (int i = 0; i < 8; i++)
            {
                Label label = (Label)ostatnioZagrane[i, 0];

                if (label != null)
                {
                    double value = (double)ostatnioZagrane[i, 2];

                    if (value == 0)
                    {
                        label.Text = "";
                    }
                    else
                    {
                        label.Text = (value > 0 ? "+" : "") + value.ToString("F2") + " PLN";
                        label.ForeColor = (bool)ostatnioZagrane[i, 1] ? Color.Green : Color.Red;
                    }
                }
            }
        }

        private void setLeaderboard()
        {
            DataTable table = dbHandler.BalanceScoreboard();

            for (int i = 0; i < 8; i++)
            {
                Label label = (Label)this.Controls.Find("najlepszy" + (i + 1), true).FirstOrDefault();

                if (label != null)
                {
                    if (i < table.Rows.Count)
                    {
                        DataRow row = table.Rows[i];
                        string name = row["nazwa"].ToString();
                        double balance = Convert.ToDouble(row["balans"]);

                        label.Text = $"{i + 1}. {name} - {balance.ToString("F2")} zł";
                    }
                    else
                    {
                        label.Text = "";
                    }
                }
            }
        }

        private void setImages()
        {
            for (int i = 1; i <= 9; i++)
            {
                PictureBox pictureBox = (PictureBox)this.Controls.Find("pictureBox" + i, true)[0];
                if (pictureBox == null)
                {
                    MessageBox.Show("Nie znaleziono PictureBox: image" + i);
                    continue;
                }

                int randomIndex = random.Next(0, images.Length);
                string imagePath = Path.Combine("imgs", images[randomIndex] + ".png");

                if (File.Exists(imagePath))
                {
                    try
                    {
                        pictureBox.Image = Image.FromFile(imagePath);
                        pictureBox.Tag = images[randomIndex];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Błąd ładowania obrazu: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Obraz nie istnieje: " + imagePath);
                }
            }
        }

        private void PlayWinSound()
        {
            string soundPath = Path.Combine("sounds", "win.wav");

            if (File.Exists(soundPath))
            {
                try
                {
                    SoundPlayer player = new SoundPlayer(soundPath);
                    player.Play();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd odtwarzania dźwięku: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Plik dźwiękowy nie istnieje: " + soundPath);
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Podaj stawkę")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Podaj stawkę";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.Split('.').Length > 1 &&
                (sender as TextBox).Text.Split('.')[1].Length >= 2)
            {
                e.Handled = true;
            }
            if (char.IsDigit(e.KeyChar) && (sender as TextBox).Text.Contains('.') &&
                (sender as TextBox).Text.Split('.')[1].Length >= 2)
            {
                e.Handled = true;
            }
        }
    }
}