namespace Capital_
{
    public class FrmInputDialog_90DI : Form
    {
        private Label lblMensaje;
        private TextBox txtInput;
        private Button btnAceptar;
        private Button btnCancelar;

        public string Resultado => txtInput.Text.Trim();

        public FrmInputDialog_90DI(string titulo, string mensaje)
        {
            Text            = titulo;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition   = FormStartPosition.CenterParent;
            MinimizeBox     = false;
            MaximizeBox     = false;
            Size            = new Size(360, 160);

            lblMensaje = new Label     { Text = mensaje, Left = 12, Top = 12, Width = 320 };
            txtInput   = new TextBox   { Left = 12, Top = 36, Width = 320 };
            btnAceptar = new Button    { Text = "Aceptar",  Left = 172, Top = 72, Width = 80, DialogResult = DialogResult.OK };
            btnCancelar= new Button    { Text = "Cancelar", Left = 258, Top = 72, Width = 80, DialogResult = DialogResult.Cancel };

            AcceptButton = btnAceptar;
            CancelButton = btnCancelar;

            Controls.AddRange([lblMensaje, txtInput, btnAceptar, btnCancelar]);
        }
    }
}
