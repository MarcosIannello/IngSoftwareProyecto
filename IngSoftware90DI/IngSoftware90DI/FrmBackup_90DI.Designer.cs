namespace UI_90DI
{
    partial class FrmBackup_90DI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblUltimoTitulo = new Label();
            lblUltimoFecha = new Label();
            lblInfo = new Label();
            btnGenerar = new Button();
            btnAplicar = new Button();
            btnCerrar = new Button();
            SuspendLayout();
            //
            // lblUltimoTitulo
            //
            lblUltimoTitulo.AutoSize = true;
            lblUltimoTitulo.Location = new Point(20, 20);
            lblUltimoTitulo.Name = "lblUltimoTitulo";
            lblUltimoTitulo.Size = new Size(140, 15);
            lblUltimoTitulo.TabIndex = 0;
            lblUltimoTitulo.Text = "Último backup realizado:";
            //
            // lblUltimoFecha
            //
            lblUltimoFecha.AutoSize = true;
            lblUltimoFecha.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUltimoFecha.Location = new Point(20, 42);
            lblUltimoFecha.Name = "lblUltimoFecha";
            lblUltimoFecha.Size = new Size(50, 19);
            lblUltimoFecha.TabIndex = 1;
            lblUltimoFecha.Text = "-";
            //
            // lblInfo
            //
            lblInfo.Location = new Point(20, 72);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(400, 40);
            lblInfo.TabIndex = 2;
            lblInfo.Text = "Si aplica el último backup, la base volverá al estado de ese momento.";
            //
            // btnGenerar
            //
            btnGenerar.BackColor = Color.GreenYellow;
            btnGenerar.Location = new Point(20, 125);
            btnGenerar.Name = "btnGenerar";
            btnGenerar.Size = new Size(190, 45);
            btnGenerar.TabIndex = 3;
            btnGenerar.Text = "Generar nuevo backup";
            btnGenerar.UseVisualStyleBackColor = false;
            btnGenerar.Click += BtnGenerar_Click;
            //
            // btnAplicar
            //
            btnAplicar.BackColor = Color.Gold;
            btnAplicar.Location = new Point(230, 125);
            btnAplicar.Name = "btnAplicar";
            btnAplicar.Size = new Size(190, 45);
            btnAplicar.TabIndex = 4;
            btnAplicar.Text = "Aplicar último backup";
            btnAplicar.UseVisualStyleBackColor = false;
            btnAplicar.Click += BtnAplicar_Click;
            //
            // btnCerrar
            //
            btnCerrar.BackColor = Color.LightGray;
            btnCerrar.Location = new Point(290, 185);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(130, 36);
            btnCerrar.TabIndex = 5;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = false;
            btnCerrar.Click += BtnCerrar_Click;
            //
            // FrmBackup_90DI
            //
            ClientSize = new Size(440, 240);
            Controls.Add(lblUltimoTitulo);
            Controls.Add(lblUltimoFecha);
            Controls.Add(lblInfo);
            Controls.Add(btnGenerar);
            Controls.Add(btnAplicar);
            Controls.Add(btnCerrar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmBackup_90DI";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Gestión de Backup";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblUltimoTitulo;
        private Label lblUltimoFecha;
        private Label lblInfo;
        private Button btnGenerar;
        private Button btnAplicar;
        private Button btnCerrar;
    }
}
