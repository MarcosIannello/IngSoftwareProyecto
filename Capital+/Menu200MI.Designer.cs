namespace Capital_
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
            loginLogoutToolStripMenuItem = new ToolStripMenuItem();
            loginToolStripMenuItem = new ToolStripMenuItem();
            logoutToolStripMenuItem = new ToolStripMenuItem();
            nuevoPrestamoToolStripMenuItem = new ToolStripMenuItem();
            pagoPrestamoToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(32, 32);
            menuStrip1.Items.AddRange(new ToolStripItem[] { loginLogoutToolStripMenuItem, nuevoPrestamoToolStripMenuItem, pagoPrestamoToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 42);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // loginLogoutToolStripMenuItem
            // 
            loginLogoutToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loginToolStripMenuItem, logoutToolStripMenuItem });
            loginLogoutToolStripMenuItem.Name = "loginLogoutToolStripMenuItem";
            loginLogoutToolStripMenuItem.Size = new Size(191, 38);
            loginLogoutToolStripMenuItem.Text = "Login / Logout";
            // 
            // loginToolStripMenuItem
            // 
            loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            loginToolStripMenuItem.Size = new Size(359, 44);
            loginToolStripMenuItem.Text = "Login";
            loginToolStripMenuItem.Click += loginToolStripMenuItem_Click;
            // 
            // logoutToolStripMenuItem
            // 
            logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            logoutToolStripMenuItem.Size = new Size(359, 44);
            logoutToolStripMenuItem.Text = "Logout";
            logoutToolStripMenuItem.Click += logoutToolStripMenuItem_Click;
            // 
            // nuevoPrestamoToolStripMenuItem
            // 
            nuevoPrestamoToolStripMenuItem.Name = "nuevoPrestamoToolStripMenuItem";
            nuevoPrestamoToolStripMenuItem.Size = new Size(211, 38);
            nuevoPrestamoToolStripMenuItem.Text = "Nuevo Prestamo";
            // 
            // pagoPrestamoToolStripMenuItem
            // 
            pagoPrestamoToolStripMenuItem.Name = "pagoPrestamoToolStripMenuItem";
            pagoPrestamoToolStripMenuItem.Size = new Size(192, 38);
            pagoPrestamoToolStripMenuItem.Text = "Pago Prestamo";
            // 
            // Menu200MI
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Menu200MI";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Menu200MI";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem loginLogoutToolStripMenuItem;
        private ToolStripMenuItem loginToolStripMenuItem;
        private ToolStripMenuItem logoutToolStripMenuItem;
        private ToolStripMenuItem nuevoPrestamoToolStripMenuItem;
        private ToolStripMenuItem pagoPrestamoToolStripMenuItem;
    }
}