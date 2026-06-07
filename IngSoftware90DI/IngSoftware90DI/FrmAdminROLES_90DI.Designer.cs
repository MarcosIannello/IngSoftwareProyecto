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
            label2 = new Label();
            label3 = new Label();
            pnlFamiliaNombre.SuspendLayout();
            pnlAgregarQuitarFamilias.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // LstFamiliasAsignadas
            // 
            LstFamiliasAsignadas.FormattingEnabled = true;
            LstFamiliasAsignadas.Location = new Point(124, 687);
            LstFamiliasAsignadas.Name = "LstFamiliasAsignadas";
            LstFamiliasAsignadas.Size = new Size(620, 324);
            LstFamiliasAsignadas.TabIndex = 36;
            // 
            // pnlFamiliaNombre
            // 
            pnlFamiliaNombre.Controls.Add(label4);
            pnlFamiliaNombre.Controls.Add(txtNombreRol);
            pnlFamiliaNombre.Controls.Add(label2);
            pnlFamiliaNombre.Location = new Point(76, 269);
            pnlFamiliaNombre.Name = "pnlFamiliaNombre";
            pnlFamiliaNombre.Size = new Size(712, 53);
            pnlFamiliaNombre.TabIndex = 34;
            pnlFamiliaNombre.Visible = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 0);
            label4.Name = "label4";
            label4.Size = new Size(142, 32);
            label4.TabIndex = 0;
            label4.Text = "Nombre Rol";
            // 
            // txtNombreRol
            // 
            txtNombreRol.Location = new Point(151, 3);
            txtNombreRol.Name = "txtNombreRol";
            txtNombreRol.Size = new Size(524, 39);
            txtNombreRol.TabIndex = 1;
            // 
            // rdbModoConsulta
            // 
            rdbModoConsulta.AutoSize = true;
            rdbModoConsulta.Location = new Point(506, 70);
            rdbModoConsulta.Name = "rdbModoConsulta";
            rdbModoConsulta.Size = new Size(209, 36);
            rdbModoConsulta.TabIndex = 32;
            rdbModoConsulta.TabStop = true;
            rdbModoConsulta.Text = "Modo Consulta";
            rdbModoConsulta.UseVisualStyleBackColor = true;
            rdbModoConsulta.CheckedChanged += rdbModoConsulta_CheckedChanged;
            //
            // rdbCrearFamilia
            //
            rdbCrearFamilia.AutoSize = true;
            rdbCrearFamilia.Location = new Point(1117, 70);
            rdbCrearFamilia.Name = "rdbCrearFamilia";
            rdbCrearFamilia.Size = new Size(141, 36);
            rdbCrearFamilia.TabIndex = 31;
            rdbCrearFamilia.TabStop = true;
            rdbCrearFamilia.Text = "Crear Rol";
            rdbCrearFamilia.UseVisualStyleBackColor = true;
            rdbCrearFamilia.CheckedChanged += rdbCrearRol_CheckedChanged;
            //
            // rdbModificarFamilia
            //
            rdbModificarFamilia.AutoSize = true;
            rdbModificarFamilia.Location = new Point(800, 70);
            rdbModificarFamilia.Name = "rdbModificarFamilia";
            rdbModificarFamilia.Size = new Size(186, 36);
            rdbModificarFamilia.TabIndex = 30;
            rdbModificarFamilia.TabStop = true;
            rdbModificarFamilia.Text = "Modificar Rol";
            rdbModificarFamilia.UseVisualStyleBackColor = true;
            rdbModificarFamilia.CheckedChanged += rdbModificarRol_CheckedChanged;
            //
            // LstPatentesAsignadas
            // 
            LstPatentesAsignadas.FormattingEnabled = true;
            LstPatentesAsignadas.Location = new Point(124, 367);
            LstPatentesAsignadas.Name = "LstPatentesAsignadas";
            LstPatentesAsignadas.Size = new Size(620, 324);
            LstPatentesAsignadas.TabIndex = 28;
            // 
            // button5
            // 
            button5.Location = new Point(1108, 1119);
            button5.Name = "button5";
            button5.Size = new Size(465, 74);
            button5.TabIndex = 25;
            button5.Text = "Salir";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // btnAplicarCambios
            // 
            btnAplicarCambios.Location = new Point(124, 1119);
            btnAplicarCambios.Name = "btnAplicarCambios";
            btnAplicarCambios.Size = new Size(620, 74);
            btnAplicarCambios.TabIndex = 24;
            btnAplicarCambios.Text = "Aplicar";
            btnAplicarCambios.UseVisualStyleBackColor = true;
            btnAplicarCambios.Click += btnAplicarCambios_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(35, 29);
            label1.Name = "label1";
            label1.Size = new Size(227, 32);
            label1.TabIndex = 21;
            label1.Text = "Administrador Roles";
            // 
            // pnlAgregarQuitarFamilias
            // 
            pnlAgregarQuitarFamilias.Controls.Add(label5);
            pnlAgregarQuitarFamilias.Controls.Add(cmbPatentesDisponibles);
            pnlAgregarQuitarFamilias.Controls.Add(btnQuitarPatente);
            pnlAgregarQuitarFamilias.Controls.Add(btnAgregarPatente);
            pnlAgregarQuitarFamilias.Location = new Point(1080, 348);
            pnlAgregarQuitarFamilias.Name = "pnlAgregarQuitarFamilias";
            pnlAgregarQuitarFamilias.Size = new Size(493, 302);
            pnlAgregarQuitarFamilias.TabIndex = 35;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(129, 40);
            label5.Name = "label5";
            label5.Size = new Size(235, 32);
            label5.TabIndex = 20;
            label5.Text = "Patentes Disponibles";
            // 
            // cmbPatentesDisponibles
            // 
            cmbPatentesDisponibles.FormattingEnabled = true;
            cmbPatentesDisponibles.Location = new Point(63, 89);
            cmbPatentesDisponibles.Name = "cmbPatentesDisponibles";
            cmbPatentesDisponibles.Size = new Size(375, 40);
            cmbPatentesDisponibles.TabIndex = 19;
            cmbPatentesDisponibles.SelectedIndexChanged += cmbPatentesDisponibles_SelectedIndexChanged;
            //
            // btnQuitarPatente
            // 
            btnQuitarPatente.Location = new Point(262, 172);
            btnQuitarPatente.Name = "btnQuitarPatente";
            btnQuitarPatente.Size = new Size(202, 74);
            btnQuitarPatente.TabIndex = 18;
            btnQuitarPatente.Text = "Quitar";
            btnQuitarPatente.UseVisualStyleBackColor = true;
            btnQuitarPatente.Click += btnQuitarPatente_Click;
            // 
            // btnAgregarPatente
            // 
            btnAgregarPatente.Location = new Point(20, 172);
            btnAgregarPatente.Name = "btnAgregarPatente";
            btnAgregarPatente.Size = new Size(202, 74);
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
            panel1.Location = new Point(1080, 713);
            panel1.Name = "panel1";
            panel1.Size = new Size(493, 302);
            panel1.TabIndex = 36;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(135, 40);
            label6.Name = "label6";
            label6.Size = new Size(229, 32);
            label6.TabIndex = 20;
            label6.Text = "Familias Disponibles";
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
            // btnQuitarFamilia
            // 
            btnQuitarFamilia.Location = new Point(262, 172);
            btnQuitarFamilia.Name = "btnQuitarFamilia";
            btnQuitarFamilia.Size = new Size(202, 74);
            btnQuitarFamilia.TabIndex = 18;
            btnQuitarFamilia.Text = "Quitar ";
            btnQuitarFamilia.UseVisualStyleBackColor = true;
            btnQuitarFamilia.Click += btnQuitarFamilia_Click;
            // 
            // btnAgregarFamilia
            // 
            btnAgregarFamilia.Location = new Point(20, 172);
            btnAgregarFamilia.Name = "btnAgregarFamilia";
            btnAgregarFamilia.Size = new Size(202, 74);
            btnAgregarFamilia.TabIndex = 17;
            btnAgregarFamilia.Text = "Agregar ";
            btnAgregarFamilia.UseVisualStyleBackColor = true;
            btnAgregarFamilia.Click += btnAgregarFamilia_Click;
            // 
            // cmbRolActual
            // 
            cmbRolActual.FormattingEnabled = true;
            cmbRolActual.Location = new Point(743, 142);
            cmbRolActual.Name = "cmbRolActual";
            cmbRolActual.Size = new Size(543, 40);
            cmbRolActual.TabIndex = 21;
            cmbRolActual.SelectedIndexChanged += cmbRolActual_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 45);
            label2.Name = "label2";
            label2.Size = new Size(142, 32);
            label2.TabIndex = 2;
            label2.Text = "Nombre Rol";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(534, 145);
            label3.Name = "label3";
            label3.Size = new Size(195, 32);
            label3.TabIndex = 37;
            label3.Text = "Rol Seleccionado";
            // 
            // FrmAdminROLES_90DI
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1796, 1269);
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
            Name = "FrmAdminROLES_90DI";
            Text = "FrmAdminFamilias_90DI";
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
    }
}