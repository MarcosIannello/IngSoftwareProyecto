using BLL;
using Services_90DI;
using Services_90DI.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Capital_
{
    public partial class FrmAdminROLES_90DI : Form, IObserver_90DI
    {
        private readonly RolBLL_90DI _rolBLL = new RolBLL_90DI();
        private readonly string _dm = "Nombre_90DI";

        private Rol_90DI tempRol = new Rol_90DI();
        private Patente_90DI tempPatente = new Patente_90DI();
        private Familia_90DI tempFamilia = new Familia_90DI();

        public FrmAdminROLES_90DI()
        {
            InitializeComponent();
            rdbModoConsulta.Checked = true;

            LanguageManager_90DI.Current.AddObserver_90DI(this);
            var t = LanguageManager_90DI.Current.TraduccionesActuales_90DI;
            if (t.Count > 0) UpdateLanguage_90DI(t);
        }

        public void UpdateLanguage_90DI(Dictionary<string, string> traducciones)
        {
            if (traducciones.TryGetValue("roles_title",                    out var v)) label1.Text                   = v;
            if (traducciones.TryGetValue("roles_lbl_nombre",               out v))     label4.Text                   = v;
            if (traducciones.TryGetValue("roles_lbl_sel_rol",              out v))     label3.Text                   = v;
            if (traducciones.TryGetValue("roles_lbl_patentes_disponibles", out v))     label5.Text                   = v;
            if (traducciones.TryGetValue("roles_lbl_familias_disponibles", out v))     label6.Text                   = v;
            if (traducciones.TryGetValue("roles_btn_aplicar",              out v))     btnAplicarCambios.Text        = v;
            if (traducciones.TryGetValue("roles_btn_salir",                out v))     button5.Text                  = v;
            if (traducciones.TryGetValue("roles_btn_agregar",              out v))     { btnAgregarPatente.Text = v; btnAgregarFamilia.Text = v; }
            if (traducciones.TryGetValue("roles_btn_quitar",               out v))     { btnQuitarPatente.Text  = v; btnQuitarFamilia.Text  = v; }
            if (traducciones.TryGetValue("roles_rdb_consulta",             out v))     rdbModoConsulta.Text          = v;
            if (traducciones.TryGetValue("roles_rdb_crear",                out v))     rdbCrearFamilia.Text          = v;
            if (traducciones.TryGetValue("roles_rdb_modificar",            out v))     rdbModificarFamilia.Text      = v;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            LanguageManager_90DI.Current.Unsubscribe_90DI(this);
            base.OnFormClosed(e);
        }

        // ── Modos ─────────────────────────────────────────────────────────────

        private void LoadModoConsulta()
        {
            EstadoInicial();
            SetBotonesAsignacion(false);
            btnAplicarCambios.Enabled = false;
            pnlFamiliaNombre.Visible = false;
            btnEliminarRol.Visible = false;

            CargarCmbRoles();
        }

        private void LoadModoCreacion()
        {
            EstadoInicial();
            SetBotonesAsignacion(true);
            btnAplicarCambios.Enabled = true;
            pnlFamiliaNombre.Visible = true;
            txtNombreRol.Enabled = true;
            txtNombreRol.Text = "";
            btnEliminarRol.Visible = false;

            CargarCmbPatentes();
            CargarCmbFamilias();
            cmbRolActual.Enabled = false;
        }

        private void LoadModoEdicion()
        {
            EstadoInicial();
            SetBotonesAsignacion(true);
            btnAplicarCambios.Enabled = true;
            pnlFamiliaNombre.Visible = true;
            txtNombreRol.Enabled = false;
            cmbRolActual.Enabled = true;
            btnEliminarRol.Visible = true;
            CargarCmbRoles();
        }

        // ── Helpers de carga ──────────────────────────────────────────────────

        private void CargarCmbRoles()
        {
            cmbRolActual.DataSource = null;
            cmbRolActual.DataSource = _rolBLL.GetAllRoles_90DI();
            cmbRolActual.DisplayMember = _dm;
        }

        private void CargarCmbPatentes()
        {
            var idsAsignados = tempRol.Patentes.Select(p => p.IdPatente_90DI).ToHashSet();
            cmbPatentesDisponibles.DataSource = null;
            cmbPatentesDisponibles.DataSource = _rolBLL.GetAllPatentes_90DI()
                                                          .Where(p => !idsAsignados.Contains(p.IdPatente_90DI))
                                                          .ToList();
            cmbPatentesDisponibles.DisplayMember = _dm;
        }

        private void CargarCmbFamilias()
        {
            var idsAsignados = tempRol.Familias.Select(f => f.IdFamilia_90DI).ToHashSet();
            cmbFamiliasDisponibles.DataSource = null;
            cmbFamiliasDisponibles.DataSource = _rolBLL.GetAllFamilias_90DI()
                                                          .Where(f => !idsAsignados.Contains(f.IdFamilia_90DI))
                                                          .ToList();
            cmbFamiliasDisponibles.DisplayMember = _dm;
        }

        private void RefrescarListasAsignadas()
        {
            LstPatentesAsignadas.DataSource = null;
            LstPatentesAsignadas.DataSource = tempRol.Patentes;
            LstPatentesAsignadas.DisplayMember = _dm;

            LstFamiliasAsignadas.DataSource = null;
            LstFamiliasAsignadas.DataSource = tempRol.Familias;
            LstFamiliasAsignadas.DisplayMember = _dm;

            CargarCmbPatentes();
            CargarCmbFamilias();
        }

        private void SetBotonesAsignacion(bool enabled)
        {
            btnAgregarPatente.Enabled = enabled;
            btnQuitarPatente.Enabled = enabled;
            btnAgregarFamilia.Enabled = enabled;
            btnQuitarFamilia.Enabled = enabled;
        }

        private void EstadoInicial()
        {
            tempRol = new Rol_90DI();
            tempPatente = new Patente_90DI();
            tempFamilia = new Familia_90DI();

            LstPatentesAsignadas.DataSource = null;
            LstFamiliasAsignadas.DataSource = null;
            txtNombreRol.Text = "";
            cmbRolActual.Enabled = true;
        }

        // ── Eventos radio buttons ─────────────────────────────────────────────

        private void rdbModoConsulta_CheckedChanged(object sender, EventArgs e) { if (rdbModoConsulta.Checked) LoadModoConsulta(); }
        private void rdbCrearRol_CheckedChanged(object sender, EventArgs e) { if (rdbCrearFamilia.Checked) LoadModoCreacion(); }
        private void rdbModificarRol_CheckedChanged(object sender, EventArgs e) { if (rdbModificarFamilia.Checked) LoadModoEdicion(); }

        // ── Selección de rol ──────────────────────────────────────────────────

        private void cmbRolActual_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRolActual.SelectedItem is not Rol_90DI rol) return;

            var rolCompleto = _rolBLL.GetRolCompleto_90DI(rol.IdRol_90DI);
            if (rolCompleto == null) return;

            tempRol = rolCompleto;
            txtNombreRol.Text = tempRol.Nombre_90DI;

            LstPatentesAsignadas.DataSource = null;
            LstPatentesAsignadas.DataSource = tempRol.Patentes;
            LstPatentesAsignadas.DisplayMember = _dm;

            LstFamiliasAsignadas.DataSource = null;
            LstFamiliasAsignadas.DataSource = tempRol.Familias;
            LstFamiliasAsignadas.DisplayMember = _dm;

            if (rdbModificarFamilia.Checked)
            {
                CargarCmbPatentes();
                CargarCmbFamilias();
            }
        }

        // ── Selección en combos disponibles ──────────────────────────────────

        private void cmbPatentesDisponibles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPatentesDisponibles.SelectedItem is Patente_90DI p) tempPatente = p;
        }

        private void cmbFamiliasDisponibles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFamiliasDisponibles.SelectedItem is Familia_90DI f) tempFamilia = f;
        }

        // ── Agregar / Quitar patentes ─────────────────────────────────────────

        private void btnAgregarPatente_Click(object sender, EventArgs e)
        {
            if (tempPatente.IdPatente_90DI == 0) return;
            if (tempRol.Patentes.Any(p => p.IdPatente_90DI == tempPatente.IdPatente_90DI)) return;

            tempRol.Patentes.Add(tempPatente);
            RefrescarListasAsignadas();
        }

        private void btnQuitarPatente_Click(object sender, EventArgs e)
        {
            if (LstPatentesAsignadas.SelectedItem is not Patente_90DI p) return;

            tempRol.Patentes.Remove(p);
            RefrescarListasAsignadas();
        }

        // ── Agregar / Quitar familias ─────────────────────────────────────────

        private void btnAgregarFamilia_Click(object sender, EventArgs e)
        {
            if (tempFamilia.IdFamilia_90DI == 0) return;
            if (tempRol.Familias.Any(f => f.IdFamilia_90DI == tempFamilia.IdFamilia_90DI)) return;

            tempRol.Familias.Add(tempFamilia);
            RefrescarListasAsignadas();
        }

        private void btnQuitarFamilia_Click(object sender, EventArgs e)
        {
            if (LstFamiliasAsignadas.SelectedItem is not Familia_90DI f) return;

            tempRol.Familias.Remove(f);
            RefrescarListasAsignadas();
        }

        // ── Aplicar ───────────────────────────────────────────────────────────

        private void btnAplicarCambios_Click(object sender, EventArgs e)
        {
            bool response = false;

            if (rdbCrearFamilia.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtNombreRol.Text))
                {
                    MessageBox.Show(LanguageManager_90DI.T("roles_msg_nombre_empty"), LanguageManager_90DI.T("roles_msg_nombre_empty_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (tempRol.Patentes.Count == 0 && tempRol.Familias.Count == 0)
                {
                    MessageBox.Show(LanguageManager_90DI.T("roles_msg_contenido_empty"), LanguageManager_90DI.T("roles_msg_nombre_empty_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string nombreTrim = txtNombreRol.Text.Trim();
                bool nombreDuplicado = _rolBLL.GetAllRoles_90DI()
                    .Any(r => r.Nombre_90DI.Equals(nombreTrim, StringComparison.OrdinalIgnoreCase));
                if (nombreDuplicado)
                {
                    MessageBox.Show(string.Format(LanguageManager_90DI.T("roles_msg_nombre_dup"), nombreTrim), LanguageManager_90DI.T("roles_msg_nombre_dup_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                tempRol.Nombre_90DI = nombreTrim;
                response = _rolBLL.CreateRol_90DI(tempRol);
                MessageBox.Show(
                    response ? LanguageManager_90DI.T("roles_msg_creado") : LanguageManager_90DI.T("roles_msg_error_crear"),
                    response ? LanguageManager_90DI.T("roles_msg_exito_title") : LanguageManager_90DI.T("roles_msg_error_title"),
                    MessageBoxButtons.OK, response ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            }

            if (rdbModificarFamilia.Checked)
            {
                if (tempRol.Patentes.Count == 0 && tempRol.Familias.Count == 0)
                {
                    MessageBox.Show(LanguageManager_90DI.T("roles_msg_contenido_empty"), LanguageManager_90DI.T("roles_msg_nombre_empty_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                response = _rolBLL.UpdateRol_90DI(tempRol);
                MessageBox.Show(
                    response ? LanguageManager_90DI.T("roles_msg_modificado") : LanguageManager_90DI.T("roles_msg_error_modificar"),
                    response ? LanguageManager_90DI.T("roles_msg_exito_title") : LanguageManager_90DI.T("roles_msg_error_title"),
                    MessageBoxButtons.OK, response ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            }

            rdbModoConsulta.Checked = true;
        }

        // ── Salir ─────────────────────────────────────────────────────────────

        private void button5_Click(object sender, EventArgs e) => this.Hide();

        private void label2_Click(object sender, EventArgs e) { }
        private void FrmAdminROLES_90DI_Load(object sender, EventArgs e) { }

        private void btnEliminarRol_Click(object sender, EventArgs e)
        {
            if (cmbRolActual.SelectedItem is not Rol_90DI rol) return;

            var confirm = MessageBox.Show(
                string.Format(LanguageManager_90DI.T("roles_msg_confirm_eliminar"), rol.Nombre_90DI),
                LanguageManager_90DI.T("roles_msg_confirm_eliminar_title"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            bool response = _rolBLL.DeleteRol_90DI(rol.IdRol_90DI, rol.Nombre_90DI);
            MessageBox.Show(
                response ? LanguageManager_90DI.T("roles_msg_eliminado") : LanguageManager_90DI.T("roles_msg_error_eliminar"),
                response ? LanguageManager_90DI.T("roles_msg_exito_title") : LanguageManager_90DI.T("roles_msg_error_title"),
                MessageBoxButtons.OK,
                response ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            rdbModoConsulta.Checked = true;
        }
    }
}
