using BLL_90DI;
using Entities_90DI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace UI_90DI
{
    public partial class Bitacora : Form
    {
        private readonly UsersService90DI _usersService = new UsersService90DI();
        private readonly BitacoraBLL_90DI _bll = new BitacoraBLL_90DI();
        private List<LogEvent_90DI> _eventosList = new List<LogEvent_90DI>();

        public Bitacora()
        {
            InitializeComponent();
            ConfigurarCombos();
            CargarEventos();
        }

        // ─── Configuración ────────────────────────────────────────────────────

        private void ConfigurarCombos()
        {
            // Criticidad 1 (más importante) a 5 (menos importante)
            cmbLevel.Items.Clear();
            cmbLevel.Items.Add("");
            for (int i = 1; i <= 5; i++) cmbLevel.Items.Add(i.ToString());
            cmbLevel.SelectedIndex = 0;

            // Módulos tipificados
            cmbModule.Items.Clear();
            cmbModule.Items.AddRange(new[] { "", "Usuarios", "Ventas", "Compras", "Maestro", "Perfiles", "Seguridad" });
            cmbModule.SelectedIndex = 0;

            // Eventos tipificados
            cmdEvent.Items.Clear();
            cmdEvent.Items.AddRange(new[] { "", "Login", "Logout", "Crear Usuario", "Cambiar Clave",
                                            "Bloquear Usuario", "Activar Usuario", "Desactivar Usuario",
                                            "Registrar Cliente", "Generar Carrito", "Eliminar Producto",
                                            "Imprimir Factura" });
            cmdEvent.SelectedIndex = 0;

            // Fechas: últimos 3 días por defecto
            dtpInitialDate.Value = DateTime.Now.AddDays(-3);
            dtpEndDate.Value = DateTime.Now;

            // Display fields: solo lectura
            txtName.ReadOnly = true;
            txtLastName.ReadOnly = true;
        }

        // ─── Carga y refresco ─────────────────────────────────────────────────

        private void CargarEventos()
        {
            _eventosList = _bll.GetLogEvent_90DI();

            // Por defecto: últimos 3 días
            var ultimos3dias = _eventosList
                .Where(e => e.Fecha_90DI.Date >= DateTime.Now.AddDays(-3).Date)
                .ToList();

            RefreshGrid(ultimos3dias);
        }

        private void RefreshGrid(List<LogEvent_90DI> list)
        {
            dataGridEvents.DataSource = list;
            lblCantUsers.Text = "BITACORA EVENTOS  |  Registros: " + list.Count;
        }

        // ─── Filtro ───────────────────────────────────────────────────────────

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            var filtered = _eventosList.AsEnumerable();

            // Rango de fechas (siempre activo)
            filtered = filtered.Where(ev =>
                ev.Fecha_90DI.Date >= dtpInitialDate.Value.Date &&
                ev.Fecha_90DI.Date <= dtpEndDate.Value.Date);

            // Login
            if (!string.IsNullOrWhiteSpace(txtDni.Text))
                filtered = filtered.Where(ev => ev.Login_90DI.Contains(txtDni.Text.Trim(), StringComparison.OrdinalIgnoreCase));

            // Modulo
            if (cmbModule.SelectedIndex > 0)
                filtered = filtered.Where(ev => ev.Modulo_90DI.Equals(cmbModule.SelectedItem?.ToString(), StringComparison.OrdinalIgnoreCase));

            // Evento
            if (cmdEvent.SelectedIndex > 0)
                filtered = filtered.Where(ev => ev.Evento_90DI.Equals(cmdEvent.SelectedItem?.ToString(), StringComparison.OrdinalIgnoreCase));

            // Criticidad
            if (cmbLevel.SelectedIndex > 0 && byte.TryParse(cmbLevel.SelectedItem?.ToString(), out byte crit))
                filtered = filtered.Where(ev => ev.Criticidad_90DI == crit);

            RefreshGrid(filtered.ToList());
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var menu = new Menu200MI();
            this.Close();
            menu.Show();
        }

        private void CrudUsers_FormClosed(object sender, FormClosedEventArgs e)
        {
            var menu = new Menu200MI();
            this.Hide();
            menu.Show();
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            LimpiarFiltros();
        }

        public void LimpiarFiltros()
        {
            // Limpiar filtros y recargar últimos 3 días
            txtDni.Text = "";
            cmbModule.SelectedIndex = 0;
            cmdEvent.SelectedIndex = 0;
            cmbLevel.SelectedIndex = 0;
            dtpInitialDate.Value = DateTime.Now.AddDays(-3);
            dtpEndDate.Value = DateTime.Now;
            txtName.Text = "";
            txtLastName.Text = "";

            var ultimos3dias = _eventosList
                .Where(e => e.Fecha_90DI.Date >= DateTime.Now.AddDays(-3).Date)
                .ToList();
            RefreshGrid(ultimos3dias);
        }

        
        private void dataGridEvents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            

        }

        LogEvent_90DI selectedItem;
        private void dataGridEvents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            selectedItem = (LogEvent_90DI)dataGridEvents.CurrentRow.DataBoundItem;
            var user = _usersService.getUserByUsername(selectedItem.Login_90DI);

            if (user == null) return;

            txtName.Text = user.Nombre_90DI;
            txtLastName.Text = user.Apellidos_90DI;
        }
    }
}
