namespace Capital_
{
    partial class FrmAdminFamilias
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
            label1 = new Label();
            frmPanelContentAdminFamilias = new Panel();
            btnEliminarFamilia = new Button();
            lstFamiliasFamilia = new ListBox();
            pnlFamiliaNombre = new FlowLayoutPanel();
            label4 = new Label();
            txtNombreFamilia = new TextBox();
            lstFamilias = new ListBox();
            rdbModoConsulta = new RadioButton();
            rdbCrearFamilia = new RadioButton();
            rdbModificarFamilia = new RadioButton();
            lstPatentes = new ComboBox();
            LstFamiliasPatentes = new ListBox();
            label3 = new Label();
            label2 = new Label();
            button5 = new Button();
            btnAplicarCambios = new Button();
            btnQuitarPatente = new Button();
            btnAgregarPatente = new Button();
            pnlAgregarQuitarFamilias = new Panel();
            label5 = new Label();
            cmbFamiliasDisponibles = new ComboBox();
            button1 = new Button();
            button2 = new Button();
            frmPanelContentAdminFamilias.SuspendLayout();
            pnlFamiliaNombre.SuspendLayout();
            pnlAgregarQuitarFamilias.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(19, 18);
            label1.Name = "label1";
            label1.Size = new Size(218, 25);
            label1.TabIndex = 0;
            label1.Text = "Administrador de Familias";
            label1.Click += label1_Click;
            // 
            // frmPanelContentAdminFamilias
            // 
            frmPanelContentAdminFamilias.Controls.Add(btnEliminarFamilia);
            frmPanelContentAdminFamilias.Controls.Add(label1);
            frmPanelContentAdminFamilias.Controls.Add(lstFamiliasFamilia);
            frmPanelContentAdminFamilias.Controls.Add(pnlFamiliaNombre);
            frmPanelContentAdminFamilias.Controls.Add(lstFamilias);
            frmPanelContentAdminFamilias.Controls.Add(rdbModoConsulta);
            frmPanelContentAdminFamilias.Controls.Add(rdbCrearFamilia);
            frmPanelContentAdminFamilias.Controls.Add(rdbModificarFamilia);
            frmPanelContentAdminFamilias.Controls.Add(lstPatentes);
            frmPanelContentAdminFamilias.Controls.Add(LstFamiliasPatentes);
            frmPanelContentAdminFamilias.Controls.Add(label3);
            frmPanelContentAdminFamilias.Controls.Add(label2);
            frmPanelContentAdminFamilias.Controls.Add(button5);
            frmPanelContentAdminFamilias.Controls.Add(btnAplicarCambios);
            frmPanelContentAdminFamilias.Controls.Add(btnQuitarPatente);
            frmPanelContentAdminFamilias.Controls.Add(btnAgregarPatente);
            frmPanelContentAdminFamilias.Controls.Add(pnlAgregarQuitarFamilias);
            frmPanelContentAdminFamilias.Location = new Point(27, 26);
            frmPanelContentAdminFamilias.Margin = new Padding(4, 5, 4, 5);
            frmPanelContentAdminFamilias.Name = "frmPanelContentAdminFamilias";
            frmPanelContentAdminFamilias.Size = new Size(1711, 1092);
            frmPanelContentAdminFamilias.TabIndex = 41;
            // 
            // btnEliminarFamilia
            // 
            btnEliminarFamilia.BackColor = Color.Red;
            btnEliminarFamilia.Cursor = Cursors.Hand;
            btnEliminarFamilia.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEliminarFamilia.ForeColor = SystemColors.Control;
            btnEliminarFamilia.Location = new Point(629, 245);
            btnEliminarFamilia.Margin = new Padding(3, 2, 3, 2);
            btnEliminarFamilia.Name = "btnEliminarFamilia";
            btnEliminarFamilia.Size = new Size(54, 42);
            btnEliminarFamilia.TabIndex = 55;
            btnEliminarFamilia.Text = "x";
            btnEliminarFamilia.UseVisualStyleBackColor = false;
            btnEliminarFamilia.Visible = false;
            // 
            // lstFamiliasFamilia
            // 
            lstFamiliasFamilia.FormattingEnabled = true;
            lstFamiliasFamilia.ItemHeight = 25;
            lstFamiliasFamilia.Location = new Point(241, 585);
            lstFamiliasFamilia.Margin = new Padding(3, 2, 3, 2);
            lstFamiliasFamilia.Name = "lstFamiliasFamilia";
            lstFamiliasFamilia.Size = new Size(358, 254);
            lstFamiliasFamilia.TabIndex = 54;
            // 
            // pnlFamiliaNombre
            // 
            pnlFamiliaNombre.Controls.Add(label4);
            pnlFamiliaNombre.Controls.Add(txtNombreFamilia);
            pnlFamiliaNombre.Location = new Point(187, 245);
            pnlFamiliaNombre.Margin = new Padding(3, 2, 3, 2);
            pnlFamiliaNombre.Name = "pnlFamiliaNombre";
            pnlFamiliaNombre.Size = new Size(429, 42);
            pnlFamiliaNombre.TabIndex = 52;
            pnlFamiliaNombre.Visible = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 0);
            label4.Name = "label4";
            label4.Size = new Size(132, 25);
            label4.TabIndex = 0;
            label4.Text = "NombreFamilia";
            // 
            // txtNombreFamilia
            // 
            txtNombreFamilia.Location = new Point(141, 2);
            txtNombreFamilia.Margin = new Padding(3, 2, 3, 2);
            txtNombreFamilia.Name = "txtNombreFamilia";
            txtNombreFamilia.Size = new Size(281, 31);
            txtNombreFamilia.TabIndex = 1;
            // 
            // lstFamilias
            // 
            lstFamilias.FormattingEnabled = true;
            lstFamilias.ItemHeight = 25;
            lstFamilias.Location = new Point(760, 227);
            lstFamilias.Margin = new Padding(3, 2, 3, 2);
            lstFamilias.Name = "lstFamilias";
            lstFamilias.Size = new Size(330, 179);
            lstFamilias.TabIndex = 51;
            lstFamilias.Visible = false;
            // 
            // rdbModoConsulta
            // 
            rdbModoConsulta.AutoSize = true;
            rdbModoConsulta.Location = new Point(534, 147);
            rdbModoConsulta.Margin = new Padding(3, 2, 3, 2);
            rdbModoConsulta.Name = "rdbModoConsulta";
            rdbModoConsulta.Size = new Size(160, 29);
            rdbModoConsulta.TabIndex = 50;
            rdbModoConsulta.TabStop = true;
            rdbModoConsulta.Text = "Modo Consulta";
            rdbModoConsulta.UseVisualStyleBackColor = true;
            // 
            // rdbCrearFamilia
            // 
            rdbCrearFamilia.AutoSize = true;
            rdbCrearFamilia.Location = new Point(1004, 147);
            rdbCrearFamilia.Margin = new Padding(3, 2, 3, 2);
            rdbCrearFamilia.Name = "rdbCrearFamilia";
            rdbCrearFamilia.Size = new Size(137, 29);
            rdbCrearFamilia.TabIndex = 49;
            rdbCrearFamilia.TabStop = true;
            rdbCrearFamilia.Text = "Crear Familia";
            rdbCrearFamilia.UseVisualStyleBackColor = true;
            // 
            // rdbModificarFamilia
            // 
            rdbModificarFamilia.AutoSize = true;
            rdbModificarFamilia.Location = new Point(760, 147);
            rdbModificarFamilia.Margin = new Padding(3, 2, 3, 2);
            rdbModificarFamilia.Name = "rdbModificarFamilia";
            rdbModificarFamilia.Size = new Size(171, 29);
            rdbModificarFamilia.TabIndex = 48;
            rdbModificarFamilia.TabStop = true;
            rdbModificarFamilia.Text = "Modificar Familia";
            rdbModificarFamilia.UseVisualStyleBackColor = true;
            // 
            // lstPatentes
            // 
            lstPatentes.DropDownStyle = ComboBoxStyle.DropDownList;
            lstPatentes.FormattingEnabled = true;
            lstPatentes.Location = new Point(1166, 337);
            lstPatentes.Margin = new Padding(3, 2, 3, 2);
            lstPatentes.Name = "lstPatentes";
            lstPatentes.Size = new Size(358, 33);
            lstPatentes.TabIndex = 47;
            // 
            // LstFamiliasPatentes
            // 
            LstFamiliasPatentes.FormattingEnabled = true;
            LstFamiliasPatentes.ItemHeight = 25;
            LstFamiliasPatentes.Location = new Point(241, 335);
            LstFamiliasPatentes.Margin = new Padding(3, 2, 3, 2);
            LstFamiliasPatentes.Name = "LstFamiliasPatentes";
            LstFamiliasPatentes.Size = new Size(358, 254);
            LstFamiliasPatentes.TabIndex = 46;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(1303, 302);
            label3.Name = "label3";
            label3.Size = new Size(78, 25);
            label3.TabIndex = 45;
            label3.Text = "Patentes";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(334, 300);
            label2.Name = "label2";
            label2.Size = new Size(161, 25);
            label2.TabIndex = 44;
            label2.Text = "Familia -> Patentes";
            // 
            // button5
            // 
            button5.BackColor = Color.LightCoral;
            button5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button5.ForeColor = SystemColors.ControlLightLight;
            button5.Location = new Point(1166, 885);
            button5.Margin = new Padding(3, 2, 3, 2);
            button5.Name = "button5";
            button5.Size = new Size(357, 58);
            button5.TabIndex = 43;
            button5.Text = "Salir";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click_1;
            // 
            // btnAplicarCambios
            // 
            btnAplicarCambios.Location = new Point(241, 885);
            btnAplicarCambios.Margin = new Padding(3, 2, 3, 2);
            btnAplicarCambios.Name = "btnAplicarCambios";
            btnAplicarCambios.Size = new Size(357, 58);
            btnAplicarCambios.TabIndex = 42;
            btnAplicarCambios.Text = "Aplicar";
            btnAplicarCambios.UseVisualStyleBackColor = true;
            // 
            // btnQuitarPatente
            // 
            btnQuitarPatente.Location = new Point(849, 525);
            btnQuitarPatente.Margin = new Padding(3, 2, 3, 2);
            btnQuitarPatente.Name = "btnQuitarPatente";
            btnQuitarPatente.Size = new Size(156, 58);
            btnQuitarPatente.TabIndex = 41;
            btnQuitarPatente.Text = "Quitar Patente";
            btnQuitarPatente.UseVisualStyleBackColor = true;
            // 
            // btnAgregarPatente
            // 
            btnAgregarPatente.Location = new Point(849, 442);
            btnAgregarPatente.Margin = new Padding(3, 2, 3, 2);
            btnAgregarPatente.Name = "btnAgregarPatente";
            btnAgregarPatente.Size = new Size(156, 58);
            btnAgregarPatente.TabIndex = 40;
            btnAgregarPatente.Text = "Agregar Patente";
            btnAgregarPatente.UseVisualStyleBackColor = true;
            // 
            // pnlAgregarQuitarFamilias
            // 
            pnlAgregarQuitarFamilias.Controls.Add(label5);
            pnlAgregarQuitarFamilias.Controls.Add(cmbFamiliasDisponibles);
            pnlAgregarQuitarFamilias.Controls.Add(button1);
            pnlAgregarQuitarFamilias.Controls.Add(button2);
            pnlAgregarQuitarFamilias.Location = new Point(733, 603);
            pnlAgregarQuitarFamilias.Margin = new Padding(3, 2, 3, 2);
            pnlAgregarQuitarFamilias.Name = "pnlAgregarQuitarFamilias";
            pnlAgregarQuitarFamilias.Size = new Size(379, 237);
            pnlAgregarQuitarFamilias.TabIndex = 53;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(104, 32);
            label5.Name = "label5";
            label5.Size = new Size(172, 25);
            label5.TabIndex = 20;
            label5.Text = "Familias Disponibles";
            // 
            // cmbFamiliasDisponibles
            // 
            cmbFamiliasDisponibles.FormattingEnabled = true;
            cmbFamiliasDisponibles.Location = new Point(49, 70);
            cmbFamiliasDisponibles.Margin = new Padding(3, 2, 3, 2);
            cmbFamiliasDisponibles.Name = "cmbFamiliasDisponibles";
            cmbFamiliasDisponibles.Size = new Size(290, 33);
            cmbFamiliasDisponibles.TabIndex = 19;
            // 
            // button1
            // 
            button1.Location = new Point(201, 135);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(156, 58);
            button1.TabIndex = 18;
            button1.Text = "Quitar Familia";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(16, 135);
            button2.Margin = new Padding(3, 2, 3, 2);
            button2.Name = "button2";
            button2.Size = new Size(156, 58);
            button2.TabIndex = 17;
            button2.Text = "Agregar Familia";
            button2.UseVisualStyleBackColor = true;
            // 
            // FrmAdminFamilias
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1710, 1039);
            Controls.Add(frmPanelContentAdminFamilias);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Margin = new Padding(3, 2, 3, 2);
            Name = "FrmAdminFamilias";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmAdminFamilias_90DI";
            WindowState = FormWindowState.Maximized;
            Load += FrmAdminRoles_90DI_Load;
            frmPanelContentAdminFamilias.ResumeLayout(false);
            frmPanelContentAdminFamilias.PerformLayout();
            pnlFamiliaNombre.ResumeLayout(false);
            pnlFamiliaNombre.PerformLayout();
            pnlAgregarQuitarFamilias.ResumeLayout(false);
            pnlAgregarQuitarFamilias.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Panel frmPanelContentAdminFamilias;
        private Button btnEliminarFamilia;
        private ListBox lstFamiliasFamilia;
        private FlowLayoutPanel pnlFamiliaNombre;
        private Label label4;
        private TextBox txtNombreFamilia;
        private ListBox lstFamilias;
        private RadioButton rdbModoConsulta;
        private RadioButton rdbCrearFamilia;
        private RadioButton rdbModificarFamilia;
        private ComboBox lstPatentes;
        private ListBox LstFamiliasPatentes;
        private Label label3;
        private Label label2;
        private Button button5;
        private Button btnAplicarCambios;
        private Button btnQuitarPatente;
        private Button btnAgregarPatente;
        private Panel pnlAgregarQuitarFamilias;
        private Label label5;
        private ComboBox cmbFamiliasDisponibles;
        private Button button1;
        private Button button2;
    }
}