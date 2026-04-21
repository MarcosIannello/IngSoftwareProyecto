using Infraestructure_DAL_.SessionManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Capital_
{
    public partial class Menu : Form
    {
        public string UserName;

        public string IdUsuario;

        public string Rol;

        public Menu(string username, string idusuario, string rol)
        {
            InitializeComponent();
            this.UserName = username;
            this.IdUsuario = idusuario;
            this.Rol = rol;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SessionManager.Instancia.IniciarSesion(this.UserName, this.IdUsuario, this.Rol);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar sesión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_LogOut_Click(object sender, EventArgs e)
        {
            SessionManager.Instancia.CerrarSesion();
            var LoginForm = new Form1();

            LoginForm.Show();

            this.Hide();
        }
    }
}
