using BLL_90DI;
using Capital_;
using Entities_90DI;
using Service_90DI;

namespace UI_90DI
{
    public partial class Login_90DI : Form
    {
        private readonly BitacoraBLL_90DI _bitacora = new BitacoraBLL_90DI();

        public Login_90DI()
        {
            InitializeComponent();
        }

        private void Btn_Login_Click(object sender, EventArgs e)
        {
            try
            {

                var login = SessionManager_90DI.Instancia.IniciarSesion(txt_LoginName.Text, txt_loginPass.Text);

                if (login)
                {
                    _bitacora.CreateLogEvent_90DI(new LogEvent_90DI
                    {
                        Login_90DI      = txt_LoginName.Text,
                        Fecha_90DI      = DateTime.Now,
                        Hora_90DI       = DateTime.Now.TimeOfDay,
                        Modulo_90DI     = "Login",
                        Evento_90DI     = "Inicio de sesion exitoso",
                        Criticidad_90DI = 1
                    });

                    var menu = new  Menu_90DI();

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
    }
}
