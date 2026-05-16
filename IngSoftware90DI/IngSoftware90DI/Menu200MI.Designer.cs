namespace UI_90DI
{
    partial class Menu200MI
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
            maestroToolStripMenuItem = new ToolStripMenuItem();
            clientesToolStripMenuItem = new ToolStripMenuItem();
            prestamosToolStripMenuItem = new ToolStripMenuItem();
            medicosToolStripMenuItem = new ToolStripMenuItem();
            pacientesToolStripMenuItem = new ToolStripMenuItem();
            reportesToolStripMenuItem = new ToolStripMenuItem();
            historialClienteToolStripMenuItem = new ToolStripMenuItem();
            simulacionPrestamoToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(32, 32);
            menuStrip1.Items.AddRange(new ToolStripItem[] { usuariosToolStripMenuItem, adminToolStripMenuItem, maestroToolStripMenuItem, reportesToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1474, 42);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // usuariosToolStripMenuItem
            // 
            usuariosToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loginToolStripMenuItem, logoutToolStripMenuItem, passwordToolStripMenuItem, idiomaToolStripMenuItem });
            usuariosToolStripMenuItem.Name = "usuariosToolStripMenuItem";
            usuariosToolStripMenuItem.Size = new Size(124, 38);
            usuariosToolStripMenuItem.Text = "Usuarios";
            // 
            // loginToolStripMenuItem
            // 
            loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            loginToolStripMenuItem.Size = new Size(244, 44);
            loginToolStripMenuItem.Text = "Login";
            loginToolStripMenuItem.Click += loginToolStripMenuItem_Click;
            // 
            // logoutToolStripMenuItem
            // 
            logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            logoutToolStripMenuItem.Size = new Size(244, 44);
            logoutToolStripMenuItem.Text = "Logout";
            logoutToolStripMenuItem.Click += logoutToolStripMenuItem_Click;
            // 
            // passwordToolStripMenuItem
            // 
            passwordToolStripMenuItem.Name = "passwordToolStripMenuItem";
            passwordToolStripMenuItem.Size = new Size(244, 44);
            passwordToolStripMenuItem.Text = "Password";
            passwordToolStripMenuItem.Click += passwordToolStripMenuItem_Click;
            // 
            // idiomaToolStripMenuItem
            // 
            idiomaToolStripMenuItem.Name = "idiomaToolStripMenuItem";
            idiomaToolStripMenuItem.Size = new Size(244, 44);
            idiomaToolStripMenuItem.Text = "Idioma";
            // 
            // adminToolStripMenuItem
            // 
            adminToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { gestionUsuariosToolStripMenuItem, bitacoraToolStripMenuItem });
            adminToolStripMenuItem.Name = "adminToolStripMenuItem";
            adminToolStripMenuItem.Size = new Size(104, 38);
            adminToolStripMenuItem.Text = "Admin";
            // 
            // gestionUsuariosToolStripMenuItem
            // 
            gestionUsuariosToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aBMUsuariosToolStripMenuItem });
            gestionUsuariosToolStripMenuItem.Name = "gestionUsuariosToolStripMenuItem";
            gestionUsuariosToolStripMenuItem.Size = new Size(359, 44);
            gestionUsuariosToolStripMenuItem.Text = "Gestion Usuarios";
            // 
            // aBMUsuariosToolStripMenuItem
            // 
            aBMUsuariosToolStripMenuItem.Name = "aBMUsuariosToolStripMenuItem";
            aBMUsuariosToolStripMenuItem.Size = new Size(295, 44);
            aBMUsuariosToolStripMenuItem.Text = "ABM Usuarios";
            aBMUsuariosToolStripMenuItem.Click += aBMUsuariosToolStripMenuItem_Click;
            // 
            // bitacoraToolStripMenuItem
            // 
            bitacoraToolStripMenuItem.Name = "bitacoraToolStripMenuItem";
            bitacoraToolStripMenuItem.Size = new Size(359, 44);
            bitacoraToolStripMenuItem.Text = "Bitacora";
            bitacoraToolStripMenuItem.Click += bitacoraToolStripMenuItem_Click;
            // 
            // maestroToolStripMenuItem
            // 
            maestroToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { clientesToolStripMenuItem, prestamosToolStripMenuItem, medicosToolStripMenuItem, pacientesToolStripMenuItem });
            maestroToolStripMenuItem.Name = "maestroToolStripMenuItem";
            maestroToolStripMenuItem.Size = new Size(121, 38);
            maestroToolStripMenuItem.Text = "Maestro";
            // 
            // clientesToolStripMenuItem
            // 
            clientesToolStripMenuItem.Name = "clientesToolStripMenuItem";
            clientesToolStripMenuItem.Size = new Size(256, 44);
            clientesToolStripMenuItem.Text = "Clientes";
            // 
            // prestamosToolStripMenuItem
            // 
            prestamosToolStripMenuItem.Name = "prestamosToolStripMenuItem";
            prestamosToolStripMenuItem.Size = new Size(256, 44);
            prestamosToolStripMenuItem.Text = "Prestamos";
            // 
            // medicosToolStripMenuItem
            // 
            medicosToolStripMenuItem.Name = "medicosToolStripMenuItem";
            medicosToolStripMenuItem.Size = new Size(256, 44);
            medicosToolStripMenuItem.Text = "Medicos";
            // 
            // pacientesToolStripMenuItem
            // 
            pacientesToolStripMenuItem.Name = "pacientesToolStripMenuItem";
            pacientesToolStripMenuItem.Size = new Size(256, 44);
            pacientesToolStripMenuItem.Text = "Pacientes";
            // 
            // reportesToolStripMenuItem
            // 
            reportesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { historialClienteToolStripMenuItem, simulacionPrestamoToolStripMenuItem });
            reportesToolStripMenuItem.Name = "reportesToolStripMenuItem";
            reportesToolStripMenuItem.Size = new Size(127, 38);
            reportesToolStripMenuItem.Text = "Reportes";
            // 
            // historialClienteToolStripMenuItem
            // 
            historialClienteToolStripMenuItem.Name = "historialClienteToolStripMenuItem";
            historialClienteToolStripMenuItem.Size = new Size(370, 44);
            historialClienteToolStripMenuItem.Text = "Historial Cliente";
            // 
            // simulacionPrestamoToolStripMenuItem
            // 
            simulacionPrestamoToolStripMenuItem.Name = "simulacionPrestamoToolStripMenuItem";
            simulacionPrestamoToolStripMenuItem.Size = new Size(370, 44);
            simulacionPrestamoToolStripMenuItem.Text = "Simulacion Prestamo";
            // 
            // Menu200MI
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1474, 929);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Menu200MI";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Menu200MI";
            FormClosed += Menu200MI_FormClosed;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Button btnReportsNode;
        private Button btnMasterNode;
        private Button btnAdminNode;
        private Button btnUsersNode;
        private Label label1;
        private Panel UserPanel;
        private Button btnCambiarIdioma;
        private Button btnChangePassword;
        private Button btnLogoutMenu;
        private Button btnLoginMenu;
        private Panel panelAdmin;
        private Button btnBitacora;
        private Button btnUnblockUser;
        private Button btnAbmUsers;
        private Button btnGestionUsuarios;
        private Panel panelMaestro;
        private Button button1;
        private Button button6;
        private Panel panelReportes;
        private Button button7;
        private Button button10;
        private Label label2;
        private Button button5;
        private Button button4;
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
    }
}