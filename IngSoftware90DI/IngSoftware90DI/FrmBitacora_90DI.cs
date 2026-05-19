using BLL_90DI;
using Entities_90DI;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace UI_90DI
{
    public partial class FrmBitacora_90DI : Form
    {
        private readonly UsersBLL_90DI _usersService = new UsersBLL_90DI();
        private readonly BitacoraBLL_90DI _bll = new BitacoraBLL_90DI();
        private List<Event_90DI> _eventosList = new List<Event_90DI>();
        private readonly FrmMenu_90DI _menu;

        public FrmBitacora_90DI(FrmMenu_90DI menu)
        {
            _menu = menu;
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

        private void RefreshGrid(List<Event_90DI> list)
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
            ExportarPDF90DI();
        }

        // ─── Exportar PDF ─────────────────────────────────────────────────────

        

        private void button1_Click(object sender, EventArgs e)
        {
            _menu.Show();
            this.Close();
        }

        private void CrudUsers_FormClosed(object sender, FormClosedEventArgs e)
        {
            _menu.Show();
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

        Event_90DI selectedItem;
        private void dataGridEvents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            selectedItem = (Event_90DI)dataGridEvents.CurrentRow.DataBoundItem;
            var user = _usersService.getUserByUsername(selectedItem.Login_90DI);

            if (user == null) return;

            txtName.Text = user.Nombre_90DI;
            txtLastName.Text = user.Apellidos_90DI;
        }

        private void ExportarPDF90DI()
        {
            // Obtener datos actuales del grid
            var eventos = dataGridEvents.DataSource as List<Event_90DI>;
            if (eventos == null || eventos.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using var dlg = new SaveFileDialog
            {
                Title = "Guardar Bitacora",
                Filter = "PDF (*.pdf)|*.pdf",
                FileName = $"Bitacora_{DateTime.Now:yyyyMMdd_HHmm}.pdf",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };

            if (dlg.ShowDialog() != DialogResult.OK) return;

            try
            {
                using var writer = new PdfWriter(dlg.FileName);
                using var pdf = new PdfDocument(writer);
                using var doc = new Document(pdf, PageSize.A4.Rotate());

                doc.SetMargins(20, 20, 20, 20);

                // ── Título ──
                doc.Add(new Paragraph("Bitacora de Eventos")
                    .SetFontSize(16)
                    .SetBold()
                    .SetTextAlignment(TextAlignment.CENTER));

                // ── Subtítulo con filtros aplicados ──
                var filtros = $"Periodo: {dtpInitialDate.Value:dd/MM/yyyy} - {dtpEndDate.Value:dd/MM/yyyy}" +
                              $"   |   Registros: {eventos.Count}" +
                              $"   |   Generado: {DateTime.Now:dd/MM/yyyy HH:mm}";
                doc.Add(new Paragraph(filtros)
                    .SetFontSize(9)
                    .SetTextAlignment(TextAlignment.CENTER));

                doc.Add(new Paragraph("\n").SetFontSize(4));

                // ── Tabla ──
                string[] headers = { "ID", "Login", "Fecha", "Hora", "Modulo", "Evento", "Criticidad" };
                float[] widths = { 1f, 2f, 2f, 1.5f, 2f, 4f, 1.5f };

                var table = new Table(UnitValue.CreatePercentArray(widths)).UseAllAvailableWidth();

                // Cabeceras
                var headerBg = new DeviceRgb(52, 73, 94);
                foreach (var h in headers)
                {
                    table.AddHeaderCell(new Cell()
                        .Add(new Paragraph(h).SetBold().SetFontSize(9).SetFontColor(ColorConstants.WHITE))
                        .SetBackgroundColor(headerBg)
                        .SetTextAlignment(TextAlignment.CENTER));
                }

                // Filas
                var rowAlt = new DeviceRgb(235, 240, 245);
                for (int i = 0; i < eventos.Count; i++)
                {
                    var ev = eventos[i];
                    var bg = i % 2 == 0 ? ColorConstants.WHITE : rowAlt;

                    var values = new[]
                    {
                        ev.IdEvento_90DI.ToString(),
                        ev.Login_90DI,
                        ev.Fecha_90DI.ToString("dd/MM/yyyy"),
                        ev.Hora_90DI.ToString(@"hh\:mm\:ss"),
                        ev.Modulo_90DI,
                        ev.Evento_90DI,
                        ev.Criticidad_90DI.ToString()
                    };

                    foreach (var v in values)
                    {
                        table.AddCell(new Cell()
                            .Add(new Paragraph(v ?? "").SetFontSize(8))
                            .SetBackgroundColor(bg));
                    }
                }

                doc.Add(table);

                MessageBox.Show($"PDF exportado correctamente.", "Listo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Abrir el archivo automáticamente
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = dlg.FileName,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el PDF: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
