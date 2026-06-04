using Services_90DI;
using BLL_90DI;
using Service_90DI;
using Capital_;

namespace UI_90DI
{
    public partial class FrmMenu_90DI : Form
    {
        private readonly BitacoraBLL_90DI _bitacora = new BitacoraBLL_90DI();
        public UsersBLL_90DI _userBll = new UsersBLL_90DI();

        public FrmMenu_90DI()
        {
            InitializeComponent();
        }


        private void Menu200MI_FormClosed(object sender, FormClosedEventArgs e)
        {
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
                    "¿Desea cerrar sesión?",
                    "Cerrar sesión",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes) return;

                _userBll.Logout_90DI();
                var login = new FrmLogin_90DI();
                login.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void passwordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FrmCambiarPassword_90DI();
            form.Show();
            this.Hide();
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

        private void adminFamiliaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmAdminFamilias = new FrmAdminFamilias();
            frmAdminFamilias.Show();
        }
    }
}
