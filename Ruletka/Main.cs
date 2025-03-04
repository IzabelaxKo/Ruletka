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
        private DbHandler dbHandler;

        public Main()
        {
            InitializeComponent();

            dbHandler = new DbHandler();

            RoundedButton registerButton = new RoundedButton
            {
                Text = "Kliknij mnie",
                Size = new Size(150, 50),
                Location = new Point(380, 400),
                BackColor = Color.CornflowerBlue,
                ForeColor = Color.White,
            };
            registerButton.Click += (sender, e) => Rejestrowanie(); // Dodanie zdarzenia kliknięcia
            this.Controls.Add(registerButton);

            RoundedButton loginButton = new RoundedButton
            {
                Text = "Zaloguj",
                Size = new Size(150, 50),
                Location = new Point(380, 300),
                BackColor = Color.CornflowerBlue,
                ForeColor = Color.White,
                BorderRadius = 10,
            };
            loginButton.Click += (sender, e) => Logowanie(); // Dodanie zdarzenia kliknięcia
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
