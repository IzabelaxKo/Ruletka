using Microsoft.Win32;
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
    public partial class Main : Form
    {


        public Main()
        {
            InitializeComponent();

            RoundedButton registerButton = new RoundedButton
            {
                Text = "Zarejestruj",
                Size = new Size(150, 50),
                Location = new Point(17, 10),
                BackColor = Color.CornflowerBlue,
                ForeColor = Color.White,
                BorderRadius = 20,
            };
            registerButton.Click += (sender, e) => Rejestrowanie();
            this.Controls.Add(registerButton);

            RoundedButton loginButton = new RoundedButton
            {
                Text = "Zaloguj",
                Size = new Size(150, 50),
                Location = new Point(17, 70),
                BackColor = Color.CornflowerBlue,
                ForeColor = Color.White,
                BorderRadius = 20,
            };
            loginButton.Click += (sender, e) => Logowanie();
            this.Controls.Add(loginButton);
        }

        private void Logowanie()
        {
            Login loginPage = new Login();
            this.Hide();
            loginPage.ShowDialog();
            this.Close();
        }
        private void Rejestrowanie()
        {
            Register register = new Register();
            this.Hide();
            register.ShowDialog();
            this.Close();
        }
    }
}
