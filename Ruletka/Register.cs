using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ruletka
{
    public partial class Register : Form
    {
        private string name;
        private string password;
        private string repeatPassword;
        DbHandler dbHandler;

        public Register()
        {
            InitializeComponent();

            dbHandler = new DbHandler();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            nameError.Text = "";
            PasswordError.Text = "";
            RepeatPasswordError.Text = "";

        }

        private bool rejestracja()
        {
            bool isValid = true;

            // Nazwa użytkownika
            if (string.IsNullOrWhiteSpace(UserName.Text))
            {
                nameError.Text = "Nazwa użytkownika nie może być pusta";
                isValid = false;
            }
            else
            {
                nameError.Text = "";
                name = UserName.Text;
            }

            // Hasło
            if (string.IsNullOrWhiteSpace(Pass.Text))
            {
                PasswordError.Text = "Hasło nie może być puste";
                isValid = false;
            }
            else if (!silaHasla())
            {
                PasswordError.Text = "Hasło musi zawierać co najmniej 8 znaków,\n w tym wielkie litery, małe litery,\n cyfry i znaki specjalne.";
                isValid = false;
            }
            else
            {
                PasswordError.Text = "";
                password = Pass.Text;
            }

            // Powtórz hasło
            if (string.IsNullOrWhiteSpace(RepeatPass.Text))
            {
                RepeatPasswordError.Text = "Powtórz hasło";
                isValid = false;
            }
            else if (Pass.Text != RepeatPass.Text)
            {
                RepeatPasswordError.Text = "Hasła nie są takie same";
                isValid = false;
            }
            else
            {
                RepeatPasswordError.Text = "";
                repeatPassword = RepeatPass.Text;
            }

            return isValid;
        }

        private void wyslanieDoBazy()
        {
            if (rejestracja())
            {
                
                dbHandler.AddUser(name, password);

                MessageBox.Show("Rejestracja przebiegła pomyślnie");

                Login login = new Login();
                this.Hide();
                login.ShowDialog();
                this.Close();
            }
        }

        private bool silaHasla()
        {
            string pass = Pass.Text;

            if (string.IsNullOrWhiteSpace(pass))
            {
                return false;
            }

            bool hasUpperCase = pass.Any(char.IsUpper);
            bool hasLowerCase = pass.Any(char.IsLower);
            bool hasDigit = pass.Any(char.IsDigit);
            bool hasSpecialChar = pass.Any(ch => !char.IsLetterOrDigit(ch));
            bool isValidLength = pass.Length >= 8;

            return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar && isValidLength;
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            wyslanieDoBazy();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.ShowDialog();
            this.Close();
        }
    }
}
