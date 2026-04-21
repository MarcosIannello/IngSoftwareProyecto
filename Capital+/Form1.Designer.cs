namespace Capital_
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Btn_Login = new Button();
            txt_LoginName = new TextBox();
            txt_loginPass = new TextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // Btn_Login
            // 
            Btn_Login.Location = new Point(320, 326);
            Btn_Login.Name = "Btn_Login";
            Btn_Login.Size = new Size(150, 46);
            Btn_Login.TabIndex = 0;
            Btn_Login.Text = "Ingresar";
            Btn_Login.UseVisualStyleBackColor = true;
            Btn_Login.Click += Btn_Login_Click;
            // 
            // txt_LoginName
            // 
            txt_LoginName.Location = new Point(294, 170);
            txt_LoginName.Name = "txt_LoginName";
            txt_LoginName.Size = new Size(200, 39);
            txt_LoginName.TabIndex = 1;
            // 
            // txt_loginPass
            // 
            txt_loginPass.Location = new Point(294, 242);
            txt_loginPass.Name = "txt_loginPass";
            txt_loginPass.PasswordChar = '*';
            txt_loginPass.Size = new Size(200, 39);
            txt_loginPass.TabIndex = 2;
            txt_loginPass.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(355, 63);
            label1.Name = "label1";
            label1.Size = new Size(82, 32);
            label1.TabIndex = 3;
            label1.Text = "LOGIN";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(txt_loginPass);
            Controls.Add(txt_LoginName);
            Controls.Add(Btn_Login);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Btn_Login;
        private TextBox txt_LoginName;
        private TextBox txt_loginPass;
        private Label label1;
    }
}
