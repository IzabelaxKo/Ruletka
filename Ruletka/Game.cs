using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ruletka
{
    public partial class Game : Form
    {
        private string[] images = { "bar", "bell", "cherry", "lemon", "orange", "plum", "seven", "diamond" };
        private Random random = new Random();

        public Game()
        {
            InitializeComponent();
            setImages();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        // Ustawienie początkowych obrazów w PictureBox
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
                        pictureBox.Tag = images[randomIndex]; // Przypisz nazwę obrazu do Tag
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

        // Asynchroniczna metoda do wykonywania "spinu"
        private async Task spinAsync()
        {
            Image[] tempImagesFirstRow = new Image[3];
            string[] tempTagsFirstRow = new string[3];
            Image[] tempImagesSecondRow = new Image[3];
            string[] tempTagsSecondRow = new string[3];
            PictureBox pictureBox;
            int randomIndex;

            // Kopiowanie obrazów i tagów z pierwszego i drugiego wiersza
            for (int i = 1; i <= 3; i++)
            {
                // Pierwszy wiersz
                pictureBox = (PictureBox)this.Controls.Find("pictureBox" + i, true)[0];
                tempImagesFirstRow[i - 1] = pictureBox.Image;
                tempTagsFirstRow[i - 1] = pictureBox.Tag?.ToString();

                // Drugi wiersz
                pictureBox = (PictureBox)this.Controls.Find("pictureBox" + (i + 3), true)[0];
                tempImagesSecondRow[i - 1] = pictureBox.Image;
                tempTagsSecondRow[i - 1] = pictureBox.Tag?.ToString();
            }

            // Losowanie nowych obrazów dla pierwszego wiersza
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

            // Przesunięcie obrazów i tagów:
            for (int i = 4; i <= 6; i++)
            {
                // - Drugi wiersz otrzymuje stare obrazy i tagi z pierwszego wiersza

                pictureBox = (PictureBox)this.Controls.Find("pictureBox" + i, true)[0];
                int tempIndex = i - 4;
                pictureBox.Image = tempImagesFirstRow[tempIndex];
                pictureBox.Tag = tempTagsFirstRow[tempIndex]; // Przenieś tag
            }

            for (int i = 7; i <= 9; i++)
            {
                // - Trzeci wiersz otrzymuje stare obrazy i tagi z drugiego wiersza

                pictureBox = (PictureBox)this.Controls.Find("pictureBox" + i, true)[0];
                int tempIndex = i - 7;
                pictureBox.Image = tempImagesSecondRow[tempIndex];
                pictureBox.Tag = tempTagsSecondRow[tempIndex]; // Przenieś tag
            }
        }

        private void checkWin()
        {
            PictureBox pictureBox;
            string[] imageNames = new string[9];

            // Pobierz nazwy obrazów z PictureBox
            for (int i = 1; i <= 9; i++)
            {
                pictureBox = (PictureBox)this.Controls.Find("pictureBox" + i, true)[0];
                if (pictureBox.Tag != null)
                {
                    imageNames[i - 1] = pictureBox.Tag.ToString();
                }
            }

            // Sprawdź kombinacje wygrywające
            if (CheckRow(imageNames, 0, 1, 2)) // Pierwszy wiersz
            {
                PlayWinSound();
            }
            else if (CheckRow(imageNames, 3, 4, 5)) // Drugi wiersz
            {
                PlayWinSound();
            }
            else if (CheckRow(imageNames, 6, 7, 8)) // Trzeci wiersz
            {
                PlayWinSound();
            }
            else if (CheckRow(imageNames, 0, 4, 8)) // Przekątna (lewy górny do prawego dolnego)
            {
                PlayWinSound();
            }
            else if (CheckRow(imageNames, 2, 4, 6)) // Przekątna (prawy górny do lewego dolnego)
            {
                PlayWinSound();
            }
            else if (CheckRow(imageNames, 0, 3, 6)) // Lewa kolumna
            {
                PlayWinSound();
            }
            else if (CheckRow(imageNames, 1, 4, 7)) // Środkowa kolumna
            {
                PlayWinSound();
            }
            else if (CheckRow(imageNames, 2, 5, 8)) // Prawa kolumna
            {
                PlayWinSound();
            }
            else
            {
                // Przegrana
            }
        }

        private bool CheckRow(string[] imageNames, int index1, int index2, int index3)
        {
            return !string.IsNullOrEmpty(imageNames[index1]) && // Upewnij się, że nazwa nie jest pusta
                   imageNames[index1] == imageNames[index2] && // Porównaj pierwsze i drugie pole
                   imageNames[index2] == imageNames[index3];  // Porównaj drugie i trzecie pole
        }

        private void PlayWinSound()
        {
            string soundPath = Path.Combine("sounds", "win.wav"); // Ścieżka do pliku dźwiękowego

            if (File.Exists(soundPath))
            {
                try
                {
                    SoundPlayer player = new SoundPlayer(soundPath);
                    player.Play(); // Odtwórz dźwięk
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

        private async Task gameStartAsync()
        {
            SoundPlayer simpleSound = new SoundPlayer(Path.Combine("sounds", "tick.wav"));
            int spins = random.Next(6, 17);

            for (int i = 0; i < spins; i++)
            {
                await spinAsync();
                simpleSound.Play();
                await Task.Delay(200);
            }

            checkWin();
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
            //await gameStartAsync();
            setImages();
            checkWin();
        }
    }
}