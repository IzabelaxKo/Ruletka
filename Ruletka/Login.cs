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

            RoundedButton registerButton = new RoundedButton
            {
                Text = "Zaloguj",
                Size = new Size(150, 50),
                Location = new Point(105, 120),
                BackColor = Color.CornflowerBlue,
                ForeColor = Color.White,
                BorderRadius = 20,
            };
            registerButton.Click += (sender, e) => wysylanieDoBazy();
            this.Controls.Add(registerButton);
        }    
        private void wysylanieDoBazy()
        {
            // Izabelka tutaj tez trzebawysłac do bazy
        }
    }


}
