namespace UI_90DI
{
    partial class FrmGestionUsuarios
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
            btnCrear = new Button();
            btnDesbloquear = new Button();
            btnModificar = new Button();
            btnActive = new Button();
            btnAplicar = new Button();
            label1 = new Label();
            dataGridUsers = new DataGridView();
            txtDni = new TextBox();
            txtApellido = new TextBox();
            txtEmail = new TextBox();
            txtNombre = new TextBox();
            txtLogin = new TextBox();
            txtRol = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            chkBlock = new CheckBox();
            chkActiveUSer = new CheckBox();
            lblCantUsers = new Label();
            rdbActive = new RadioButton();
            rdbTodos = new RadioButton();
            txtActiveMode = new TextBox();
            label10 = new Label();
            btnCancelar = new Button();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridUsers).BeginInit();
            SuspendLayout();
            // 
            // btnCrear
            // 
            btnCrear.BackColor = Color.AliceBlue;
            btnCrear.Cursor = Cursors.Hand;
            btnCrear.Location = new Point(1101, 211);
            btnCrear.Margin = new Padding(2, 1, 2, 1);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(108, 33);
            btnCrear.TabIndex = 0;
            btnCrear.Text = "Crear";
            btnCrear.UseVisualStyleBackColor = false;
            btnCrear.Click += btnCrear_Click;
            // 
            // btnDesbloquear
            // 
            btnDesbloquear.BackColor = Color.AliceBlue;
            btnDesbloquear.Cursor = Cursors.Hand;
            btnDesbloquear.Enabled = false;
            btnDesbloquear.Location = new Point(1101, 248);
            btnDesbloquear.Margin = new Padding(2, 1, 2, 1);
            btnDesbloquear.Name = "btnDesbloquear";
            btnDesbloquear.Size = new Size(108, 33);
            btnDesbloquear.TabIndex = 1;
            btnDesbloquear.Text = "Desbloquear";
            btnDesbloquear.UseVisualStyleBackColor = false;
            btnDesbloquear.Click += btnDesbloquear_Click;
            // 
            // btnModificar
            // 
            btnModificar.BackColor = Color.AliceBlue;
            btnModificar.Cursor = Cursors.Hand;
            btnModificar.Location = new Point(1101, 287);
            btnModificar.Margin = new Padding(2, 1, 2, 1);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(108, 33);
            btnModificar.TabIndex = 2;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = false;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnActive
            // 
            btnActive.BackColor = Color.AliceBlue;
            btnActive.Cursor = Cursors.Hand;
            btnActive.Location = new Point(1101, 326);
            btnActive.Margin = new Padding(2, 1, 2, 1);
            btnActive.Name = "btnActive";
            btnActive.Size = new Size(108, 33);
            btnActive.TabIndex = 3;
            btnActive.Text = "Act / Desactivar";
            btnActive.UseVisualStyleBackColor = false;
            btnActive.Click += btnActive_Click;
            // 
            // btnAplicar
            // 
            btnAplicar.BackColor = Color.GreenYellow;
            btnAplicar.Location = new Point(981, 530);
            btnAplicar.Margin = new Padding(2, 1, 2, 1);
            btnAplicar.Name = "btnAplicar";
            btnAplicar.Size = new Size(103, 37);
            btnAplicar.TabIndex = 4;
            btnAplicar.Text = "Aplicar";
            btnAplicar.UseVisualStyleBackColor = false;
            btnAplicar.Click += btnAplicar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(359, 185);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(52, 15);
            label1.TabIndex = 7;
            label1.Text = "Usuarios";
            // 
            // dataGridUsers
            // 
            dataGridUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridUsers.Location = new Point(300, 212);
            dataGridUsers.Margin = new Padding(2, 1, 2, 1);
            dataGridUsers.Name = "dataGridUsers";
            dataGridUsers.RowHeadersWidth = 82;
            dataGridUsers.Size = new Size(762, 159);
            dataGridUsers.TabIndex = 8;
            dataGridUsers.CellClick += dataGridUsers_CellClick;
            // 
            // txtDni
            // 
            txtDni.Location = new Point(383, 397);
            txtDni.Margin = new Padding(2, 1, 2, 1);
            txtDni.MaxLength = 8;
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(110, 23);
            txtDni.TabIndex = 9;
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(383, 418);
            txtApellido.Margin = new Padding(2, 1, 2, 1);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(110, 23);
            txtApellido.TabIndex = 10;
            txtApellido.TextChanged += txtApellido_TextChanged;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(383, 461);
            txtEmail.Margin = new Padding(2, 1, 2, 1);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(110, 23);
            txtEmail.TabIndex = 12;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(383, 440);
            txtNombre.Margin = new Padding(2, 1, 2, 1);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(110, 23);
            txtNombre.TabIndex = 11;
            txtNombre.TextChanged += txtNombre_TextChanged;
            // 
            // txtLogin
            // 
            txtLogin.Location = new Point(383, 502);
            txtLogin.Margin = new Padding(2, 1, 2, 1);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(110, 23);
            txtLogin.TabIndex = 14;
            // 
            // txtRol
            // 
            txtRol.Location = new Point(383, 481);
            txtRol.Margin = new Padding(2, 1, 2, 1);
            txtRol.Name = "txtRol";
            txtRol.Size = new Size(110, 23);
            txtRol.TabIndex = 13;
            // 
            // label2
            // 
            label2.BorderStyle = BorderStyle.FixedSingle;
            label2.Location = new Point(300, 397);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(81, 19);
            label2.TabIndex = 17;
            label2.Text = "DNI";
            // 
            // label3
            // 
            label3.BorderStyle = BorderStyle.FixedSingle;
            label3.Location = new Point(300, 419);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(81, 19);
            label3.TabIndex = 18;
            label3.Text = "Apellido";
            // 
            // label4
            // 
            label4.BorderStyle = BorderStyle.FixedSingle;
            label4.Location = new Point(300, 462);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(81, 19);
            label4.TabIndex = 20;
            label4.Text = "Email";
            // 
            // label5
            // 
            label5.BorderStyle = BorderStyle.FixedSingle;
            label5.Location = new Point(300, 440);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(81, 19);
            label5.TabIndex = 19;
            label5.Text = "Nombre";
            // 
            // label6
            // 
            label6.BorderStyle = BorderStyle.FixedSingle;
            label6.Location = new Point(300, 503);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(81, 19);
            label6.TabIndex = 22;
            label6.Text = "Login";
            // 
            // label7
            // 
            label7.BorderStyle = BorderStyle.FixedSingle;
            label7.Location = new Point(300, 481);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(81, 19);
            label7.TabIndex = 21;
            label7.Text = "ROL";
            // 
            // label8
            // 
            label8.BorderStyle = BorderStyle.FixedSingle;
            label8.Location = new Point(300, 547);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(81, 19);
            label8.TabIndex = 24;
            label8.Text = "Activo";
            // 
            // label9
            // 
            label9.BorderStyle = BorderStyle.FixedSingle;
            label9.Location = new Point(300, 525);
            label9.Margin = new Padding(2, 0, 2, 0);
            label9.Name = "label9";
            label9.Size = new Size(81, 19);
            label9.TabIndex = 23;
            label9.Text = "Blockeado";
            // 
            // chkBlock
            // 
            chkBlock.AutoSize = true;
            chkBlock.Location = new Point(406, 527);
            chkBlock.Margin = new Padding(2, 1, 2, 1);
            chkBlock.Name = "chkBlock";
            chkBlock.Size = new Size(55, 19);
            chkBlock.TabIndex = 25;
            chkBlock.Text = "Block";
            chkBlock.UseVisualStyleBackColor = true;
            // 
            // chkActiveUSer
            // 
            chkActiveUSer.AutoSize = true;
            chkActiveUSer.Checked = true;
            chkActiveUSer.CheckState = CheckState.Checked;
            chkActiveUSer.Location = new Point(406, 548);
            chkActiveUSer.Margin = new Padding(2, 1, 2, 1);
            chkActiveUSer.Name = "chkActiveUSer";
            chkActiveUSer.Size = new Size(60, 19);
            chkActiveUSer.TabIndex = 26;
            chkActiveUSer.Text = "Activo";
            chkActiveUSer.UseVisualStyleBackColor = true;
            // 
            // lblCantUsers
            // 
            lblCantUsers.AutoSize = true;
            lblCantUsers.Location = new Point(792, 185);
            lblCantUsers.Margin = new Padding(2, 0, 2, 0);
            lblCantUsers.Name = "lblCantUsers";
            lblCantUsers.Size = new Size(109, 15);
            lblCantUsers.TabIndex = 27;
            lblCantUsers.Text = "Cantidad Usuarios: ";
            // 
            // rdbActive
            // 
            rdbActive.AutoSize = true;
            rdbActive.Location = new Point(564, 185);
            rdbActive.Margin = new Padding(2, 1, 2, 1);
            rdbActive.Name = "rdbActive";
            rdbActive.Size = new Size(64, 19);
            rdbActive.TabIndex = 28;
            rdbActive.TabStop = true;
            rdbActive.Text = "Activos";
            rdbActive.UseVisualStyleBackColor = true;
            rdbActive.CheckedChanged += rdbActive_CheckedChanged;
            // 
            // rdbTodos
            // 
            rdbTodos.AutoSize = true;
            rdbTodos.Location = new Point(659, 185);
            rdbTodos.Margin = new Padding(2, 1, 2, 1);
            rdbTodos.Name = "rdbTodos";
            rdbTodos.Size = new Size(57, 19);
            rdbTodos.TabIndex = 29;
            rdbTodos.TabStop = true;
            rdbTodos.Text = "Todos";
            rdbTodos.UseVisualStyleBackColor = true;
            rdbTodos.CheckedChanged += rdbTodos_CheckedChanged;
            // 
            // txtActiveMode
            // 
            txtActiveMode.Location = new Point(797, 413);
            txtActiveMode.Margin = new Padding(2, 1, 2, 1);
            txtActiveMode.Name = "txtActiveMode";
            txtActiveMode.ReadOnly = true;
            txtActiveMode.Size = new Size(213, 23);
            txtActiveMode.TabIndex = 30;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(878, 389);
            label10.Margin = new Padding(2, 0, 2, 0);
            label10.Name = "label10";
            label10.Size = new Size(39, 15);
            label10.TabIndex = 31;
            label10.Text = "Modo";
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.LightCoral;
            btnCancelar.Location = new Point(1106, 530);
            btnCancelar.Margin = new Padding(2, 1, 2, 1);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(103, 37);
            btnCancelar.TabIndex = 32;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.AliceBlue;
            button1.Cursor = Cursors.Hand;
            button1.Location = new Point(1101, 371);
            button1.Margin = new Padding(2, 1, 2, 1);
            button1.Name = "button1";
            button1.Size = new Size(108, 33);
            button1.TabIndex = 33;
            button1.Text = "Salir";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // FrmGestionUsuarios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1628, 777);
            Controls.Add(button1);
            Controls.Add(btnCancelar);
            Controls.Add(label10);
            Controls.Add(txtActiveMode);
            Controls.Add(rdbTodos);
            Controls.Add(rdbActive);
            Controls.Add(lblCantUsers);
            Controls.Add(chkActiveUSer);
            Controls.Add(chkBlock);
            Controls.Add(label8);
            Controls.Add(label9);
            Controls.Add(label6);
            Controls.Add(label7);
            Controls.Add(label4);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtLogin);
            Controls.Add(txtRol);
            Controls.Add(txtEmail);
            Controls.Add(txtNombre);
            Controls.Add(txtApellido);
            Controls.Add(txtDni);
            Controls.Add(dataGridUsers);
            Controls.Add(label1);
            Controls.Add(btnAplicar);
            Controls.Add(btnActive);
            Controls.Add(btnModificar);
            Controls.Add(btnDesbloquear);
            Controls.Add(btnCrear);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Margin = new Padding(2, 1, 2, 1);
            Name = "FrmGestionUsuarios";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CrudUsers";
            WindowState = FormWindowState.Maximized;
            FormClosed += CrudUsers_FormClosed;
            Load += CrudUsers_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridUsers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCrear;
        private Button btnDesbloquear;
        private Button btnModificar;
        private Button btnActive;
        private Button btnAplicar;
        private Label label1;
        private DataGridView dataGridUsers;
        private TextBox txtDni;
        private TextBox txtApellido;
        private TextBox txtEmail;
        private TextBox txtNombre;
        private TextBox txtLogin;
        private TextBox txtRol;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private CheckBox chkBlock;
        private CheckBox chkActiveUSer;
        private Label lblCantUsers;
        private RadioButton rdbActive;
        private RadioButton rdbTodos;
        private TextBox txtActiveMode;
        private Label label10;
        private Button btnCancelar;
        private Button button1;
    }
}