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
            btnApply.Location = new Point(743, 543);
            btnApply.Margin = new Padding(2, 1, 2, 1);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(103, 23);
            btnApply.TabIndex = 4;
            btnApply.Text = "Aplicar";
            btnApply.UseVisualStyleBackColor = false;
            btnApply.Click += btnAplicar_Click;
            // 
            // dataGridEvents
            // 
            dataGridEvents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridEvents.Location = new Point(429, 236);
            dataGridEvents.Margin = new Padding(2, 1, 2, 1);
            dataGridEvents.Name = "dataGridEvents";
            dataGridEvents.RowHeadersWidth = 82;
            dataGridEvents.Size = new Size(703, 159);
            dataGridEvents.TabIndex = 8;
            dataGridEvents.CellClick += dataGridEvents_CellClick;
            dataGridEvents.CellContentClick += dataGridEvents_CellContentClick;
            // 
            // txtDni
            // 
            txtDni.Location = new Point(557, 463);
            txtDni.Margin = new Padding(2, 1, 2, 1);
            txtDni.MaxLength = 8;
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(110, 23);
            txtDni.TabIndex = 9;
            // 
            // label2
            // 
            label2.BorderStyle = BorderStyle.FixedSingle;
            label2.Location = new Point(474, 463);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(81, 19);
            label2.TabIndex = 17;
            label2.Text = "Login";
            // 
            // label3
            // 
            label3.BorderStyle = BorderStyle.FixedSingle;
            label3.Location = new Point(922, 464);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(81, 19);
            label3.TabIndex = 18;
            label3.Text = "Fecha Fin";
            // 
            // label4
            // 
            label4.BorderStyle = BorderStyle.FixedSingle;
            label4.Location = new Point(696, 464);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(81, 19);
            label4.TabIndex = 20;
            label4.Text = "Fecha Inicio";
            // 
            // label5
            // 
            label5.BorderStyle = BorderStyle.FixedSingle;
            label5.Location = new Point(474, 498);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(81, 19);
            label5.TabIndex = 19;
            label5.Text = "Modulo";
            // 
            // label6
            // 
            label6.BorderStyle = BorderStyle.FixedSingle;
            label6.Location = new Point(696, 498);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(81, 19);
            label6.TabIndex = 22;
            label6.Text = "Evento";
            // 
            // label7
            // 
            label7.BorderStyle = BorderStyle.FixedSingle;
            label7.Location = new Point(922, 497);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(81, 19);
            label7.TabIndex = 21;
            label7.Text = "Criticidad";
            // 
            // lblCantUsers
            // 
            lblCantUsers.AutoSize = true;
            lblCantUsers.Location = new Point(429, 207);
            lblCantUsers.Margin = new Padding(2, 0, 2, 0);
            lblCantUsers.Name = "lblCantUsers";
            lblCantUsers.Size = new Size(115, 15);
            lblCantUsers.TabIndex = 27;
            lblCantUsers.Text = "BITACORA EVENTOS";
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(880, 426);
            txtLastName.Margin = new Padding(2, 1, 2, 1);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(155, 23);
            txtLastName.TabIndex = 30;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(819, 427);
            label10.Margin = new Padding(2, 0, 2, 0);
            label10.Name = "label10";
            label10.Size = new Size(51, 15);
            label10.TabIndex = 31;
            label10.Text = "Apellido";
            // 
            // btnPrint
            // 
            btnPrint.BackColor = SystemColors.ButtonFace;
            btnPrint.Location = new Point(1029, 543);
            btnPrint.Margin = new Padding(2, 1, 2, 1);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(103, 23);
            btnPrint.TabIndex = 32;
            btnPrint.Text = "Imprimir";
            btnPrint.UseVisualStyleBackColor = false;
            btnPrint.Click += btnPrint_Click;
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.FromArgb(255, 128, 128);
            btnSalir.Cursor = Cursors.Hand;
            btnSalir.Location = new Point(1171, 216);
            btnSalir.Margin = new Padding(2, 1, 2, 1);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(108, 33);
            btnSalir.TabIndex = 33;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(557, 427);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 35;
            label1.Text = "Nombre";
            // 
            // txtName
            // 
            txtName.Location = new Point(617, 426);
            txtName.Margin = new Padding(2, 1, 2, 1);
            txtName.Name = "txtName";
            txtName.Size = new Size(155, 23);
            txtName.TabIndex = 34;
            // 
            // btnClean
            // 
            btnClean.BackColor = SystemColors.ButtonFace;
            btnClean.Location = new Point(429, 543);
            btnClean.Margin = new Padding(2, 1, 2, 1);
            btnClean.Name = "btnClean";
            btnClean.Size = new Size(103, 23);
            btnClean.TabIndex = 36;
            btnClean.Text = "Limpiar";
            btnClean.UseVisualStyleBackColor = false;
            btnClean.Click += btnClean_Click;
            // 
            // dtpInitialDate
            // 
            dtpInitialDate.Location = new Point(779, 464);
            dtpInitialDate.Margin = new Padding(2, 1, 2, 1);
            dtpInitialDate.Name = "dtpInitialDate";
            dtpInitialDate.Size = new Size(105, 23);
            dtpInitialDate.TabIndex = 37;
            // 
            // dtpEndDate
            // 
            dtpEndDate.Location = new Point(1011, 464);
            dtpEndDate.Margin = new Padding(2, 1, 2, 1);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Size = new Size(105, 23);
            dtpEndDate.TabIndex = 38;
            // 
            // cmbLevel
            // 
            cmbLevel.FormattingEnabled = true;
            cmbLevel.Items.AddRange(new object[] { "1", "2", "3", "4", "5" });
            cmbLevel.Location = new Point(1012, 498);
            cmbLevel.Margin = new Padding(2, 1, 2, 1);
            cmbLevel.Name = "cmbLevel";
            cmbLevel.Size = new Size(105, 23);
            cmbLevel.TabIndex = 39;
            // 
            // cmdEvent
            // 
            cmdEvent.FormattingEnabled = true;
            cmdEvent.Items.AddRange(new object[] { "Logout", "Login", "Crear Usuario", "Cambiar Clave" });
            cmdEvent.Location = new Point(780, 498);
            cmdEvent.Margin = new Padding(2, 1, 2, 1);
            cmdEvent.Name = "cmdEvent";
            cmdEvent.Size = new Size(105, 23);
            cmdEvent.TabIndex = 40;
            // 
            // cmbModule
            // 
            cmbModule.FormattingEnabled = true;
            cmbModule.Items.AddRange(new object[] { "Usuarios", "Maestro", "Eventos" });
            cmbModule.Location = new Point(557, 498);
            cmbModule.Margin = new Padding(2, 1, 2, 1);
            cmbModule.Name = "cmbModule";
            cmbModule.Size = new Size(110, 23);
            cmbModule.TabIndex = 41;
            // 
            // FrmBitacora_90DI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1785, 797);
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
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Margin = new Padding(2, 1, 2, 1);
            Name = "FrmBitacora_90DI";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bitácora de Eventos";
            WindowState = FormWindowState.Maximized;
            FormClosed += FrmBitacora_90DI_FormClosed;
            Load += FrmBitacora_90DI_Load;
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