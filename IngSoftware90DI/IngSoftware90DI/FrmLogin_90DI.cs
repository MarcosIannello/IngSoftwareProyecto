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
        // Patente que habilita a ver y reparar la integridad. Por ahora usamos la de
        // administración de roles, porque el que administra roles es el admin del sistema.
        // Si más adelante hace falta un permiso propio, se agrega en Patentes_90DI y se cambia acá.
        private const string PATENTE_ADMIN_INTEGRIDAD = Patentes_90DI.AdminRoles;
        private readonly UsersBLL_90DI      _usuariosBLL = new UsersBLL_90DI();
        private readonly IntegridadBLL_90DI _integridad  = new IntegridadBLL_90DI();
        private readonly RolBLL_90DI        _rolesBLL    = new RolBLL_90DI();

        public FrmLogin_90DI()
        {
            InitializeComponent();

            LanguageManager_90DI.Current_90DI.AddObserver_90DI(this);

            // Idioma actual (español por defecto, o el que ya esté en la sesión).
            LanguageManager_90DI.Current_90DI.NotifyAllObservers_90DI(SessionManager_90DI.Instancia_90DI.IdiomaActual_90DI);
        }

        // Observer: llegan las traducciones nuevas y actualizamos los textos.
        public void UpdateLanguage_90DI(Dictionary<string, string> traducciones)
        {
            if (traducciones.TryGetValue("login_titulo", out var titulo))    label1.Text    = titulo;
            if (traducciones.TryGetValue("login_usuario", out var usuario))  label2.Text    = usuario;
            if (traducciones.TryGetValue("login_password", out var pass))    label3.Text    = pass;
            if (traducciones.TryGetValue("login_btn_ingresar", out var btn)) Btn_Login.Text = btn;

            CentrarTitulo_90DI();
        }
        
        private void CentrarTitulo_90DI()
        {
            label1.Left = (ClientSize.Width - label1.Width) / 2;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            LanguageManager_90DI.Current_90DI.Unsubscribe_90DI(this); // solo este form, no todos los observers
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
                    user = _usuariosBLL.GetUserByUsername_90DI("admin");
                else
                    user = _usuariosBLL.Login_90DI(txt_LoginName.Text, txt_loginPass.Text);
#else
                // Autenticación
                user = _usuariosBLL.Login_90DI(txt_LoginName.Text, txt_loginPass.Text);
#endif

                if (user != null && user.Bloqueo_90DI)
                {
                    MessageBox.Show("Usuario bloqueado por intentos fallidos. Contacte al administrador.", "Usuario Bloqueado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                var login = SessionManager_90DI.Instancia_90DI.Login_90DI(user);

                if (!login)
                {
                    MessageBox.Show("Credenciales inválidas, intente nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Cargamos en la sesión las patentes efectivas del usuario
                int idRol = int.TryParse(user!.Rol_90DI, out var r) ? r : 0;
                SessionManager_90DI.Instancia_90DI.PatentesActivas_90DI = _rolesBLL.GetPatentesEfectivas_90DI(idRol);

                // Verificamos la integridad antes de escribir cualquier cosa en la base
                var resultados = _integridad.Verificar_90DI();
                var corruptos  = resultados.Where(r => r.EsCorrupto).ToList();

                if (corruptos.Any())
                {
                    // Solo el admin (el que tiene la patente) puede ver y reparar la integridad.
                    // Un usuario común no puede entrar.
                    bool esAdmin = SessionManager_90DI.Instancia_90DI.TienePatente_90DI(PATENTE_ADMIN_INTEGRIDAD);

                    if (esAdmin)
                    {
                        // Le mostramos el form con todos los resultados
                        var frmIntegridad = new FrmAdminIntegridad_90DI(resultados);
                        var resultado = frmIntegridad.ShowDialog();

                        if (resultado == DialogResult.Cancel)
                            return;
                    }
                    else
                    {
                        // Usuario común: cerramos la sesión y no lo dejamos entrar. No le decimos que
                        // es un problema de integridad, esa info del estado del sistema no tiene
                        // por qué verla alguien que no es admin.
                        SessionManager_90DI.Instancia_90DI.CerrarSesion_90DI();
                        MessageBox.Show(
                            "No es posible iniciar sesión en este momento.\n\nPor favor, comuníquese con un administrador.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                }

                // Aplicamos el idioma guardado del usuario antes de abrir el menú
                // (se avisa a todos los observers, este form incluido).
                if (!string.IsNullOrEmpty(user!.Idioma_90DI))
                {
                    var idiomaUsuario = new Idioma_90DI
                    {
                        CodigoIdioma_90DI = user.Idioma_90DI,
                        NombreIdioma_90DI = user.Idioma_90DI == "en" ? "English" : "Español"
                    };
                    LanguageManager_90DI.Current_90DI.NotifyAllObservers_90DI(idiomaUsuario);
                }

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

        // Limpia los campos para el próximo login (cuando vuelve del logout).
        public void ResetCampos_90DI()
        {
            txt_LoginName.Text = "";
            txt_loginPass.Text = "";
            txt_LoginName.Focus();
        }

        private void pictureBox1_Click(object sender, EventArgs e) { }

        private void FrmLogin_90DI_Load(object sender, EventArgs e) { }
    }
}
