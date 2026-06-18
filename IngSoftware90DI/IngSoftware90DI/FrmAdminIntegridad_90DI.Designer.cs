namespace Capital_
{
    partial class FrmAdminIntegridad_90DI
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
            btnBackup = new Button();
            btnForzarIntegridadAdmin = new Button();
            btnSalir = new Button();
            richTextBox1 = new RichTextBox();
            SuspendLayout();
            // 
            // btnBackup
            // 
            btnBackup.BackColor = Color.DodgerBlue;
            btnBackup.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBackup.ForeColor = SystemColors.ControlLightLight;
            btnBackup.Location = new Point(23, 366);
            btnBackup.Name = "btnBackup";
            btnBackup.Size = new Size(240, 46);
            btnBackup.TabIndex = 0;
            btnBackup.Text = "Recuperar Backup";
            btnBackup.UseVisualStyleBackColor = false;
            btnBackup.Click += BtnBackup_Click;
            // 
            // btnForzarIntegridadAdmin
            // 
            btnForzarIntegridadAdmin.BackColor = Color.LimeGreen;
            btnForzarIntegridadAdmin.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnForzarIntegridadAdmin.ForeColor = SystemColors.ControlLightLight;
            btnForzarIntegridadAdmin.Location = new Point(303, 366);
            btnForzarIntegridadAdmin.Name = "btnForzarIntegridadAdmin";
            btnForzarIntegridadAdmin.Size = new Size(240, 46);
            btnForzarIntegridadAdmin.TabIndex = 1;
            btnForzarIntegridadAdmin.Text = "Reparar Integridad";
            btnForzarIntegridadAdmin.UseVisualStyleBackColor = false;
            btnForzarIntegridadAdmin.Click += BtnForzarIntegridadAdmin_Click;
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.Red;
            btnSalir.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSalir.ForeColor = SystemColors.ControlLightLight;
            btnSalir.Location = new Point(586, 366);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(184, 46);
            btnSalir.TabIndex = 2;
            btnSalir.Text = "Cancelar";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += button2_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(23, 45);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(747, 274);
            richTextBox1.TabIndex = 2;
            richTextBox1.Text = "";
            // 
            // FrmAdminIntegridad_90DI
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(richTextBox1);
            Controls.Add(btnSalir);
            Controls.Add(btnForzarIntegridadAdmin);
            Controls.Add(btnBackup);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmAdminIntegridad_90DI";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmAdminIntegridad_90DI";
            Load += FrmAdminIntegridad_90DI_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnBackup;
        private Button btnForzarIntegridadAdmin;
        private Button btnSalir;
        private RichTextBox richTextBox1;
    }
}