using BLL_90DI;
using Capital_;
using Service_90DI;
using Services_90DI.entities;
using System.Linq;


namespace UI_90DI
{
    public partial class FrmLogin_90DI : Form
    {
        // Perfil que corresponde a administrador en la tabla de usuarios Hardcodeado para test
        private const int PERFIL_ADMIN = 1;
        private readonly UsersBLL_90DI      _usuariosBLL = new UsersBLL_90DI();
        private readonly IntegridadBLL_90DI _integridad  = new IntegridadBLL_90DI();

        public FrmLogin_90DI()
        {
            InitializeComponent();
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

                // Verificar integridad del sistema
                var resultados = _integridad.Verificar_90DI();
                var corruptos  = resultados.Where(r => r.EsCorrupto).ToList();

                if (corruptos.Any())
                {
                    bool esAdmin = user!.IdPerfil_90DI == PERFIL_ADMIN;

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
                        // Usuario común: no permitir acceso, cerrar sesión
                        SessionManager_90DI.Instancia.CerrarSesion();
                        MessageBox.Show(
                            "Se detectó un problema de integridad en el sistema.\n\nPor favor comuníquese con un administrador.",
                            "Acceso Restringido",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
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

        private void pictureBox1_Click(object sender, EventArgs e) { }

        private void FrmLogin_90DI_Load(object sender, EventArgs e) { }
    }
}
