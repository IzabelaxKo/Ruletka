using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Ruletka
{
    public partial class Game : Form
    {
        private String[] images = { "bar", "bell", "cherry", "lemon", "orange", "plum", "seven", "diamond" };
        Random random = new Random();


        public Game()
        {
            InitializeComponent();
            setImages();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
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

                string imagePath = "imgs/" + images[randomIndex] + ".png";
                if (File.Exists(imagePath))
                {
                    try
                    {
                        pictureBox.Image = Image.FromFile(imagePath);
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

        private void spin() 
        {
            PictureBox pictureBox;
            for (int i = 0; i < 10; i++)
            {
                pictureBox = (PictureBox)this.Controls.Find("pictureBox" + i, true)[0];
            }

            for (int i = 0;i < 10; i++)
            {

            }

        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            this.Hide();
            main.ShowDialog();
            this.Close();
        }

        private void spinBtn_Click(object sender, EventArgs e)
        {
            spin();
        }
    }
}