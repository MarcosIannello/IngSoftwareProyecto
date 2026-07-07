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
            button5 = new Button();
            btnAplicarCambios = new Button();
            label1 = new Label();
            pnlAdminRolesContent = new Panel();
            btnEliminarRol = new Button();
            label3 = new Label();
            cmbRolActual = new ComboBox();
            panel2 = new Panel();
            label6 = new Label();
            cmbFamiliasDisponibles = new ComboBox();
            btnQuitarFamilia = new Button();
            btnAgregarFamilia = new Button();
            LstFamiliasAsignadas = new ListBox();
            pnlFamiliaNombre = new FlowLayoutPanel();
            label4 = new Label();
            txtNombreRol = new TextBox();
            label2 = new Label();
            rdbModoConsulta = new RadioButton();
            rdbCrearFamilia = new RadioButton();
            rdbModificarFamilia = new RadioButton();
            LstPatentesAsignadas = new ListBox();
            pnlAgregarQuitarFamilias = new Panel();
            label5 = new Label();
            cmbPatentesDisponibles = new ComboBox();
            btnQuitarPatente = new Button();
            btnAgregarPatente = new Button();
            pnlAdminRolesContent.SuspendLayout();
            panel2.SuspendLayout();
            pnlFamiliaNombre.SuspendLayout();
            pnlAgregarQuitarFamilias.SuspendLayout();
            SuspendLayout();
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
            // pnlAdminRolesContent
            // 
            pnlAdminRolesContent.Controls.Add(btnEliminarRol);
            pnlAdminRolesContent.Controls.Add(label3);
            pnlAdminRolesContent.Controls.Add(cmbRolActual);
            pnlAdminRolesContent.Controls.Add(panel2);
            pnlAdminRolesContent.Controls.Add(LstFamiliasAsignadas);
            pnlAdminRolesContent.Controls.Add(pnlFamiliaNombre);
            pnlAdminRolesContent.Controls.Add(rdbModoConsulta);
            pnlAdminRolesContent.Controls.Add(rdbCrearFamilia);
            pnlAdminRolesContent.Controls.Add(rdbModificarFamilia);
            pnlAdminRolesContent.Controls.Add(LstPatentesAsignadas);
            pnlAdminRolesContent.Controls.Add(pnlAgregarQuitarFamilias);
            pnlAdminRolesContent.Location = new Point(57, 26);
            pnlAdminRolesContent.Margin = new Padding(2, 2, 2, 2);
            pnlAdminRolesContent.Name = "pnlAdminRolesContent";
            pnlAdminRolesContent.Size = new Size(1182, 451);
            pnlAdminRolesContent.TabIndex = 26;
            // 
            // btnEliminarRol
            // 
            btnEliminarRol.BackColor = Color.Red;
            btnEliminarRol.Cursor = Cursors.Hand;
            btnEliminarRol.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEliminarRol.ForeColor = SystemColors.Control;
            btnEliminarRol.Location = new Point(564, 90);
            btnEliminarRol.Margin = new Padding(2, 1, 2, 1);
            btnEliminarRol.Name = "btnEliminarRol";
            btnEliminarRol.Size = new Size(38, 25);
            btnEliminarRol.TabIndex = 49;
            btnEliminarRol.Text = "x";
            btnEliminarRol.UseVisualStyleBackColor = false;
            btnEliminarRol.Visible = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(392, 46);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(97, 15);
            label3.TabIndex = 48;
            label3.Text = "Rol Seleccionado";
            // 
            // cmbRolActual
            // 
            cmbRolActual.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRolActual.FormattingEnabled = true;
            cmbRolActual.Location = new Point(504, 44);
            cmbRolActual.Margin = new Padding(2, 1, 2, 1);
            cmbRolActual.Name = "cmbRolActual";
            cmbRolActual.Size = new Size(294, 23);
            cmbRolActual.TabIndex = 39;
            // 
            // panel2
            // 
            panel2.Controls.Add(label6);
            panel2.Controls.Add(cmbFamiliasDisponibles);
            panel2.Controls.Add(btnQuitarFamilia);
            panel2.Controls.Add(btnAgregarFamilia);
            panel2.Location = new Point(704, 298);
            panel2.Margin = new Padding(2, 1, 2, 1);
            panel2.Name = "panel2";
            panel2.Size = new Size(265, 142);
            panel2.TabIndex = 46;
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
            cmbFamiliasDisponibles.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFamiliasDisponibles.FormattingEnabled = true;
            cmbFamiliasDisponibles.Location = new Point(34, 42);
            cmbFamiliasDisponibles.Margin = new Padding(2, 1, 2, 1);
            cmbFamiliasDisponibles.Name = "cmbFamiliasDisponibles";
            cmbFamiliasDisponibles.Size = new Size(204, 23);
            cmbFamiliasDisponibles.TabIndex = 19;
            // 
            // btnQuitarFamilia
            // 
            btnQuitarFamilia.BackColor = Color.LightCoral;
            btnQuitarFamilia.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnQuitarFamilia.ForeColor = SystemColors.ButtonHighlight;
            btnQuitarFamilia.Location = new Point(141, 81);
            btnQuitarFamilia.Margin = new Padding(2, 1, 2, 1);
            btnQuitarFamilia.Name = "btnQuitarFamilia";
            btnQuitarFamilia.Size = new Size(109, 35);
            btnQuitarFamilia.TabIndex = 18;
            btnQuitarFamilia.Text = "Quitar ";
            btnQuitarFamilia.UseVisualStyleBackColor = false;
            // 
            // btnAgregarFamilia
            // 
            btnAgregarFamilia.BackColor = Color.DarkSeaGreen;
            btnAgregarFamilia.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAgregarFamilia.ForeColor = SystemColors.ButtonHighlight;
            btnAgregarFamilia.Location = new Point(11, 81);
            btnAgregarFamilia.Margin = new Padding(2, 1, 2, 1);
            btnAgregarFamilia.Name = "btnAgregarFamilia";
            btnAgregarFamilia.Size = new Size(109, 35);
            btnAgregarFamilia.TabIndex = 17;
            btnAgregarFamilia.Text = "Agregar ";
            btnAgregarFamilia.UseVisualStyleBackColor = false;
            // 
            // LstFamiliasAsignadas
            // 
            LstFamiliasAsignadas.FormattingEnabled = true;
            LstFamiliasAsignadas.ItemHeight = 15;
            LstFamiliasAsignadas.Location = new Point(189, 286);
            LstFamiliasAsignadas.Margin = new Padding(2, 1, 2, 1);
            LstFamiliasAsignadas.Name = "LstFamiliasAsignadas";
            LstFamiliasAsignadas.Size = new Size(336, 154);
            LstFamiliasAsignadas.TabIndex = 47;
            // 
            // pnlFamiliaNombre
            // 
            pnlFamiliaNombre.Controls.Add(label4);
            pnlFamiliaNombre.Controls.Add(txtNombreRol);
            pnlFamiliaNombre.Controls.Add(label2);
            pnlFamiliaNombre.Location = new Point(162, 90);
            pnlFamiliaNombre.Margin = new Padding(2, 1, 2, 1);
            pnlFamiliaNombre.Name = "pnlFamiliaNombre";
            pnlFamiliaNombre.Size = new Size(383, 25);
            pnlFamiliaNombre.TabIndex = 44;
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
            rdbModoConsulta.Location = new Point(376, 10);
            rdbModoConsulta.Margin = new Padding(2, 1, 2, 1);
            rdbModoConsulta.Name = "rdbModoConsulta";
            rdbModoConsulta.Size = new Size(107, 19);
            rdbModoConsulta.TabIndex = 43;
            rdbModoConsulta.TabStop = true;
            rdbModoConsulta.Text = "Modo Consulta";
            rdbModoConsulta.UseVisualStyleBackColor = true;
            // 
            // rdbCrearFamilia
            // 
            rdbCrearFamilia.AutoSize = true;
            rdbCrearFamilia.Location = new Point(705, 10);
            rdbCrearFamilia.Margin = new Padding(2, 1, 2, 1);
            rdbCrearFamilia.Name = "rdbCrearFamilia";
            rdbCrearFamilia.Size = new Size(73, 19);
            rdbCrearFamilia.TabIndex = 42;
            rdbCrearFamilia.TabStop = true;
            rdbCrearFamilia.Text = "Crear Rol";
            rdbCrearFamilia.UseVisualStyleBackColor = true;
            // 
            // rdbModificarFamilia
            // 
            rdbModificarFamilia.AutoSize = true;
            rdbModificarFamilia.Location = new Point(535, 10);
            rdbModificarFamilia.Margin = new Padding(2, 1, 2, 1);
            rdbModificarFamilia.Name = "rdbModificarFamilia";
            rdbModificarFamilia.Size = new Size(96, 19);
            rdbModificarFamilia.TabIndex = 41;
            rdbModificarFamilia.TabStop = true;
            rdbModificarFamilia.Text = "Modificar Rol";
            rdbModificarFamilia.UseVisualStyleBackColor = true;
            // 
            // LstPatentesAsignadas
            // 
            LstPatentesAsignadas.FormattingEnabled = true;
            LstPatentesAsignadas.ItemHeight = 15;
            LstPatentesAsignadas.Location = new Point(189, 136);
            LstPatentesAsignadas.Margin = new Padding(2, 1, 2, 1);
            LstPatentesAsignadas.Name = "LstPatentesAsignadas";
            LstPatentesAsignadas.Size = new Size(336, 154);
            LstPatentesAsignadas.TabIndex = 40;
            // 
            // pnlAgregarQuitarFamilias
            // 
            pnlAgregarQuitarFamilias.Controls.Add(label5);
            pnlAgregarQuitarFamilias.Controls.Add(cmbPatentesDisponibles);
            pnlAgregarQuitarFamilias.Controls.Add(btnQuitarPatente);
            pnlAgregarQuitarFamilias.Controls.Add(btnAgregarPatente);
            pnlAgregarQuitarFamilias.Location = new Point(704, 127);
            pnlAgregarQuitarFamilias.Margin = new Padding(2, 1, 2, 1);
            pnlAgregarQuitarFamilias.Name = "pnlAgregarQuitarFamilias";
            pnlAgregarQuitarFamilias.Size = new Size(265, 142);
            pnlAgregarQuitarFamilias.TabIndex = 45;
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
            cmbPatentesDisponibles.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPatentesDisponibles.FormattingEnabled = true;
            cmbPatentesDisponibles.Location = new Point(34, 42);
            cmbPatentesDisponibles.Margin = new Padding(2, 1, 2, 1);
            cmbPatentesDisponibles.Name = "cmbPatentesDisponibles";
            cmbPatentesDisponibles.Size = new Size(204, 23);
            cmbPatentesDisponibles.TabIndex = 19;
            // 
            // btnQuitarPatente
            // 
            btnQuitarPatente.BackColor = Color.LightCoral;
            btnQuitarPatente.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnQuitarPatente.ForeColor = SystemColors.ButtonHighlight;
            btnQuitarPatente.Location = new Point(141, 81);
            btnQuitarPatente.Margin = new Padding(2, 1, 2, 1);
            btnQuitarPatente.Name = "btnQuitarPatente";
            btnQuitarPatente.Size = new Size(109, 35);
            btnQuitarPatente.TabIndex = 18;
            btnQuitarPatente.Text = "Quitar";
            btnQuitarPatente.UseVisualStyleBackColor = false;
            // 
            // btnAgregarPatente
            // 
            btnAgregarPatente.BackColor = Color.DarkSeaGreen;
            btnAgregarPatente.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAgregarPatente.ForeColor = SystemColors.ButtonHighlight;
            btnAgregarPatente.Location = new Point(11, 81);
            btnAgregarPatente.Margin = new Padding(2, 1, 2, 1);
            btnAgregarPatente.Name = "btnAgregarPatente";
            btnAgregarPatente.Size = new Size(109, 35);
            btnAgregarPatente.TabIndex = 17;
            btnAgregarPatente.Text = "Agregar";
            btnAgregarPatente.UseVisualStyleBackColor = false;
            // 
            // FrmAdminROLES_90DI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1032, 484);
            Controls.Add(pnlAdminRolesContent);
            Controls.Add(button5);
            Controls.Add(btnAplicarCambios);
            Controls.Add(label1);
            Margin = new Padding(2, 1, 2, 1);
            Name = "FrmAdminROLES_90DI";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmAdminRoles_90DI";
            WindowState = FormWindowState.Maximized;
            Load += FrmAdminROLES_90DI_Load;
            pnlAdminRolesContent.ResumeLayout(false);
            pnlAdminRolesContent.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            pnlFamiliaNombre.ResumeLayout(false);
            pnlFamiliaNombre.PerformLayout();
            pnlAgregarQuitarFamilias.ResumeLayout(false);
            pnlAgregarQuitarFamilias.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button button5;
        private Button btnAplicarCambios;
        private Label label1;
        private Panel pnlAdminRolesContent;
        private Button btnEliminarRol;
        private Label label3;
        private ComboBox cmbRolActual;
        private Panel panel2;
        private Label label6;
        private ComboBox cmbFamiliasDisponibles;
        private Button btnQuitarFamilia;
        private Button btnAgregarFamilia;
        private ListBox LstFamiliasAsignadas;
        private FlowLayoutPanel pnlFamiliaNombre;
        private Label label4;
        private TextBox txtNombreRol;
        private Label label2;
        private RadioButton rdbModoConsulta;
        private RadioButton rdbCrearFamilia;
        private RadioButton rdbModificarFamilia;
        private ListBox LstPatentesAsignadas;
        private Panel pnlAgregarQuitarFamilias;
        private Label label5;
        private ComboBox cmbPatentesDisponibles;
        private Button btnQuitarPatente;
        private Button btnAgregarPatente;
    }
}