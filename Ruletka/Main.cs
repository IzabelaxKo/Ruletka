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
        }


        private void roundedButton1_Click(object sender, EventArgs e)
        {
            // rejestracja

            Register register = new Register();
            this.Hide();
            register.ShowDialog();
            this.Close();
        }

        private void roundedButton2_Click(object sender, EventArgs e)
        {
            // logowanie

            Login loginPage = new Login();
            this.Hide();
            loginPage.ShowDialog();
            this.Close();
        }


    }
}
