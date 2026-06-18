namespace UI_90DI
{
    partial class FrmCambiarIdioma_90DI
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
            lblSeleccionar = new Label();
            cmbIdioma = new ComboBox();
            btnAplicar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            //
            // lblSeleccionar
            //
            lblSeleccionar.AutoSize = true;
            lblSeleccionar.Location = new Point(20, 20);
            lblSeleccionar.Name = "lblSeleccionar";
            lblSeleccionar.TabIndex = 0;
            lblSeleccionar.Text = "Seleccionar Idioma";
            //
            // cmbIdioma
            //
            cmbIdioma.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbIdioma.Location = new Point(20, 50);
            cmbIdioma.Name = "cmbIdioma";
            cmbIdioma.Size = new Size(275, 30);
            cmbIdioma.TabIndex = 1;
            //
            // btnAplicar
            //
            btnAplicar.BackColor = Color.GreenYellow;
            btnAplicar.Location = new Point(20, 100);
            btnAplicar.Name = "btnAplicar";
            btnAplicar.Size = new Size(130, 36);
            btnAplicar.TabIndex = 2;
            btnAplicar.Text = "Aplicar";
            btnAplicar.UseVisualStyleBackColor = false;
            btnAplicar.Click += BtnAplicar_Click;
            //
            // btnCancelar
            //
            btnCancelar.BackColor = Color.LightCoral;
            btnCancelar.Location = new Point(165, 100);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(130, 36);
            btnCancelar.TabIndex = 3;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += BtnCancelar_Click;
            //
            // FrmCambiarIdioma_90DI
            //
            ClientSize = new Size(320, 160);
            Controls.Add(lblSeleccionar);
            Controls.Add(cmbIdioma);
            Controls.Add(btnAplicar);
            Controls.Add(btnCancelar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmCambiarIdioma_90DI";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Cambiar Idioma";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblSeleccionar;
        private ComboBox cmbIdioma;
        private Button btnAplicar;
        private Button btnCancelar;
    }
}
