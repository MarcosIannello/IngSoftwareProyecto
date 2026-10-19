using Services_90DI;
using Services_90DI.constantes;
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

            // Asocia cada opción del menú con su patente
            AsignarTagsMenu();

            // Muestra u oculta las opciones según las patentes del usuario.
            AplicarPermisos();

            LanguageManager_90DI.Current_90DI.AddObserver_90DI(this);

            // Idioma actual
            var traducciones = LanguageManager_90DI.Current_90DI.TraduccionesActuales_90DI;
            if (traducciones.Count > 0)
                UpdateLanguage_90DI(traducciones);
        }
        
        // El Tag tiene que ser igual al Nombre_90DI de la patente en la base. Solo llevan Tag
        // las opciones que dependen de un permiso; las que no tienen (Login, Logout, Idioma
        // y los placeholders de Maestro/Reportes) se muestran siempre.
        private void AsignarTagsMenu()
        {
            // Usuarios
            passwordToolStripMenuItem.Tag         = Patentes_90DI.CambiarPassword;

            // Admin
            aBMUsuariosToolStripMenuItem.Tag      = Patentes_90DI.AbmUsuarios;
            bitacoraToolStripMenuItem.Tag         = Patentes_90DI.Bitacora;
            adminFamiliaToolStripMenuItem.Tag     = Patentes_90DI.AdminFamilia;
            adminRolesToolStripMenuItem.Tag       = Patentes_90DI.AdminRoles;
            backupToolStripMenuItem.Tag           = Patentes_90DI.BackupBD;

            // Maestro
            clientesToolStripMenuItem.Tag         = Patentes_90DI.Clientes;
            prestamosToolStripMenuItem.Tag        = Patentes_90DI.Prestamos;
            medicosToolStripMenuItem.Tag          = Patentes_90DI.Medicos;
            pacientesToolStripMenuItem.Tag        = Patentes_90DI.Pacientes;

            // Reportes
            historialClienteToolStripMenuItem.Tag   = Patentes_90DI.HistorialCliente;
            simulacionPrestamoToolStripMenuItem.Tag = Patentes_90DI.SimulacionPrestamo;
        }

        // Muestra u oculta cada opción según las patentes del usuario logueado.
        private void AplicarPermisos()
        {
            foreach (ToolStripItem item in menuStrip1.Items)
                AplicarPermisosItem(item);
        }

        // Cómo se decide si un item se ve:
        // - Si tiene hijos, se ve si al menos uno de los hijos quedó visible.
        // - Si no tiene hijos y tiene Tag, se ve solo si el usuario tiene esa patente.
        // - Si no tiene hijos ni Tag, se ve siempre (Login, Logout, Idioma, placeholders).
        private bool AplicarPermisosItem(ToolStripItem item)
        {
            if (item is not ToolStripMenuItem mi)
                return false;

            if (mi.DropDownItems.Count > 0)
            {
                bool algunoVisible = false;
                foreach (ToolStripItem hijo in mi.DropDownItems)
                    algunoVisible |= AplicarPermisosItem(hijo);
                mi.Visible = algunoVisible;
                return algunoVisible;
            }

           
            bool visible = mi.Tag is not string patente
                           || SessionManager_90DI.Instancia_90DI.TienePatente_90DI(patente);
            mi.Visible = visible;
            return visible;
        }

        public void UpdateLanguage_90DI(Dictionary<string, string> traducciones)
        {
            if (traducciones.TryGetValue("menu_usuarios", out var v)) usuariosToolStripMenuItem.Text = v;
            if (traducciones.TryGetValue("menu_login", out v)) loginToolStripMenuItem.Text = v;
            if (traducciones.TryGetValue("menu_logout", out v)) logoutToolStripMenuItem.Text = v;
            if (traducciones.TryGetValue("menu_password", out v)) passwordToolStripMenuItem.Text = v;
            if (traducciones.TryGetValue("menu_idioma", out v)) idiomaToolStripMenuItem.Text = v;
            if (traducciones.TryGetValue("menu_admin", out v)) adminToolStripMenuItem.Text = v;
            if (traducciones.TryGetValue("menu_gestion_usuarios", out v)) gestionUsuariosToolStripMenuItem.Text = v;
            if (traducciones.TryGetValue("menu_abm_usuarios", out v)) aBMUsuariosToolStripMenuItem.Text = v;
            if (traducciones.TryGetValue("menu_bitacora", out v)) bitacoraToolStripMenuItem.Text = v;
            if (traducciones.TryGetValue("menu_admin_familia", out v)) adminFamiliaToolStripMenuItem.Text = v;
            if (traducciones.TryGetValue("menu_admin_roles", out v)) adminRolesToolStripMenuItem.Text = v;
            if (traducciones.TryGetValue("menu_backup", out v)) backupToolStripMenuItem.Text = v;
            if (traducciones.TryGetValue("menu_maestro", out v)) maestroToolStripMenuItem.Text = v;
            if (traducciones.TryGetValue("menu_clientes", out v)) clientesToolStripMenuItem.Text = v;
            if (traducciones.TryGetValue("menu_prestamos", out v)) prestamosToolStripMenuItem.Text = v;
            if (traducciones.TryGetValue("menu_medicos", out v)) medicosToolStripMenuItem.Text = v;
            if (traducciones.TryGetValue("menu_pacientes", out v)) pacientesToolStripMenuItem.Text = v;
            if (traducciones.TryGetValue("menu_reportes", out v)) reportesToolStripMenuItem.Text = v;
            if (traducciones.TryGetValue("menu_historial_cliente", out v)) historialClienteToolStripMenuItem.Text = v;
            if (traducciones.TryGetValue("menu_simulacion_prestamo", out v)) simulacionPrestamoToolStripMenuItem.Text = v;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            LanguageManager_90DI.Current_90DI.Unsubscribe_90DI(this);
            base.OnFormClosed(e);
        }


        private void FrmMenu_90DI_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Si es un logout, el menú se cierra a propósito y no hay que cerrar la app.
            if (SesionUI_90DI.CerrandoPorLogout) return;
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
                    LanguageManager_90DI.T_90DI("menu_msg_logout_text"),
                    LanguageManager_90DI.T_90DI("menu_msg_logout_title"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes) return;

                SesionUI_90DI.Logout_90DI(_userBll);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void passwordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var form = new FrmCambiarPassword_90DI();
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

        // Guardamos las instancias para no crear un form nuevo cada vez que se abre
        // el mismo módulo desde el menú.
        private FrmAdminFamilias? _frmFamilias;
        private FrmAdminROLES_90DI? _frmRoles;

        // Si Familias y Roles están abiertos a la vez, los acomoda uno al lado del otro:
        // Familias a la izquierda y Roles a la derecha, cada uno con la mitad de la
        // pantalla (sin tapar la barra de tareas).
        private void TileFormsHalfScreen()
        {
            var screen = Screen.PrimaryScreen!.WorkingArea;
            int halfW = screen.Width / 2;

            // Solo si están los dos abiertos
            if (_frmFamilias != null && !_frmFamilias.IsDisposed && _frmFamilias.Visible &&
                _frmRoles != null && !_frmRoles.IsDisposed && _frmRoles.Visible)
            {
                // Hay que pasarlo a Normal, si está maximizado ignora Bounds
                _frmFamilias.WindowState = FormWindowState.Normal;
                _frmFamilias.StartPosition = FormStartPosition.Manual;
                _frmFamilias.Bounds = new Rectangle(screen.Left, screen.Top, halfW, screen.Height);

                _frmRoles.WindowState = FormWindowState.Normal;
                _frmRoles.StartPosition = FormStartPosition.Manual;
                _frmRoles.Bounds = new Rectangle(screen.Left + halfW, screen.Top, halfW, screen.Height);
            }
        }

        // Abre el ABM de Familias. Si ya estaba abierto, reusa esa instancia.
        private void adminFamiliaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_frmFamilias == null || _frmFamilias.IsDisposed)
                _frmFamilias = new FrmAdminFamilias();

            _frmFamilias.Show();
            _frmFamilias.BringToFront();
            TileFormsHalfScreen();
        }

        // Abre el ABM de Roles. Si ya estaba abierto, reusa esa instancia.
        private void adminRolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_frmRoles == null || _frmRoles.IsDisposed)
                _frmRoles = new FrmAdminROLES_90DI();

            _frmRoles.Show();
            _frmRoles.BringToFront();
            TileFormsHalfScreen();
        }

        // Abre la pantalla de backup (hacer uno nuevo o restaurar el último). Solo la ve el admin.
        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmBackup_90DI())
            {
                frm.ShowDialog(this);
            }
        }

        private void FrmMenu_90DI_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void idiomaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            using var form = new FrmCambiarIdioma_90DI();
            form.ShowDialog(this);
        }
    }
}
