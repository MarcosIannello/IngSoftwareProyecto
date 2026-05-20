using BLL_90DI;
using Capital_;
using Services_90DI;
using Service_90DI;


namespace UI_90DI
{
    public partial class FrmLogin_90DI : Form
    {
        private readonly BitacoraBLL_90DI _bitacora = new BitacoraBLL_90DI();
        private readonly UsersBLL_90DI _usuariosBLL = new UsersBLL_90DI();

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

                User_90DI? user = _usuariosBLL.Login_90DI(txt_LoginName.Text, txt_loginPass.Text);

                if(user != null && user.Bloqueo_90DI)
                {
                    MessageBox.Show("Usuario bloqueado por intentos fallidos. Contacte al administrador.", "Usuario Bloqueado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var login = SessionManager_90DI.Instancia.Login_90DI(user);

                if (login)
                {
                    _bitacora.CreateLogEvent_90DI(new Event_90DI
                    {
                        Login_90DI = txt_LoginName.Text,
                        Fecha_90DI = DateTime.Now,
                        Hora_90DI = DateTime.Now.TimeOfDay,
                        Modulo_90DI = "Login",
                        Evento_90DI = "Inicio de sesion exitoso",
                        Criticidad_90DI = 1
                    });

                    var menu = new FrmMenu_90DI();

                    menu.Show();

                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Credenciales invalidas intente nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void FrmLogin_90DI_Load(object sender, EventArgs e)
        {

        }
    }
}
