namespace UI_90DI
{
    partial class FrmCambiarPassword_90DI
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
            textBox1 = new TextBox();
            txtPassActual = new TextBox();
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtNewPass = new TextBox();
            label4 = new Label();
            txtConfirmNewPass = new TextBox();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(207, 45);
            textBox1.Margin = new Padding(2, 1, 2, 1);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(110, 23);
            textBox1.TabIndex = 0;
            // 
            // txtPassActual
            // 
            txtPassActual.Location = new Point(207, 83);
            txtPassActual.Margin = new Padding(2, 1, 2, 1);
            txtPassActual.Name = "txtPassActual";
            txtPassActual.Size = new Size(110, 23);
            txtPassActual.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(43, 183);
            button1.Margin = new Padding(2, 1, 2, 1);
            button1.Name = "button1";
            button1.Size = new Size(344, 22);
            button1.TabIndex = 2;
            button1.Text = "Cambiar Contrasenia";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(95, 46);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(94, 15);
            label1.TabIndex = 3;
            label1.Text = "Nombre Usuario";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(95, 85);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(65, 15);
            label2.TabIndex = 4;
            label2.Text = "Pass actual";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(95, 118);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(67, 15);
            label3.TabIndex = 6;
            label3.Text = "Nueva Pass";
            // 
            // txtNewPass
            // 
            txtNewPass.Location = new Point(207, 116);
            txtNewPass.Margin = new Padding(2, 1, 2, 1);
            txtNewPass.Name = "txtNewPass";
            txtNewPass.Size = new Size(110, 23);
            txtNewPass.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(95, 149);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(61, 15);
            label4.TabIndex = 8;
            label4.Text = "Confirmar";
            // 
            // txtConfirmNewPass
            // 
            txtConfirmNewPass.Location = new Point(207, 147);
            txtConfirmNewPass.Margin = new Padding(2, 1, 2, 1);
            txtConfirmNewPass.Name = "txtConfirmNewPass";
            txtConfirmNewPass.Size = new Size(110, 23);
            txtConfirmNewPass.TabIndex = 7;
            // 
            // FrmCambiarPassword_90DI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(431, 221);
            Controls.Add(label4);
            Controls.Add(txtConfirmNewPass);
            Controls.Add(label3);
            Controls.Add(txtNewPass);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(txtPassActual);
            Controls.Add(textBox1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Margin = new Padding(2, 1, 2, 1);
            Name = "FrmCambiarPassword_90DI";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CambiarPassword";
            FormClosed += FrmCambiarPassword_200MI_FormClosed;
            Load += FrmCambiarPassword_90DI_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private TextBox txtPassActual;
        private Button button1;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtNewPass;
        private Label label4;
        private TextBox txtConfirmNewPass;
    }
}