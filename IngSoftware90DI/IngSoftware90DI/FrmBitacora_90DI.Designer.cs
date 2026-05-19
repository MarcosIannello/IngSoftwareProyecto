namespace UI_90DI
{
    partial class FrmBitacora_90DI
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
            btnApply = new Button();
            dataGridEvents = new DataGridView();
            txtDni = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            lblCantUsers = new Label();
            txtLastName = new TextBox();
            label10 = new Label();
            btnPrint = new Button();
            btnSalir = new Button();
            label1 = new Label();
            txtName = new TextBox();
            btnClean = new Button();
            dtpInitialDate = new DateTimePicker();
            dtpEndDate = new DateTimePicker();
            cmbLevel = new ComboBox();
            cmdEvent = new ComboBox();
            cmbModule = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGridEvents).BeginInit();
            SuspendLayout();
            // 
            // btnApply
            // 
            btnApply.BackColor = SystemColors.ButtonFace;
            btnApply.Location = new Point(699, 738);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(191, 50);
            btnApply.TabIndex = 4;
            btnApply.Text = "Aplicar";
            btnApply.UseVisualStyleBackColor = false;
            btnApply.Click += btnAplicar_Click;
            // 
            // dataGridEvents
            // 
            dataGridEvents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridEvents.Location = new Point(55, 84);
            dataGridEvents.Name = "dataGridEvents";
            dataGridEvents.RowHeadersWidth = 82;
            dataGridEvents.Size = new Size(1306, 339);
            dataGridEvents.TabIndex = 8;
            dataGridEvents.CellClick += dataGridEvents_CellClick;
            dataGridEvents.CellContentClick += dataGridEvents_CellContentClick;
            // 
            // txtDni
            // 
            txtDni.Location = new Point(296, 565);
            txtDni.MaxLength = 8;
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(200, 39);
            txtDni.TabIndex = 9;
            // 
            // label2
            // 
            label2.BorderStyle = BorderStyle.FixedSingle;
            label2.Location = new Point(142, 565);
            label2.Name = "label2";
            label2.Size = new Size(148, 39);
            label2.TabIndex = 17;
            label2.Text = "Login";
            // 
            // label3
            // 
            label3.BorderStyle = BorderStyle.FixedSingle;
            label3.Location = new Point(973, 568);
            label3.Name = "label3";
            label3.Size = new Size(148, 39);
            label3.TabIndex = 18;
            label3.Text = "Fecha Fin";
            // 
            // label4
            // 
            label4.BorderStyle = BorderStyle.FixedSingle;
            label4.Location = new Point(554, 568);
            label4.Name = "label4";
            label4.Size = new Size(148, 39);
            label4.TabIndex = 20;
            label4.Text = "Fecha Inicio";
            // 
            // label5
            // 
            label5.BorderStyle = BorderStyle.FixedSingle;
            label5.Location = new Point(142, 640);
            label5.Name = "label5";
            label5.Size = new Size(148, 39);
            label5.TabIndex = 19;
            label5.Text = "Modulo";
            // 
            // label6
            // 
            label6.BorderStyle = BorderStyle.FixedSingle;
            label6.Location = new Point(554, 640);
            label6.Name = "label6";
            label6.Size = new Size(148, 39);
            label6.TabIndex = 22;
            label6.Text = "Evento";
            // 
            // label7
            // 
            label7.BorderStyle = BorderStyle.FixedSingle;
            label7.Location = new Point(973, 638);
            label7.Name = "label7";
            label7.Size = new Size(148, 39);
            label7.TabIndex = 21;
            label7.Text = "Criticidad";
            // 
            // lblCantUsers
            // 
            lblCantUsers.AutoSize = true;
            lblCantUsers.Location = new Point(55, 21);
            lblCantUsers.Name = "lblCantUsers";
            lblCantUsers.Size = new Size(228, 32);
            lblCantUsers.TabIndex = 27;
            lblCantUsers.Text = "BITACORA EVENTOS";
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(979, 494);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(212, 39);
            txtLastName.TabIndex = 30;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(866, 497);
            label10.Name = "label10";
            label10.Size = new Size(102, 32);
            label10.TabIndex = 31;
            label10.Text = "Apellido";
            // 
            // btnPrint
            // 
            btnPrint.BackColor = SystemColors.ButtonFace;
            btnPrint.Location = new Point(1187, 738);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(191, 50);
            btnPrint.TabIndex = 32;
            btnPrint.Text = "Imprimir";
            btnPrint.UseVisualStyleBackColor = false;
            btnPrint.Click += btnPrint_Click;
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.FromArgb(255, 128, 128);
            btnSalir.Cursor = Cursors.Hand;
            btnSalir.Location = new Point(1434, 41);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(200, 70);
            btnSalir.TabIndex = 33;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(379, 497);
            label1.Name = "label1";
            label1.Size = new Size(102, 32);
            label1.TabIndex = 35;
            label1.Text = "Nombre";
            // 
            // txtName
            // 
            txtName.Location = new Point(491, 494);
            txtName.Name = "txtName";
            txtName.Size = new Size(212, 39);
            txtName.TabIndex = 34;
            // 
            // btnClean
            // 
            btnClean.BackColor = SystemColors.ButtonFace;
            btnClean.Location = new Point(248, 738);
            btnClean.Name = "btnClean";
            btnClean.Size = new Size(191, 50);
            btnClean.TabIndex = 36;
            btnClean.Text = "Limpiar";
            btnClean.UseVisualStyleBackColor = false;
            btnClean.Click += btnClean_Click;
            // 
            // dtpInitialDate
            // 
            dtpInitialDate.Location = new Point(708, 568);
            dtpInitialDate.Name = "dtpInitialDate";
            dtpInitialDate.Size = new Size(192, 39);
            dtpInitialDate.TabIndex = 37;
            // 
            // dtpEndDate
            // 
            dtpEndDate.Location = new Point(1139, 568);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Size = new Size(192, 39);
            dtpEndDate.TabIndex = 38;
            // 
            // cmbLevel
            // 
            cmbLevel.FormattingEnabled = true;
            cmbLevel.Items.AddRange(new object[] { "1", "2", "3", "4", "5" });
            cmbLevel.Location = new Point(1140, 639);
            cmbLevel.Name = "cmbLevel";
            cmbLevel.Size = new Size(191, 40);
            cmbLevel.TabIndex = 39;
            // 
            // cmdEvent
            // 
            cmdEvent.FormattingEnabled = true;
            cmdEvent.Items.AddRange(new object[] { "Logout", "Login", "Crear Usuario", "Cambiar Clave" });
            cmdEvent.Location = new Point(709, 640);
            cmdEvent.Name = "cmdEvent";
            cmdEvent.Size = new Size(191, 40);
            cmdEvent.TabIndex = 40;
            // 
            // cmbModule
            // 
            cmbModule.FormattingEnabled = true;
            cmbModule.Items.AddRange(new object[] { "Usuarios", "Maestro", "Eventos" });
            cmbModule.Location = new Point(296, 640);
            cmbModule.Name = "cmbModule";
            cmbModule.Size = new Size(200, 40);
            cmbModule.TabIndex = 41;
            // 
            // Bitacora
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1675, 812);
            Controls.Add(cmbModule);
            Controls.Add(cmdEvent);
            Controls.Add(cmbLevel);
            Controls.Add(dtpEndDate);
            Controls.Add(dtpInitialDate);
            Controls.Add(btnClean);
            Controls.Add(label1);
            Controls.Add(txtName);
            Controls.Add(btnSalir);
            Controls.Add(btnPrint);
            Controls.Add(label10);
            Controls.Add(txtLastName);
            Controls.Add(lblCantUsers);
            Controls.Add(label6);
            Controls.Add(label7);
            Controls.Add(label4);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtDni);
            Controls.Add(dataGridEvents);
            Controls.Add(btnApply);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Bitacora";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CrudUsers";
            FormClosed += CrudUsers_FormClosed;
            ((System.ComponentModel.ISupportInitialize)dataGridEvents).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnApply;
        private DataGridView dataGridEvents;
        private TextBox txtDni;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label lblCantUsers;
        private TextBox txtLastName;
        private Label label10;
        private Button btnPrint;
        private Button btnSalir;
        private Label label1;
        private TextBox txtName;
        private Button btnClean;
        private DateTimePicker dtpInitialDate;
        private DateTimePicker dtpEndDate;
        private ComboBox cmbLevel;
        private ComboBox cmdEvent;
        private ComboBox cmbModule;
    }
}