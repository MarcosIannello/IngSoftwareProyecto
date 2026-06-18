using BLL_90DI;
using Capital_;
using Service_90DI;
using Services_90DI;
using Services_90DI.entities;


namespace UI_90DI
{
    public partial class FrmLogin_90DI : Form, IObserver_90DI
    {
        private readonly BitacoraBLL_90DI _bitacora = new BitacoraBLL_90DI();
        private readonly UsersBLL_90DI _usuariosBLL = new UsersBLL_90DI();

        public FrmLogin_90DI()
        {
            InitializeComponent();

            // Suscribir el form como observer de los cambios de idioma.
            LanguageManager_90DI.AddObserver_90DI(this);

            // Cargar idiomas disponibles y aplicar el inicial (Español).
            CargarIdiomas_90DI();
        }

        private void CargarIdiomas_90DI()
        {
            cmbIdioma.Items.Clear();
            cmbIdioma.Items.Add(new Idioma_90DI { IdIdioma_90DI = 1, NombreIdioma_90DI = "Español", CodigoIdioma_90DI = "es" });
            cmbIdioma.Items.Add(new Idioma_90DI { IdIdioma_90DI = 2, NombreIdioma_90DI = "English",  CodigoIdioma_90DI = "en" });
            cmbIdioma.SelectedIndex = 0; // dispara SelectedIndexChanged → aplica Español
        }

        // Paso 3 del caso de uso: el usuario selecciona un idioma del listado.
        private void cmbIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIdioma.SelectedItem is Idioma_90DI idioma)
                LanguageManager_90DI.NotifyAllObservers_90DI(idioma);
        }

        // Observer: recibe el diccionario de traducciones y actualiza los controles.
        public void UpdateLanguage_90DI(Dictionary<string, string> traducciones)
        {
            if (traducciones.TryGetValue("login_titulo", out var titulo))    label1.Text = titulo;
            if (traducciones.TryGetValue("login_usuario", out var usuario))  label2.Text = usuario;
            if (traducciones.TryGetValue("login_password", out var pass))    label3.Text = pass;
            if (traducciones.TryGetValue("login_btn_ingresar", out var btn)) Btn_Login.Text = btn;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // Evita memory leak: el LanguageManager (Singleton) mantiene la referencia.
            LanguageManager_90DI.Unsubscribe_90DI(this); // remueve solo este form, no todos los observers
            base.OnFormClosed(e);
        }

        private void Btn_Login_Click(object sender, EventArgs e)
        {
            txt_LoginName.Text = "admin";
            txt_loginPass.Text = "admin1234";

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
                    // Paso 4: persistir el idioma elegido en la fila del usuario autenticado.
                    _usuariosBLL.UpdateIdioma_90DI(user!.IdUsuario_90DI, SessionManager_90DI.Instancia.IdiomaActual.CodigoIdioma_90DI);

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
