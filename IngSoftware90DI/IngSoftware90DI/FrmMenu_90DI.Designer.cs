namespace UI_90DI
{
    partial class FrmMenu_90DI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMenu_90DI));
            menuStrip1 = new MenuStrip();
            usuariosToolStripMenuItem = new ToolStripMenuItem();
            loginToolStripMenuItem = new ToolStripMenuItem();
            logoutToolStripMenuItem = new ToolStripMenuItem();
            passwordToolStripMenuItem = new ToolStripMenuItem();
            idiomaToolStripMenuItem = new ToolStripMenuItem();
            adminToolStripMenuItem = new ToolStripMenuItem();
            gestionUsuariosToolStripMenuItem = new ToolStripMenuItem();
            aBMUsuariosToolStripMenuItem = new ToolStripMenuItem();
            bitacoraToolStripMenuItem = new ToolStripMenuItem();
            adminFamiliaToolStripMenuItem = new ToolStripMenuItem();
            adminRolesToolStripMenuItem = new ToolStripMenuItem();
            backupToolStripMenuItem = new ToolStripMenuItem();
            maestroToolStripMenuItem = new ToolStripMenuItem();
            clientesToolStripMenuItem = new ToolStripMenuItem();
            prestamosToolStripMenuItem = new ToolStripMenuItem();
            medicosToolStripMenuItem = new ToolStripMenuItem();
            pacientesToolStripMenuItem = new ToolStripMenuItem();
            reportesToolStripMenuItem = new ToolStripMenuItem();
            historialClienteToolStripMenuItem = new ToolStripMenuItem();
            simulacionPrestamoToolStripMenuItem = new ToolStripMenuItem();
            pictureBox1 = new PictureBox();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.Maroon;
            menuStrip1.Font = new Font("Segoe UI Black", 10.125F, FontStyle.Bold, GraphicsUnit.Point, 0);
            menuStrip1.ImageScalingSize = new Size(32, 32);
            menuStrip1.Items.AddRange(new ToolStripItem[] { usuariosToolStripMenuItem, adminToolStripMenuItem, maestroToolStripMenuItem, reportesToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(3, 1, 0, 1);
            menuStrip1.Size = new Size(794, 25);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // usuariosToolStripMenuItem
            // 
            usuariosToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loginToolStripMenuItem, logoutToolStripMenuItem, passwordToolStripMenuItem, idiomaToolStripMenuItem });
            usuariosToolStripMenuItem.ForeColor = SystemColors.Control;
            usuariosToolStripMenuItem.Name = "usuariosToolStripMenuItem";
            usuariosToolStripMenuItem.Size = new Size(81, 23);
            usuariosToolStripMenuItem.Text = "Usuarios";
            // 
            // loginToolStripMenuItem
            // 
            loginToolStripMenuItem.BackColor = Color.Maroon;
            loginToolStripMenuItem.ForeColor = SystemColors.Control;
            loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            loginToolStripMenuItem.Size = new Size(180, 24);
            loginToolStripMenuItem.Text = "Login";
            loginToolStripMenuItem.Click += loginToolStripMenuItem_Click;
            // 
            // logoutToolStripMenuItem
            // 
            logoutToolStripMenuItem.BackColor = Color.Maroon;
            logoutToolStripMenuItem.ForeColor = SystemColors.Control;
            logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            logoutToolStripMenuItem.Size = new Size(180, 24);
            logoutToolStripMenuItem.Text = "Logout";
            logoutToolStripMenuItem.Click += logoutToolStripMenuItem_Click;
            // 
            // passwordToolStripMenuItem
            // 
            passwordToolStripMenuItem.BackColor = Color.Maroon;
            passwordToolStripMenuItem.ForeColor = SystemColors.Control;
            passwordToolStripMenuItem.Name = "passwordToolStripMenuItem";
            passwordToolStripMenuItem.Size = new Size(180, 24);
            passwordToolStripMenuItem.Text = "Password";
            passwordToolStripMenuItem.Click += passwordToolStripMenuItem_Click;
            // 
            // idiomaToolStripMenuItem
            // 
            idiomaToolStripMenuItem.BackColor = Color.Maroon;
            idiomaToolStripMenuItem.ForeColor = SystemColors.Control;
            idiomaToolStripMenuItem.Name = "idiomaToolStripMenuItem";
            idiomaToolStripMenuItem.Size = new Size(180, 24);
            idiomaToolStripMenuItem.Text = "Idioma";
            idiomaToolStripMenuItem.Click += idiomaToolStripMenuItem_Click_1;
            // 
            // adminToolStripMenuItem
            // 
            adminToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { gestionUsuariosToolStripMenuItem, bitacoraToolStripMenuItem, adminFamiliaToolStripMenuItem, adminRolesToolStripMenuItem, backupToolStripMenuItem });
            adminToolStripMenuItem.ForeColor = SystemColors.Control;
            adminToolStripMenuItem.Name = "adminToolStripMenuItem";
            adminToolStripMenuItem.Size = new Size(67, 23);
            adminToolStripMenuItem.Text = "Admin";
            // 
            // gestionUsuariosToolStripMenuItem
            // 
            gestionUsuariosToolStripMenuItem.BackColor = Color.Maroon;
            gestionUsuariosToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aBMUsuariosToolStripMenuItem });
            gestionUsuariosToolStripMenuItem.ForeColor = SystemColors.Control;
            gestionUsuariosToolStripMenuItem.Name = "gestionUsuariosToolStripMenuItem";
            gestionUsuariosToolStripMenuItem.Size = new Size(195, 24);
            gestionUsuariosToolStripMenuItem.Text = "Gestion Usuarios";
            // 
            // aBMUsuariosToolStripMenuItem
            // 
            aBMUsuariosToolStripMenuItem.BackColor = Color.Maroon;
            aBMUsuariosToolStripMenuItem.ForeColor = SystemColors.Control;
            aBMUsuariosToolStripMenuItem.Name = "aBMUsuariosToolStripMenuItem";
            aBMUsuariosToolStripMenuItem.Size = new Size(177, 24);
            aBMUsuariosToolStripMenuItem.Text = "ABM Usuarios";
            aBMUsuariosToolStripMenuItem.Click += aBMUsuariosToolStripMenuItem_Click;
            // 
            // bitacoraToolStripMenuItem
            // 
            bitacoraToolStripMenuItem.BackColor = Color.Maroon;
            bitacoraToolStripMenuItem.ForeColor = SystemColors.Control;
            bitacoraToolStripMenuItem.Name = "bitacoraToolStripMenuItem";
            bitacoraToolStripMenuItem.Size = new Size(195, 24);
            bitacoraToolStripMenuItem.Text = "Bitacora";
            bitacoraToolStripMenuItem.Click += bitacoraToolStripMenuItem_Click;
            // 
            // adminFamiliaToolStripMenuItem
            // 
            adminFamiliaToolStripMenuItem.BackColor = Color.Maroon;
            adminFamiliaToolStripMenuItem.ForeColor = SystemColors.ButtonFace;
            adminFamiliaToolStripMenuItem.Name = "adminFamiliaToolStripMenuItem";
            adminFamiliaToolStripMenuItem.Size = new Size(195, 24);
            adminFamiliaToolStripMenuItem.Text = "Admin Familia";
            adminFamiliaToolStripMenuItem.Click += adminFamiliaToolStripMenuItem_Click;
            // 
            // adminRolesToolStripMenuItem
            // 
            adminRolesToolStripMenuItem.BackColor = Color.Maroon;
            adminRolesToolStripMenuItem.ForeColor = SystemColors.ButtonFace;
            adminRolesToolStripMenuItem.Name = "adminRolesToolStripMenuItem";
            adminRolesToolStripMenuItem.Size = new Size(195, 24);
            adminRolesToolStripMenuItem.Text = "Admin Roles";
            adminRolesToolStripMenuItem.Click += adminRolesToolStripMenuItem_Click;
            //
            // backupToolStripMenuItem
            //
            backupToolStripMenuItem.BackColor = Color.Maroon;
            backupToolStripMenuItem.ForeColor = SystemColors.ButtonFace;
            backupToolStripMenuItem.Name = "backupToolStripMenuItem";
            backupToolStripMenuItem.Size = new Size(195, 24);
            backupToolStripMenuItem.Text = "Backup BD";
            backupToolStripMenuItem.Click += backupToolStripMenuItem_Click;
            //
            // maestroToolStripMenuItem
            // 
            maestroToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { clientesToolStripMenuItem, prestamosToolStripMenuItem, medicosToolStripMenuItem, pacientesToolStripMenuItem });
            maestroToolStripMenuItem.ForeColor = SystemColors.Control;
            maestroToolStripMenuItem.Name = "maestroToolStripMenuItem";
            maestroToolStripMenuItem.Size = new Size(79, 23);
            maestroToolStripMenuItem.Text = "Maestro";
            // 
            // clientesToolStripMenuItem
            // 
            clientesToolStripMenuItem.BackColor = Color.Maroon;
            clientesToolStripMenuItem.ForeColor = SystemColors.Control;
            clientesToolStripMenuItem.Name = "clientesToolStripMenuItem";
            clientesToolStripMenuItem.Size = new Size(151, 24);
            clientesToolStripMenuItem.Text = "Clientes";
            // 
            // prestamosToolStripMenuItem
            // 
            prestamosToolStripMenuItem.BackColor = Color.Maroon;
            prestamosToolStripMenuItem.ForeColor = SystemColors.Control;
            prestamosToolStripMenuItem.Name = "prestamosToolStripMenuItem";
            prestamosToolStripMenuItem.Size = new Size(151, 24);
            prestamosToolStripMenuItem.Text = "Prestamos";
            // 
            // medicosToolStripMenuItem
            // 
            medicosToolStripMenuItem.BackColor = Color.Maroon;
            medicosToolStripMenuItem.ForeColor = SystemColors.Control;
            medicosToolStripMenuItem.Name = "medicosToolStripMenuItem";
            medicosToolStripMenuItem.Size = new Size(151, 24);
            medicosToolStripMenuItem.Text = "Medicos";
            // 
            // pacientesToolStripMenuItem
            // 
            pacientesToolStripMenuItem.BackColor = Color.Maroon;
            pacientesToolStripMenuItem.ForeColor = SystemColors.Control;
            pacientesToolStripMenuItem.Name = "pacientesToolStripMenuItem";
            pacientesToolStripMenuItem.Size = new Size(151, 24);
            pacientesToolStripMenuItem.Text = "Pacientes";
            // 
            // reportesToolStripMenuItem
            // 
            reportesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { historialClienteToolStripMenuItem, simulacionPrestamoToolStripMenuItem });
            reportesToolStripMenuItem.ForeColor = SystemColors.Control;
            reportesToolStripMenuItem.Name = "reportesToolStripMenuItem";
            reportesToolStripMenuItem.Size = new Size(84, 23);
            reportesToolStripMenuItem.Text = "Reportes";
            // 
            // historialClienteToolStripMenuItem
            // 
            historialClienteToolStripMenuItem.BackColor = Color.Maroon;
            historialClienteToolStripMenuItem.ForeColor = SystemColors.Control;
            historialClienteToolStripMenuItem.Name = "historialClienteToolStripMenuItem";
            historialClienteToolStripMenuItem.Size = new Size(223, 24);
            historialClienteToolStripMenuItem.Text = "Historial Cliente";
            // 
            // simulacionPrestamoToolStripMenuItem
            // 
            simulacionPrestamoToolStripMenuItem.BackColor = Color.Maroon;
            simulacionPrestamoToolStripMenuItem.ForeColor = SystemColors.Control;
            simulacionPrestamoToolStripMenuItem.Name = "simulacionPrestamoToolStripMenuItem";
            simulacionPrestamoToolStripMenuItem.Size = new Size(223, 24);
            simulacionPrestamoToolStripMenuItem.Text = "Simulacion Prestamo";
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 25);
            pictureBox1.Margin = new Padding(2, 1, 2, 1);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(794, 399);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // FrmMenu_90DI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(794, 424);
            Controls.Add(pictureBox1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(2, 1, 2, 1);
            Name = "FrmMenu_90DI";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Capital+";
            FormClosed += FrmMenu_90DI_FormClosed;
            Load += FrmMenu_90DI_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem usuariosToolStripMenuItem;
        private ToolStripMenuItem loginToolStripMenuItem;
        private ToolStripMenuItem logoutToolStripMenuItem;
        private ToolStripMenuItem passwordToolStripMenuItem;
        private ToolStripMenuItem idiomaToolStripMenuItem;
        private ToolStripMenuItem adminToolStripMenuItem;
        private ToolStripMenuItem gestionUsuariosToolStripMenuItem;
        private ToolStripMenuItem aBMUsuariosToolStripMenuItem;
        private ToolStripMenuItem bitacoraToolStripMenuItem;
        private ToolStripMenuItem maestroToolStripMenuItem;
        private ToolStripMenuItem clientesToolStripMenuItem;
        private ToolStripMenuItem prestamosToolStripMenuItem;
        private ToolStripMenuItem medicosToolStripMenuItem;
        private ToolStripMenuItem pacientesToolStripMenuItem;
        private ToolStripMenuItem reportesToolStripMenuItem;
        private ToolStripMenuItem historialClienteToolStripMenuItem;
        private ToolStripMenuItem simulacionPrestamoToolStripMenuItem;
        private PictureBox pictureBox1;
        private ToolStripMenuItem adminFamiliaToolStripMenuItem;
        private ToolStripMenuItem adminRolesToolStripMenuItem;
        private ToolStripMenuItem backupToolStripMenuItem;
    }
}