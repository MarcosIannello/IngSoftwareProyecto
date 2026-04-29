using BE;
using BLL;
using Capital_.Estilos;
using Services.SessionManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Capital_
{
    public partial class Menu200MI : Form
    {
        public MenuBLL200MI menuBll = new MenuBLL200MI();
        public Menu200MI()
        {
            InitializeComponent();
            this.BackColor = Colores_200MI.Fondo;
            ConstruirMenu();
        }

        private void ConstruirMenu()
        {
            Menu.Items.Clear();
            Menu.BackColor = Colores_200MI.MenuFondo;
            Menu.ForeColor = Colores_200MI.MenuTextoPadre;

            var menus = this.menuBll.GetMenu();

            //Estilo


            foreach (var seccion in menus)
            {
                if (seccion.IdPadre_200MI == null)
                {
                    var menuPadre = new ToolStripMenuItem(seccion.Nombre_200MI);
                    menuPadre.Tag = seccion;
                    menuPadre.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                    menuPadre.Margin = new Padding(4, 0, 4, 0);


                    foreach (var hijo in seccion.Hijos)
                    {
                        var itemHijo = new ToolStripMenuItem(hijo.Nombre_200MI);
                        itemHijo.Tag = hijo;
                        //Estilos Hijo
                        itemHijo.BackColor = Colores_200MI.MenuFondoDropdown;
                        itemHijo.ForeColor = Colores_200MI.MenuTextoHijo;
                        itemHijo.Font = new Font("Segoe UI", 10, FontStyle.Regular);

                        itemHijo.Click += (sender, e) =>
                        {
                            var pantalla = (sender as ToolStripMenuItem).Tag as Pantalla200MI;
                            AbrirForm(pantalla?.NombreForm_200MI);
                        };

                        menuPadre.DropDownItems.Add(itemHijo);
                    }

                    Menu.Items.Add(menuPadre);

                }
            }
        }

        public void AbrirForm(string? nombreForm)
        {
            if (string.IsNullOrEmpty(nombreForm)) return;

            switch (nombreForm)
            {
                case "Login":
                    var sesionActiva = SessionManager90DI.Instancia.IniciarSesion();
                    if (!sesionActiva)
                    {
                        MessageBox.Show("Ya posee una sesion activa");
                    }
                    break;
                case "LogOut":
                    SessionManager90DI.Instancia.CerrarSesion();
                    var login = new Login200MI();
                    login.Show();
                    this.Hide();
                    break;
                default:
                    AbrirDinamicamente(nombreForm);
                    break;
            }
        }

        public void AbrirDinamicamente(string nombreForm)
        {
            var tipo = Assembly.GetExecutingAssembly()
                            .GetTypes()
                            .FirstOrDefault(t => t.Name == nombreForm);

            if (tipo == null)
            {
                MessageBox.Show($"No se encontró el formulario '{nombreForm}'", "Comuniquese con el Admin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var form = Activator.CreateInstance(tipo) as Form;
            form?.Show();
        }

        public void AplicarEstilosMenu()
        {
            Menu.BackColor = Colores_200MI.MenuFondo;
            Menu.ForeColor = Colores_200MI.MenuTextoPadre;
            Menu.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            Menu.Padding = new Padding(4);
        }

        private void Menu200MI_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
