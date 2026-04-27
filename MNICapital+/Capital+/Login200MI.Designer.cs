namespace Capital_
{
    partial class Login200MI
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
            pictureBox1 = new PictureBox();
            label2 = new Label();
            label3 = new Label();
            btn_CloseApp = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // Btn_Login
            // 
            Btn_Login.Cursor = Cursors.Hand;
            Btn_Login.FlatAppearance.BorderColor = Color.White;
            Btn_Login.Location = new Point(319, 331);
            Btn_Login.Name = "Btn_Login";
            Btn_Login.Size = new Size(150, 46);
            Btn_Login.TabIndex = 0;
            Btn_Login.Text = "Ingresar";
            Btn_Login.UseVisualStyleBackColor = true;
            Btn_Login.UseWaitCursor = true;
            Btn_Login.Click += Btn_Login_Click;
            // 
            // txt_LoginName
            // 
            txt_LoginName.Location = new Point(368, 176);
            txt_LoginName.Name = "txt_LoginName";
            txt_LoginName.Size = new Size(200, 39);
            txt_LoginName.TabIndex = 1;
            // 
            // txt_loginPass
            // 
            txt_loginPass.Location = new Point(368, 242);
            txt_loginPass.Name = "txt_loginPass";
            txt_loginPass.PasswordChar = '*';
            txt_loginPass.Size = new Size(200, 39);
            txt_loginPass.TabIndex = 2;
            txt_loginPass.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei", 25.875F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(277, 42);
            label1.Name = "label1";
            label1.Size = new Size(256, 89);
            label1.TabIndex = 3;
            label1.Text = "LOGIN";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Gemini_Generated_Image_6gtvvi6gtvvi6gtv;
            pictureBox1.Location = new Point(595, 357);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(200, 100);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(224, 179);
            label2.Name = "label2";
            label2.Size = new Size(121, 32);
            label2.TabIndex = 5;
            label2.Text = "Username";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(224, 245);
            label3.Name = "label3";
            label3.Size = new Size(111, 32);
            label3.TabIndex = 6;
            label3.Text = "Password";
            // 
            // btn_CloseApp
            // 
            btn_CloseApp.BackColor = Color.LightPink;
            btn_CloseApp.Location = new Point(739, 12);
            btn_CloseApp.Name = "btn_CloseApp";
            btn_CloseApp.Size = new Size(56, 46);
            btn_CloseApp.TabIndex = 7;
            btn_CloseApp.Text = "X";
            btn_CloseApp.UseVisualStyleBackColor = false;
            btn_CloseApp.Click += btn_CloseApp_Click;
            // 
            // Login200MI
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(842, 485);
            Controls.Add(btn_CloseApp);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Controls.Add(txt_loginPass);
            Controls.Add(txt_LoginName);
            Controls.Add(Btn_Login);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Login200MI";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Btn_Login;
        private TextBox txt_LoginName;
        private TextBox txt_loginPass;
        private Label label1;
        private PictureBox pictureBox1;
        private Label label2;
        private Label label3;
        private Button btn_CloseApp;
    }
}
