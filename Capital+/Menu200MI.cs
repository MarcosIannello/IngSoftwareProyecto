using Services.SessionManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Capital_
{
    public partial class Menu200MI : Form
    {
        public string Username { get; private set; }
        public string Id { get; private set; }

        public string Rol { get; private set; }

        public Menu200MI(string username, string id, string rol)
        {
            InitializeComponent();
            this.Username = username;
            this.Id = id;
            this.Rol = rol;
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SessionManager200MI.Instancia.IniciarSesion(this.Username, this.Id, this.Rol);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionManager200MI.Instancia.CerrarSesion();

            var login = new Login200MI();
    
            login.Show();
    
            this.Hide();
        }
    }
}
