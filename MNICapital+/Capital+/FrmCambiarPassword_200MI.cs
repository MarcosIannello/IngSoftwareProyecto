using BE;
using BLL;
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
    public partial class FrmCambiarPassword_200MI : Form
    {
        
        private UsuariosBll200MI _usuarioDal200MI = new UsuariosBll200MI();
        private Usuario200MI usuario200MI = new();
        public FrmCambiarPassword_200MI()
        {
            InitializeComponent();

            usuario200MI = _usuarioDal200MI.getUserByUsername(SessionManager90DI.Instancia.UserName);

            if (usuario200MI != null)
            {
                cargarDatos(usuario200MI);
            }
            else
            {
                MessageBox.Show("Error al cargar el usuario. Por favor, intente nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var nuevaPassword = textBox2.Text;

            if (nuevaPassword == "")
            {
                MessageBox.Show("La contraseña no puede estar vacía.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var resultado = _usuarioDal200MI.updatePassword(usuario200MI.IdUsuario_200MI, nuevaPassword);

            if (resultado)
            {
                MessageBox.Show("Contraseña actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();

            }
            else
            {
                MessageBox.Show("Error al actualizar la contraseña. Por favor, intente nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 

        }

        private void cargarDatos(Usuario200MI usuario)
        {
            textBox1.Text = usuario.NombreUsuario_200MI;
            textBox1.Enabled = false;
        }
    }
}
