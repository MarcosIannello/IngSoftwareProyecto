using BLL;
using Services.SessionManager;

namespace Capital_
{
    public partial class Login200MI : Form
    {

        public Login200MI()
        {
            InitializeComponent();
        }

        private void Btn_Login_Click(object sender, EventArgs e)
        {
            try
            {

                SessionManager90DI.Instancia.IniciarSesion(txt_LoginName.Text, txt_loginPass.Text);

                var menu = new Menu200MI();

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
    }
}
