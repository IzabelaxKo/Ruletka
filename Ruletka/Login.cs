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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }    
        private void wysylanieDoBazy()
        {
            Game game = new Game();
            this.Hide();
            game.ShowDialog();
            this.Close();
            // Izabelka tutaj tez trzebawysłac do bazy
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
