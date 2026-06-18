using Services_90DI;
using BLL_90DI;
using Service_90DI;
using Capital_;

namespace UI_90DI
{
    public partial class FrmMenu_90DI : Form, IObserver_90DI
    {
        private readonly BitacoraBLL_90DI _bitacora = new BitacoraBLL_90DI();
        public UsersBLL_90DI _userBll = new UsersBLL_90DI();

        public FrmMenu_90DI()
        {
            InitializeComponent();
            LanguageManager_90DI.AddObserver_90DI(this);

            // Aplicar el idioma actual al abrir (por si ya se eligió uno en el Login).
            var traducciones = LanguageManager_90DI.TraduccionesActuales_90DI;
            if (traducciones.Count > 0)
                UpdateLanguage_90DI(traducciones);
        }

        public void UpdateLanguage_90DI(Dictionary<string, string> traducciones)
        {
            if (traducciones.TryGetValue("menu_usuarios",           out var v)) usuariosToolStripMenuItem.Text          = v;
            if (traducciones.TryGetValue("menu_login",              out v))     loginToolStripMenuItem.Text              = v;
            if (traducciones.TryGetValue("menu_logout",             out v))     logoutToolStripMenuItem.Text             = v;
            if (traducciones.TryGetValue("menu_password",           out v))     passwordToolStripMenuItem.Text           = v;
            if (traducciones.TryGetValue("menu_idioma",             out v))     idiomaToolStripMenuItem.Text             = v;
            if (traducciones.TryGetValue("menu_admin",              out v))     adminToolStripMenuItem.Text              = v;
            if (traducciones.TryGetValue("menu_gestion_usuarios",   out v))     gestionUsuariosToolStripMenuItem.Text    = v;
            if (traducciones.TryGetValue("menu_abm_usuarios",       out v))     aBMUsuariosToolStripMenuItem.Text        = v;
            if (traducciones.TryGetValue("menu_bitacora",           out v))     bitacoraToolStripMenuItem.Text           = v;
            if (traducciones.TryGetValue("menu_admin_familia",      out v))     adminFamiliaToolStripMenuItem.Text       = v;
            if (traducciones.TryGetValue("menu_admin_roles",        out v))     adminRolesToolStripMenuItem.Text         = v;
            if (traducciones.TryGetValue("menu_maestro",            out v))     maestroToolStripMenuItem.Text            = v;
            if (traducciones.TryGetValue("menu_clientes",           out v))     clientesToolStripMenuItem.Text           = v;
            if (traducciones.TryGetValue("menu_prestamos",          out v))     prestamosToolStripMenuItem.Text          = v;
            if (traducciones.TryGetValue("menu_medicos",            out v))     medicosToolStripMenuItem.Text            = v;
            if (traducciones.TryGetValue("menu_pacientes",          out v))     pacientesToolStripMenuItem.Text          = v;
            if (traducciones.TryGetValue("menu_reportes",           out v))     reportesToolStripMenuItem.Text           = v;
            if (traducciones.TryGetValue("menu_historial_cliente",  out v))     historialClienteToolStripMenuItem.Text   = v;
            if (traducciones.TryGetValue("menu_simulacion_prestamo",out v))     simulacionPrestamoToolStripMenuItem.Text = v;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            LanguageManager_90DI.Unsubscribe_90DI(this);
            base.OnFormClosed(e);
        }


        private void Menu200MI_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var frmLogin = new FrmLogin_90DI();

                this.Show();
                frmLogin.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var confirm = MessageBox.Show(
                    LanguageManager_90DI.T("menu_msg_logout_text"),
                    LanguageManager_90DI.T("menu_msg_logout_title"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes) return;

                _userBll.Logout_90DI();
                var login = new FrmLogin_90DI();
                login.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void passwordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FrmCambiarPassword_90DI();
            form.Show();
            this.Hide();
        }

        private void idiomaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var form = new FrmCambiarIdioma_90DI();
            form.ShowDialog(this);
        }

        private void aBMUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var abmUsers = new FrmGestionUsuarios(this);
            this.Hide();
            abmUsers.Show();
        }

        private void bitacoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var Bitacora = new FrmBitacora_90DI(this);
            this.Hide();
            Bitacora.Show();
        }

        // Instancias reutilizables: evitan crear un form nuevo cada vez que el usuario
        // abre el mismo módulo desde el menú
        private FrmAdminFamilias? _frmFamilias;
        private FrmAdminROLES_90DI? _frmRoles;

        // Si ambos forms están abiertos y visibles al mismo tiempo, los posiciona
        // automáticamente: Familias a la izquierda y Roles a la derecha,
        // cada uno ocupando la mitad del área de trabajo (respeta la barra de tareas)
        private void TileFormsHalfScreen()
        {
            var screen = Screen.PrimaryScreen!.WorkingArea;
            int halfW = screen.Width / 2;

            // Solo aplica el tile si los dos forms están activos simultáneamente
            if (_frmFamilias != null && !_frmFamilias.IsDisposed && _frmFamilias.Visible &&
                _frmRoles   != null && !_frmRoles.IsDisposed   && _frmRoles.Visible)
            {
                // Forzar Normal para que Bounds tenga efecto (Maximized lo ignora)
                _frmFamilias.WindowState = FormWindowState.Normal;
                _frmFamilias.StartPosition = FormStartPosition.Manual;
                _frmFamilias.Bounds = new Rectangle(screen.Left, screen.Top, halfW, screen.Height);

                _frmRoles.WindowState = FormWindowState.Normal;
                _frmRoles.StartPosition = FormStartPosition.Manual;
                _frmRoles.Bounds = new Rectangle(screen.Left + halfW, screen.Top, halfW, screen.Height);
            }
        }

        // Abre el ABM de Familias. Si ya fue abierto antes y no fue cerrado,
        // reutiliza la instancia existente en lugar de crear una nueva
        private void adminFamiliaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_frmFamilias == null || _frmFamilias.IsDisposed)
                _frmFamilias = new FrmAdminFamilias();

            _frmFamilias.Show();
            _frmFamilias.BringToFront();
            TileFormsHalfScreen();
        }

        // Abre el ABM de Roles. Si ya fue abierto antes y no fue cerrado,
        // reutiliza la instancia existente en lugar de crear una nueva
        private void adminRolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_frmRoles == null || _frmRoles.IsDisposed)
                _frmRoles = new FrmAdminROLES_90DI();

            _frmRoles.Show();
            _frmRoles.BringToFront();
            TileFormsHalfScreen();
        }
    }
}
