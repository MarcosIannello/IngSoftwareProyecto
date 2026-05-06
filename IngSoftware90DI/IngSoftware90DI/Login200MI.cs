using BLL;
using BLL_90DI;
using Capital_;

namespace UI_90DI
{
    public partial class Login_90DI : Form
    {

        public Login_90DI()
        {
            InitializeComponent();
        }

        private void Btn_Login_Click(object sender, EventArgs e)
        {
            try
            {

                var login = SessionManager90DI.Instancia.IniciarSesion(txt_LoginName.Text, txt_loginPass.Text);

                if (login)
                {
                    var menu = new Menu200MI();

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
