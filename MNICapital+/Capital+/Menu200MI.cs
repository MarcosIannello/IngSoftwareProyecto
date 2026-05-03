using Services_90DI;
using Services_90DI.Entidades;
using Services_90DI.SessionManager;
using Capital_.Estilos;
using System.Reflection;
using BLL;
using BE;

namespace Capital_
{
    public partial class Menu200MI : Form
    {
        public MenuBLL200MI menuBll = new MenuBLL200MI();

        public Menu200MI()
        {
            InitializeComponent();
            this.BackColor = Colores_200MI.Fondo;
            ConfigurarPanel();
            ConstruirMenu();
        }

        private void ConfigurarPanel()
        {
            panelMenu.Dock = DockStyle.None;
            panelMenu.Width = this.ClientSize.Width / 3;
            panelMenu.Height = this.ClientSize.Height;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panelMenu.BackColor = Color.FromArgb(240, 243, 249);
            panelMenu.AutoScroll = true;
            panelMenu.Padding = new Padding(20);
            panelMenu.BringToFront();
        }

        private void ConstruirMenu()
        {
            panelMenu.Controls.Clear();

            var menus = this.menuBll.GetMenu();
            int y = 25;

            foreach (var seccion in menus)
            {
                if (seccion.IdPadre_200MI == null)
                {
                    // Label de seccion
                    var lblSeccion = new Label
                    {
                        Text = seccion.Nombre_200MI.ToUpper(),
                        ForeColor = Color.FromArgb(100, 110, 140),
                        Font = new Font("Segoe UI", 9, FontStyle.Bold),
                        Location = new Point(20, y),
                        AutoSize = true
                    };
                    panelMenu.Controls.Add(lblSeccion);
                    y += 35;

                    // Botones hijos
                    AgregarHijosRecursivo(seccion, ref y, 0);

                    // Separador
                    var separador = new Panel
                    {
                        BackColor = Color.FromArgb(210, 215, 225),
                        Location = new Point(20, y + 5),
                        Size = new Size(panelMenu.Width - 60, 1)
                    };
                    panelMenu.Controls.Add(separador);
                    y += 30;
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
                MessageBox.Show($"No se encontro el formulario '{nombreForm}'", "Comuniquese con el Admin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var form = Activator.CreateInstance(tipo) as Form;
            form?.Show();
        }

        private void AgregarHijosRecursivo(Pantalla200MI padre, ref int y, int nivel)
        {
            foreach (var hijo in padre.Hijos)
            {
                int offsetX = 25 + (nivel * 20);

                if (hijo.Hijos.Count > 0)
                {
                    var lblSub = new Label
                    {
                        Text = hijo.Nombre_200MI,
                        ForeColor = Color.FromArgb(80, 90, 120),
                        Font = new Font("Segoe UI", 9, FontStyle.Bold),
                        Location = new Point(offsetX, y),
                        AutoSize = true
                    };
                    panelMenu.Controls.Add(lblSub);
                    y += 30;

                    AgregarHijosRecursivo(hijo, ref y, nivel + 1);
                }
                else
                {
                    int btnWidth = panelMenu.Width - offsetX - 40;

                    var btn = new Button
                    {
                        Text = "    " + hijo.Nombre_200MI,
                        Tag = hijo,
                        FlatStyle = FlatStyle.Flat,
                        BackColor = Color.White,
                        ForeColor = Color.FromArgb(50, 55, 70),
                        Font = new Font("Segoe UI", 10, FontStyle.Regular),
                        Size = new Size(btnWidth, 48),
                        Location = new Point(offsetX, y),
                        Cursor = Cursors.Hand,
                        TextAlign = ContentAlignment.MiddleLeft,
                    };

                    btn.FlatAppearance.BorderColor = Color.FromArgb(220, 225, 235);
                    btn.FlatAppearance.BorderSize = 1;
                    btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(230, 235, 245);
                    btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(210, 218, 235);

                    btn.Click += (sender, e) =>
                    {
                        var pantalla = (sender as Button)?.Tag as Pantalla200MI;
                        AbrirForm(pantalla?.NombreForm_200MI);
                    };

                    panelMenu.Controls.Add(btn);
                    y += 50;
                }
            }
        }

        private void Menu200MI_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
