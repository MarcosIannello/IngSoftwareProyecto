using Infraestructure_DAL_.SessionManager;

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

            var message = $"Username: {txt_LoginName.Text}\nPassword: {txt_loginPass.Text}";

            var idUsuario = "1";

            var rol = "Admin";

            SessionManager200MI.Instancia.IniciarSesion(txt_LoginName.Text, idUsuario, rol);

            MessageBox.Show(message, "Login Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            var menu = new Menu200MI(txt_LoginName.Text, idUsuario, rol);

            menu.Show();

            this.Hide();

        }
    }
}
