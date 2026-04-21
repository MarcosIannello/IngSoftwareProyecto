using Infraestructure_DAL_.SessionManager;

namespace Capital_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Btn_Login_Click(object sender, EventArgs e)
        {

            var message = $"Username: {txt_LoginName.Text}\nPassword: {txt_loginPass.Text}";

            var idUsuario = "1";

            var rol = "Admin";

            SessionManager.Instancia.IniciarSesion(txt_LoginName.Text, idUsuario, rol);

            MessageBox.Show(message, "Login Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            var menu = new Menu(txt_LoginName.Text, idUsuario, rol);

            menu.Show();

            this.Hide();

        }
    }
}
