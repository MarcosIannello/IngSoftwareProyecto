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
            btnAgregarPatente = new Button();
            btnQuitarPatente = new Button();
            btnAplicarCambios = new Button();
            button5 = new Button();
            label2 = new Label();
            label3 = new Label();
            LstFamiliasPatentes = new ListBox();
            lstPatentes = new ListBox();
            rdbModificarFamilia = new RadioButton();
            rdbCrearFamilia = new RadioButton();
            rdbModoConsulta = new RadioButton();
            lstFamilias = new ListBox();
            pnlFamiliaNombre = new FlowLayoutPanel();
            label4 = new Label();
            txtNombreFamilia = new TextBox();
            button1 = new Button();
            button2 = new Button();
            pnlAgregarQuitarFamilias = new Panel();
            label5 = new Label();
            cmbFamiliasDisponibles = new ComboBox();
            lstFamiliasFamilia = new ListBox();
            pnlFamiliaNombre.SuspendLayout();
            pnlAgregarQuitarFamilias.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(289, 32);
            label1.TabIndex = 0;
            label1.Text = "Administrador de Familias";
            // 
            // btnAgregarPatente
            // 
            btnAgregarPatente.Location = new Point(793, 434);
            btnAgregarPatente.Name = "btnAgregarPatente";
            btnAgregarPatente.Size = new Size(202, 74);
            btnAgregarPatente.TabIndex = 3;
            btnAgregarPatente.Text = "Agregar Patente";
            btnAgregarPatente.UseVisualStyleBackColor = true;
            btnAgregarPatente.Click += btnAgregarPatente_Click;
            // 
            // btnQuitarPatente
            // 
            btnQuitarPatente.Location = new Point(793, 541);
            btnQuitarPatente.Name = "btnQuitarPatente";
            btnQuitarPatente.Size = new Size(202, 74);
            btnQuitarPatente.TabIndex = 4;
            btnQuitarPatente.Text = "Quitar Patente";
            btnQuitarPatente.UseVisualStyleBackColor = true;
            btnQuitarPatente.Click += btnQuitarPatente_Click;
            // 
            // btnAplicarCambios
            // 
            btnAplicarCambios.Location = new Point(101, 1002);
            btnAplicarCambios.Name = "btnAplicarCambios";
            btnAplicarCambios.Size = new Size(465, 74);
            btnAplicarCambios.TabIndex = 5;
            btnAplicarCambios.Text = "Aplicar";
            btnAplicarCambios.UseVisualStyleBackColor = true;
            btnAplicarCambios.Click += btnAplicarCambios_Click;
            // 
            // button5
            // 
            button5.Location = new Point(1204, 1002);
            button5.Name = "button5";
            button5.Size = new Size(465, 74);
            button5.TabIndex = 7;
            button5.Text = "Salir";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(221, 254);
            label2.Name = "label2";
            label2.Size = new Size(218, 32);
            label2.TabIndex = 8;
            label2.Text = "Familia -> Patentes";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(1383, 254);
            label3.Name = "label3";
            label3.Size = new Size(104, 32);
            label3.TabIndex = 9;
            label3.Text = "Patentes";
            // 
            // LstFamiliasPatentes
            // 
            LstFamiliasPatentes.FormattingEnabled = true;
            LstFamiliasPatentes.Location = new Point(101, 299);
            LstFamiliasPatentes.Name = "LstFamiliasPatentes";
            LstFamiliasPatentes.Size = new Size(465, 324);
            LstFamiliasPatentes.TabIndex = 10;
            LstFamiliasPatentes.SelectedIndexChanged += LstFamiliasPatentes_SelectedIndexChanged;
            // 
            // lstPatentes
            // 
            lstPatentes.FormattingEnabled = true;
            lstPatentes.Location = new Point(1204, 299);
            lstPatentes.Name = "lstPatentes";
            lstPatentes.Size = new Size(465, 644);
            lstPatentes.TabIndex = 11;
            lstPatentes.SelectedIndexChanged += lstPatentes_SelectedIndexChanged;
            // 
            // rdbModificarFamilia
            // 
            rdbModificarFamilia.AutoSize = true;
            rdbModificarFamilia.Location = new Point(775, 58);
            rdbModificarFamilia.Name = "rdbModificarFamilia";
            rdbModificarFamilia.Size = new Size(227, 36);
            rdbModificarFamilia.TabIndex = 12;
            rdbModificarFamilia.TabStop = true;
            rdbModificarFamilia.Text = "Modificar Familia";
            rdbModificarFamilia.UseVisualStyleBackColor = true;
            rdbModificarFamilia.CheckedChanged += rdbModificarFamilia_CheckedChanged;
            // 
            // rdbCrearFamilia
            // 
            rdbCrearFamilia.AutoSize = true;
            rdbCrearFamilia.Location = new Point(1092, 58);
            rdbCrearFamilia.Name = "rdbCrearFamilia";
            rdbCrearFamilia.Size = new Size(182, 36);
            rdbCrearFamilia.TabIndex = 13;
            rdbCrearFamilia.TabStop = true;
            rdbCrearFamilia.Text = "Crear Familia";
            rdbCrearFamilia.UseVisualStyleBackColor = true;
            rdbCrearFamilia.CheckedChanged += rdbCrearFamilia_CheckedChanged;
            // 
            // rdbModoConsulta
            // 
            rdbModoConsulta.AutoSize = true;
            rdbModoConsulta.Location = new Point(481, 58);
            rdbModoConsulta.Name = "rdbModoConsulta";
            rdbModoConsulta.Size = new Size(209, 36);
            rdbModoConsulta.TabIndex = 14;
            rdbModoConsulta.TabStop = true;
            rdbModoConsulta.Text = "Modo Consulta";
            rdbModoConsulta.UseVisualStyleBackColor = true;
            rdbModoConsulta.CheckedChanged += rdbModoConsulta_CheckedChanged;
            // 
            // lstFamilias
            // 
            lstFamilias.FormattingEnabled = true;
            lstFamilias.Location = new Point(678, 159);
            lstFamilias.Name = "lstFamilias";
            lstFamilias.Size = new Size(428, 228);
            lstFamilias.TabIndex = 15;
            lstFamilias.Visible = false;
            lstFamilias.SelectedValueChanged += lstFamilias_SelectedValueChanged;
            // 
            // pnlFamiliaNombre
            // 
            pnlFamiliaNombre.Controls.Add(label4);
            pnlFamiliaNombre.Controls.Add(txtNombreFamilia);
            pnlFamiliaNombre.Location = new Point(46, 183);
            pnlFamiliaNombre.Name = "pnlFamiliaNombre";
            pnlFamiliaNombre.Size = new Size(557, 53);
            pnlFamiliaNombre.TabIndex = 16;
            pnlFamiliaNombre.Visible = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 0);
            label4.Name = "label4";
            label4.Size = new Size(176, 32);
            label4.TabIndex = 0;
            label4.Text = "NombreFamilia";
            // 
            // txtNombreFamilia
            // 
            txtNombreFamilia.Location = new Point(185, 3);
            txtNombreFamilia.Name = "txtNombreFamilia";
            txtNombreFamilia.Size = new Size(364, 39);
            txtNombreFamilia.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(262, 172);
            button1.Name = "button1";
            button1.Size = new Size(202, 74);
            button1.TabIndex = 18;
            button1.Text = "Quitar Familia";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(20, 172);
            button2.Name = "button2";
            button2.Size = new Size(202, 74);
            button2.TabIndex = 17;
            button2.Text = "Agregar Familia";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // pnlAgregarQuitarFamilias
            // 
            pnlAgregarQuitarFamilias.Controls.Add(label5);
            pnlAgregarQuitarFamilias.Controls.Add(cmbFamiliasDisponibles);
            pnlAgregarQuitarFamilias.Controls.Add(button1);
            pnlAgregarQuitarFamilias.Controls.Add(button2);
            pnlAgregarQuitarFamilias.Location = new Point(643, 641);
            pnlAgregarQuitarFamilias.Name = "pnlAgregarQuitarFamilias";
            pnlAgregarQuitarFamilias.Size = new Size(493, 302);
            pnlAgregarQuitarFamilias.TabIndex = 19;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(135, 40);
            label5.Name = "label5";
            label5.Size = new Size(229, 32);
            label5.TabIndex = 20;
            label5.Text = "Familias Disponibles";
            // 
            // cmbFamiliasDisponibles
            // 
            cmbFamiliasDisponibles.FormattingEnabled = true;
            cmbFamiliasDisponibles.Location = new Point(63, 89);
            cmbFamiliasDisponibles.Name = "cmbFamiliasDisponibles";
            cmbFamiliasDisponibles.Size = new Size(375, 40);
            cmbFamiliasDisponibles.TabIndex = 19;
            cmbFamiliasDisponibles.SelectedIndexChanged += cmbFamiliasDisponibles_SelectedIndexChanged;
            // 
            // lstFamiliasFamilia
            // 
            lstFamiliasFamilia.FormattingEnabled = true;
            lstFamiliasFamilia.Location = new Point(101, 619);
            lstFamiliasFamilia.Name = "lstFamiliasFamilia";
            lstFamiliasFamilia.Size = new Size(465, 324);
            lstFamiliasFamilia.TabIndex = 20;
            // 
            // FrmAdminFamilias
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1774, 1185);
            Controls.Add(lstFamiliasFamilia);
            Controls.Add(pnlFamiliaNombre);
            Controls.Add(lstFamilias);
            Controls.Add(rdbModoConsulta);
            Controls.Add(rdbCrearFamilia);
            Controls.Add(rdbModificarFamilia);
            Controls.Add(lstPatentes);
            Controls.Add(LstFamiliasPatentes);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(button5);
            Controls.Add(btnAplicarCambios);
            Controls.Add(btnQuitarPatente);
            Controls.Add(btnAgregarPatente);
            Controls.Add(label1);
            Controls.Add(pnlAgregarQuitarFamilias);
            Name = "FrmAdminFamilias";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmAdminFamilias_90DI";
            Load += FrmAdminRoles_90DI_Load;
            pnlFamiliaNombre.ResumeLayout(false);
            pnlFamiliaNombre.PerformLayout();
            pnlAgregarQuitarFamilias.ResumeLayout(false);
            pnlAgregarQuitarFamilias.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnAgregarPatente;
        private Button btnQuitarPatente;
        private Button btnAplicarCambios;
        private Button button5;
        private Label label2;
        private Label label3;
        private ListBox LstFamiliasPatentes;
        private ListBox lstPatentes;
        private RadioButton rdbModificarFamilia;
        private RadioButton rdbCrearFamilia;
        private RadioButton rdbModoConsulta;
        private ListBox lstFamilias;
        private FlowLayoutPanel pnlFamiliaNombre;
        private Label label4;
        private TextBox txtNombreFamilia;
        private Button button1;
        private Button button2;
        private Panel pnlAgregarQuitarFamilias;
        private ListBox lstFamiliasFamilia;
        private ComboBox cmbFamiliasDisponibles;
        private Label label5;
    }
}