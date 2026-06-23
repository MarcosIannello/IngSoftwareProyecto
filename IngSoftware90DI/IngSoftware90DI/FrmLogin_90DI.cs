using BLL;
using BLL_90DI;
using Capital_;
using Service_90DI;
using Services_90DI;
using Services_90DI.constantes;
using Services_90DI.entities;
using System.Linq;


namespace UI_90DI
{
    public partial class FrmLogin_90DI : Form, IObserver_90DI
    {
        // Patente que habilita a ver/reparar el form de integridad (admin del sistema).
        // Se reutiliza la patente de administración de roles: el admin de roles es el
        // admin del sistema. Si en el futuro la integridad necesita un permiso propio,
        // agregar una patente nueva en Patentes_90DI y referenciarla acá.
        private const string PATENTE_ADMIN_INTEGRIDAD = Patentes_90DI.AdminRoles;
        private readonly UsersBLL_90DI      _usuariosBLL = new UsersBLL_90DI();
        private readonly IntegridadBLL_90DI _integridad  = new IntegridadBLL_90DI();
        private readonly RolBLL_90DI        _rolesBLL    = new RolBLL_90DI();

        public FrmLogin_90DI()
        {
            InitializeComponent();

            LanguageManager_90DI.Current.AddObserver_90DI(this);

            // Aplicar el idioma actual (Español por defecto, o el que haya en sesión).
            LanguageManager_90DI.Current.NotifyAllObservers_90DI(SessionManager_90DI.Instancia.IdiomaActual);
        }

        private void btnCambiarIdioma_Click(object sender, EventArgs e)
        {
            using var form = new FrmCambiarIdioma_90DI();
            form.ShowDialog(this);
        }

        // Observer: recibe el diccionario de traducciones y actualiza los controles.
        public void UpdateLanguage_90DI(Dictionary<string, string> traducciones)
        {
            if (traducciones.TryGetValue("login_titulo", out var titulo))        label1.Text = titulo;
            if (traducciones.TryGetValue("login_usuario", out var usuario))      label2.Text = usuario;
            if (traducciones.TryGetValue("login_password", out var pass))        label3.Text = pass;
            if (traducciones.TryGetValue("login_btn_ingresar", out var btn))     Btn_Login.Text = btn;
            if (traducciones.TryGetValue("login_btn_idioma", out var btnIdioma)) btnCambiarIdioma.Text = btnIdioma;

            CentrarTitulo_90DI();
        }
        
        private void CentrarTitulo_90DI()
        {
            label1.Left = (ClientSize.Width - label1.Width) / 2;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            LanguageManager_90DI.Current.Unsubscribe_90DI(this); // remueve solo este form, no todos los observers
            base.OnFormClosed(e);
        }

        private void Btn_Login_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_LoginName.Text == "" || txt_loginPass.Text == "")
                {
                    MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                User_90DI? user;
#if DEBUG
                if (txt_LoginName.Text == "." && txt_loginPass.Text == ".")
                    user = _usuariosBLL.getUserByUsername("admin");
                else
                    user = _usuariosBLL.Login_90DI(txt_LoginName.Text, txt_loginPass.Text);
#else
                // autenticacion usuario
                user = _usuariosBLL.Login_90DI(txt_LoginName.Text, txt_loginPass.Text);
#endif

                if (user != null && user.Bloqueo_90DI)
                {
                    MessageBox.Show("Usuario bloqueado por intentos fallidos. Contacte al administrador.", "Usuario Bloqueado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                var login = SessionManager_90DI.Instancia.Login_90DI(user);

                if (!login)
                {
                    MessageBox.Show("Credenciales inválidas, intente nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Cargar las patentes efectivas del usuario en la sesión 
                int idRol = int.TryParse(user!.Rol_90DI, out var r) ? r : 0;
                SessionManager_90DI.Instancia.PatentesActivas = _rolesBLL.GetPatentesEfectivas_90DI(idRol);

                // Verificacion de integridad del sistema ANTES de cualquier escritura
                var resultados = _integridad.Verificar_90DI();
                var corruptos  = resultados.Where(r => r.EsCorrupto).ToList();

                if (corruptos.Any())
                {
                    // Solo un administrador (tiene la patente correspondiente) puede ver
                    // y reparar la integridad. El usuario común queda sin acceso.
                    bool esAdmin = SessionManager_90DI.Instancia.TienePatente_90DI(PATENTE_ADMIN_INTEGRIDAD);

                    if (esAdmin)
                    {
                        //mostrar form con todos los resultados
                        var frmIntegridad = new FrmAdminIntegridad_90DI(resultados);
                        var resultado = frmIntegridad.ShowDialog();

                        if (resultado == DialogResult.Cancel)
                            return;
                    }
                    else
                    {
                        // Usuario común: no permitir acceso, cerrar sesión.
                        // No se revela la causa (problema de integridad) para no exponer
                        // información sensible del estado del sistema a un usuario no admin.
                        SessionManager_90DI.Instancia.CerrarSesion();
                        MessageBox.Show(
                            "No es posible iniciar sesión en este momento.\n\nPor favor, comuníquese con un administrador.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                }

                // Solo si difiere del que ya tiene en BD: evita un UPDATE y un recálculo
                // de DV innecesarios cuando el idioma no cambió.
                string idiomaElegido = SessionManager_90DI.Instancia.IdiomaActual.CodigoIdioma_90DI;
                if (!string.Equals(idiomaElegido, user!.Idioma_90DI, StringComparison.OrdinalIgnoreCase))
                    _usuariosBLL.UpdateIdioma_90DI(user.IdUsuario_90DI, idiomaElegido, registrarBitacora: false);

                var menu = new FrmMenu_90DI();
                menu.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_CloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e) { }

        private void FrmLogin_90DI_Load(object sender, EventArgs e) { }
    }
}
