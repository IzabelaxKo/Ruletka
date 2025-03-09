using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ruletka
{
    public partial class Login : Form
    {
        DbHandler dbHandler;
        private string username;
        private string password;


        public Login()
        {
            InitializeComponent();

            dbHandler = new DbHandler();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            nameError.Text = "";
        }
        private void wysylanieDoBazy()
        {
            username = UserName.Text;
            password = Pass.Text;

            int loggedInUser = dbHandler.TryToLoginUser(username, password);

            if (loggedInUser >= 0)
            {
                // tutaj musi być przekazenie id zalogowanego użytkownika gdzieś
                Game game = new Game(loggedInUser);
                this.Hide();
                game.ShowDialog();
                this.Close();
            }
            else
            {
                nameError.Text = "Niepoprawne dane logowania";
            }
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            wysylanieDoBazy();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            this.Hide();
            register.ShowDialog();
            this.Close();
        }
    }
}
