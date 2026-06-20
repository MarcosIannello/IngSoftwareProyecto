namespace Capital_
{
    partial class FrmAdminROLES_90DI
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
            LstFamiliasAsignadas = new ListBox();
            pnlFamiliaNombre = new FlowLayoutPanel();
            label4 = new Label();
            txtNombreRol = new TextBox();
            label2 = new Label();
            rdbModoConsulta = new RadioButton();
            rdbCrearFamilia = new RadioButton();
            rdbModificarFamilia = new RadioButton();
            LstPatentesAsignadas = new ListBox();
            button5 = new Button();
            btnAplicarCambios = new Button();
            label1 = new Label();
            pnlAgregarQuitarFamilias = new Panel();
            label5 = new Label();
            cmbPatentesDisponibles = new ComboBox();
            btnQuitarPatente = new Button();
            btnAgregarPatente = new Button();
            panel1 = new Panel();
            label6 = new Label();
            cmbFamiliasDisponibles = new ComboBox();
            btnQuitarFamilia = new Button();
            btnAgregarFamilia = new Button();
            cmbRolActual = new ComboBox();
            label3 = new Label();
            btnEliminarRol = new Button();
            pnlFamiliaNombre.SuspendLayout();
            pnlAgregarQuitarFamilias.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // LstFamiliasAsignadas
            // 
            LstFamiliasAsignadas.FormattingEnabled = true;
            LstFamiliasAsignadas.ItemHeight = 15;
            LstFamiliasAsignadas.Location = new Point(426, 434);
            LstFamiliasAsignadas.Margin = new Padding(2, 1, 2, 1);
            LstFamiliasAsignadas.Name = "LstFamiliasAsignadas";
            LstFamiliasAsignadas.Size = new Size(336, 154);
            LstFamiliasAsignadas.TabIndex = 36;
            // 
            // pnlFamiliaNombre
            // 
            pnlFamiliaNombre.Controls.Add(label4);
            pnlFamiliaNombre.Controls.Add(txtNombreRol);
            pnlFamiliaNombre.Controls.Add(label2);
            pnlFamiliaNombre.Location = new Point(400, 238);
            pnlFamiliaNombre.Margin = new Padding(2, 1, 2, 1);
            pnlFamiliaNombre.Name = "pnlFamiliaNombre";
            pnlFamiliaNombre.Size = new Size(383, 25);
            pnlFamiliaNombre.TabIndex = 34;
            pnlFamiliaNombre.Visible = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(2, 0);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(71, 15);
            label4.TabIndex = 0;
            label4.Text = "Nombre Rol";
            // 
            // txtNombreRol
            // 
            txtNombreRol.Location = new Point(77, 1);
            txtNombreRol.Margin = new Padding(2, 1, 2, 1);
            txtNombreRol.Name = "txtNombreRol";
            txtNombreRol.Size = new Size(284, 23);
            txtNombreRol.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(2, 25);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(71, 15);
            label2.TabIndex = 2;
            label2.Text = "Nombre Rol";
            // 
            // rdbModoConsulta
            // 
            rdbModoConsulta.AutoSize = true;
            rdbModoConsulta.Location = new Point(613, 143);
            rdbModoConsulta.Margin = new Padding(2, 1, 2, 1);
            rdbModoConsulta.Name = "rdbModoConsulta";
            rdbModoConsulta.Size = new Size(107, 19);
            rdbModoConsulta.TabIndex = 32;
            rdbModoConsulta.TabStop = true;
            rdbModoConsulta.Text = "Modo Consulta";
            rdbModoConsulta.UseVisualStyleBackColor = true;
            rdbModoConsulta.CheckedChanged += rdbModoConsulta_CheckedChanged;
            // 
            // rdbCrearFamilia
            // 
            rdbCrearFamilia.AutoSize = true;
            rdbCrearFamilia.Location = new Point(942, 143);
            rdbCrearFamilia.Margin = new Padding(2, 1, 2, 1);
            rdbCrearFamilia.Name = "rdbCrearFamilia";
            rdbCrearFamilia.Size = new Size(73, 19);
            rdbCrearFamilia.TabIndex = 31;
            rdbCrearFamilia.TabStop = true;
            rdbCrearFamilia.Text = "Crear Rol";
            rdbCrearFamilia.UseVisualStyleBackColor = true;
            rdbCrearFamilia.CheckedChanged += rdbCrearRol_CheckedChanged;
            // 
            // rdbModificarFamilia
            // 
            rdbModificarFamilia.AutoSize = true;
            rdbModificarFamilia.Location = new Point(772, 143);
            rdbModificarFamilia.Margin = new Padding(2, 1, 2, 1);
            rdbModificarFamilia.Name = "rdbModificarFamilia";
            rdbModificarFamilia.Size = new Size(96, 19);
            rdbModificarFamilia.TabIndex = 30;
            rdbModificarFamilia.TabStop = true;
            rdbModificarFamilia.Text = "Modificar Rol";
            rdbModificarFamilia.UseVisualStyleBackColor = true;
            rdbModificarFamilia.CheckedChanged += rdbModificarRol_CheckedChanged;
            // 
            // LstPatentesAsignadas
            // 
            LstPatentesAsignadas.FormattingEnabled = true;
            LstPatentesAsignadas.ItemHeight = 15;
            LstPatentesAsignadas.Location = new Point(426, 284);
            LstPatentesAsignadas.Margin = new Padding(2, 1, 2, 1);
            LstPatentesAsignadas.Name = "LstPatentesAsignadas";
            LstPatentesAsignadas.Size = new Size(336, 154);
            LstPatentesAsignadas.TabIndex = 28;
            // 
            // button5
            // 
            button5.BackColor = Color.LightCoral;
            button5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button5.ForeColor = SystemColors.ControlLightLight;
            button5.Location = new Point(956, 637);
            button5.Margin = new Padding(2, 1, 2, 1);
            button5.Name = "button5";
            button5.Size = new Size(250, 35);
            button5.TabIndex = 25;
            button5.Text = "Salir";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // btnAplicarCambios
            // 
            btnAplicarCambios.Location = new Point(426, 637);
            btnAplicarCambios.Margin = new Padding(2, 1, 2, 1);
            btnAplicarCambios.Name = "btnAplicarCambios";
            btnAplicarCambios.Size = new Size(334, 35);
            btnAplicarCambios.TabIndex = 24;
            btnAplicarCambios.Text = "Aplicar";
            btnAplicarCambios.UseVisualStyleBackColor = true;
            btnAplicarCambios.Click += btnAplicarCambios_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 9);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(114, 15);
            label1.TabIndex = 21;
            label1.Text = "Administrador Roles";
            // 
            // pnlAgregarQuitarFamilias
            // 
            pnlAgregarQuitarFamilias.Controls.Add(label5);
            pnlAgregarQuitarFamilias.Controls.Add(cmbPatentesDisponibles);
            pnlAgregarQuitarFamilias.Controls.Add(btnQuitarPatente);
            pnlAgregarQuitarFamilias.Controls.Add(btnAgregarPatente);
            pnlAgregarQuitarFamilias.Location = new Point(941, 275);
            pnlAgregarQuitarFamilias.Margin = new Padding(2, 1, 2, 1);
            pnlAgregarQuitarFamilias.Name = "pnlAgregarQuitarFamilias";
            pnlAgregarQuitarFamilias.Size = new Size(265, 142);
            pnlAgregarQuitarFamilias.TabIndex = 35;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(69, 19);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(116, 15);
            label5.TabIndex = 20;
            label5.Text = "Patentes Disponibles";
            // 
            // cmbPatentesDisponibles
            // 
            cmbPatentesDisponibles.FormattingEnabled = true;
            cmbPatentesDisponibles.Location = new Point(34, 42);
            cmbPatentesDisponibles.Margin = new Padding(2, 1, 2, 1);
            cmbPatentesDisponibles.Name = "cmbPatentesDisponibles";
            cmbPatentesDisponibles.Size = new Size(204, 23);
            cmbPatentesDisponibles.TabIndex = 19;
            cmbPatentesDisponibles.SelectedIndexChanged += cmbPatentesDisponibles_SelectedIndexChanged;
            // 
            // btnQuitarPatente
            // 
            btnQuitarPatente.Location = new Point(141, 81);
            btnQuitarPatente.Margin = new Padding(2, 1, 2, 1);
            btnQuitarPatente.Name = "btnQuitarPatente";
            btnQuitarPatente.Size = new Size(109, 35);
            btnQuitarPatente.TabIndex = 18;
            btnQuitarPatente.Text = "Quitar";
            btnQuitarPatente.UseVisualStyleBackColor = true;
            btnQuitarPatente.Click += btnQuitarPatente_Click;
            // 
            // btnAgregarPatente
            // 
            btnAgregarPatente.Location = new Point(11, 81);
            btnAgregarPatente.Margin = new Padding(2, 1, 2, 1);
            btnAgregarPatente.Name = "btnAgregarPatente";
            btnAgregarPatente.Size = new Size(109, 35);
            btnAgregarPatente.TabIndex = 17;
            btnAgregarPatente.Text = "Agregar";
            btnAgregarPatente.UseVisualStyleBackColor = true;
            btnAgregarPatente.Click += btnAgregarPatente_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(label6);
            panel1.Controls.Add(cmbFamiliasDisponibles);
            panel1.Controls.Add(btnQuitarFamilia);
            panel1.Controls.Add(btnAgregarFamilia);
            panel1.Location = new Point(941, 446);
            panel1.Margin = new Padding(2, 1, 2, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(265, 142);
            panel1.TabIndex = 36;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(73, 19);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(114, 15);
            label6.TabIndex = 20;
            label6.Text = "Familias Disponibles";
            // 
            // cmbFamiliasDisponibles
            // 
            cmbFamiliasDisponibles.FormattingEnabled = true;
            cmbFamiliasDisponibles.Location = new Point(34, 42);
            cmbFamiliasDisponibles.Margin = new Padding(2, 1, 2, 1);
            cmbFamiliasDisponibles.Name = "cmbFamiliasDisponibles";
            cmbFamiliasDisponibles.Size = new Size(204, 23);
            cmbFamiliasDisponibles.TabIndex = 19;
            cmbFamiliasDisponibles.SelectedIndexChanged += cmbFamiliasDisponibles_SelectedIndexChanged;
            // 
            // btnQuitarFamilia
            // 
            btnQuitarFamilia.Location = new Point(141, 81);
            btnQuitarFamilia.Margin = new Padding(2, 1, 2, 1);
            btnQuitarFamilia.Name = "btnQuitarFamilia";
            btnQuitarFamilia.Size = new Size(109, 35);
            btnQuitarFamilia.TabIndex = 18;
            btnQuitarFamilia.Text = "Quitar ";
            btnQuitarFamilia.UseVisualStyleBackColor = true;
            btnQuitarFamilia.Click += btnQuitarFamilia_Click;
            // 
            // btnAgregarFamilia
            // 
            btnAgregarFamilia.Location = new Point(11, 81);
            btnAgregarFamilia.Margin = new Padding(2, 1, 2, 1);
            btnAgregarFamilia.Name = "btnAgregarFamilia";
            btnAgregarFamilia.Size = new Size(109, 35);
            btnAgregarFamilia.TabIndex = 17;
            btnAgregarFamilia.Text = "Agregar ";
            btnAgregarFamilia.UseVisualStyleBackColor = true;
            btnAgregarFamilia.Click += btnAgregarFamilia_Click;
            // 
            // cmbRolActual
            // 
            cmbRolActual.FormattingEnabled = true;
            cmbRolActual.Location = new Point(741, 177);
            cmbRolActual.Margin = new Padding(2, 1, 2, 1);
            cmbRolActual.Name = "cmbRolActual";
            cmbRolActual.Size = new Size(294, 23);
            cmbRolActual.TabIndex = 21;
            cmbRolActual.SelectedIndexChanged += cmbRolActual_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(629, 178);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(97, 15);
            label3.TabIndex = 37;
            label3.Text = "Rol Seleccionado";
            // 
            // btnEliminarRol
            // 
            btnEliminarRol.BackColor = Color.Red;
            btnEliminarRol.Cursor = Cursors.Hand;
            btnEliminarRol.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEliminarRol.ForeColor = SystemColors.Control;
            btnEliminarRol.Location = new Point(801, 238);
            btnEliminarRol.Margin = new Padding(2, 1, 2, 1);
            btnEliminarRol.Name = "btnEliminarRol";
            btnEliminarRol.Size = new Size(38, 25);
            btnEliminarRol.TabIndex = 38;
            btnEliminarRol.Text = "x";
            btnEliminarRol.UseVisualStyleBackColor = false;
            btnEliminarRol.Visible = false;
            btnEliminarRol.Click += btnEliminarRol_Click;
            // 
            // FrmAdminROLES_90DI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1786, 803);
            Controls.Add(btnEliminarRol);
            Controls.Add(label3);
            Controls.Add(cmbRolActual);
            Controls.Add(panel1);
            Controls.Add(LstFamiliasAsignadas);
            Controls.Add(pnlFamiliaNombre);
            Controls.Add(rdbModoConsulta);
            Controls.Add(rdbCrearFamilia);
            Controls.Add(rdbModificarFamilia);
            Controls.Add(LstPatentesAsignadas);
            Controls.Add(button5);
            Controls.Add(btnAplicarCambios);
            Controls.Add(label1);
            Controls.Add(pnlAgregarQuitarFamilias);
            Margin = new Padding(2, 1, 2, 1);
            Name = "FrmAdminROLES_90DI";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmAdminRoles_90DI";
            WindowState = FormWindowState.Maximized;
            Load += FrmAdminROLES_90DI_Load;
            pnlFamiliaNombre.ResumeLayout(false);
            pnlFamiliaNombre.PerformLayout();
            pnlAgregarQuitarFamilias.ResumeLayout(false);
            pnlAgregarQuitarFamilias.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox LstFamiliasAsignadas;
        private FlowLayoutPanel pnlFamiliaNombre;
        private Label label4;
        private TextBox txtNombreRol;
        private RadioButton rdbModoConsulta;
        private RadioButton rdbCrearFamilia;
        private RadioButton rdbModificarFamilia;
        private ListBox LstPatentesAsignadas;
        private Button button5;
        private Button btnAplicarCambios;
        private Label label1;
        private Panel pnlAgregarQuitarFamilias;
        private Label label5;
        private ComboBox cmbPatentesDisponibles;
        private Button btnQuitarPatente;
        private Button btnAgregarPatente;
        private Panel panel1;
        private Label label6;
        private ComboBox cmbFamiliasDisponibles;
        private Button btnQuitarFamilia;
        private Button btnAgregarFamilia;
        private Label label2;
        private ComboBox cmbRolActual;
        private Label label3;
        private Button btnEliminarRol;
    }
}