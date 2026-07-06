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

            // El Designer no suscribe estos eventos — los conectamos aquí igual que FrmAdminFamilias
            rdbModoConsulta.CheckedChanged    += rdbModoConsulta_CheckedChanged;
            rdbCrearFamilia.CheckedChanged    += rdbCrearRol_CheckedChanged;
            rdbModificarFamilia.CheckedChanged += rdbModificarRol_CheckedChanged;

            cmbRolActual.SelectedIndexChanged           += cmbRolActual_SelectedIndexChanged;
            cmbPatentesDisponibles.SelectedIndexChanged += cmbPatentesDisponibles_SelectedIndexChanged;
            cmbFamiliasDisponibles.SelectedIndexChanged += cmbFamiliasDisponibles_SelectedIndexChanged;

            btnAgregarPatente.Click += btnAgregarPatente_Click;
            btnQuitarPatente.Click  += btnQuitarPatente_Click;
            btnAgregarFamilia.Click += btnAgregarFamilia_Click;
            btnQuitarFamilia.Click  += btnQuitarFamilia_Click;
            btnEliminarRol.Click    += btnEliminarRol_Click;

            rdbModoConsulta.Checked = true; // ahora sí dispara LoadModoConsulta → CargarCmbRoles

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

            CargarCmbPatentes();
            CargarCmbFamilias();
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

        // Devuelve todos los IdPatente de una familia: directas + las de cada subfamilia.
        private HashSet<int> GetIdsPatentesCompletos(Familia_90DI familiaCompleta)
        {
            var ids = familiaCompleta.Patentes.Select(p => p.IdPatente_90DI).ToHashSet();
            foreach (var sub in familiaCompleta.SubFamilias)
            {
                var subCompleta = _rolBLL.GetFamiliaCompleta_90DI(sub.IdFamilia_90DI);
                if (subCompleta != null)
                    foreach (var p in subCompleta.Patentes)
                        ids.Add(p.IdPatente_90DI);
            }
            return ids;
        }

        // ── Agregar / Quitar patentes ─────────────────────────────────────────

        private void btnAgregarPatente_Click(object sender, EventArgs e)
        {
            if (tempPatente.IdPatente_90DI == 0) return;
            if (tempRol.Patentes.Any(p => p.IdPatente_90DI == tempPatente.IdPatente_90DI)) return;

            // Bloquear si la patente ya está cubierta por alguna familia (o subfamilia) asignada al rol
            foreach (var familia in tempRol.Familias)
            {
                var completa = _rolBLL.GetFamiliaCompleta_90DI(familia.IdFamilia_90DI);
                if (completa == null) continue;
                if (GetIdsPatentesCompletos(completa).Contains(tempPatente.IdPatente_90DI))
                {
                    MessageBox.Show(
                        string.Format(LanguageManager_90DI.T("roles_msg_patente_dup"), tempPatente.Nombre_90DI, familia.Nombre_90DI),
                        LanguageManager_90DI.T("roles_msg_patente_dup_title"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

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

            var familiaCompleta = _rolBLL.GetFamiliaCompleta_90DI(tempFamilia.IdFamilia_90DI);
            if (familiaCompleta == null) return;

            // Todas las patentes (directas + subfamilias) de la familia a agregar
            var idsNueva = GetIdsPatentesCompletos(familiaCompleta);

            // Bloquear si alguna patente de la nueva familia ya está asignada directamente al rol
            foreach (var p in familiaCompleta.Patentes.Concat(
                familiaCompleta.SubFamilias
                    .SelectMany(s => _rolBLL.GetFamiliaCompleta_90DI(s.IdFamilia_90DI)?.Patentes ?? Enumerable.Empty<Patente_90DI>())))
            {
                if (tempRol.Patentes.Any(dp => dp.IdPatente_90DI == p.IdPatente_90DI))
                {
                    MessageBox.Show(
                        string.Format(LanguageManager_90DI.T("roles_msg_patente_dup"), p.Nombre_90DI, familiaCompleta.Nombre_90DI),
                        LanguageManager_90DI.T("roles_msg_patente_dup_title"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Bloquear si la nueva familia comparte patentes con otra familia ya asignada al rol
            foreach (var otraFamilia in tempRol.Familias)
            {
                var otraCompleta = _rolBLL.GetFamiliaCompleta_90DI(otraFamilia.IdFamilia_90DI);
                if (otraCompleta == null) continue;
                var idsOtra = GetIdsPatentesCompletos(otraCompleta);
                var pConflicto = familiaCompleta.Patentes
                    .Concat(familiaCompleta.SubFamilias
                        .SelectMany(s => _rolBLL.GetFamiliaCompleta_90DI(s.IdFamilia_90DI)?.Patentes ?? Enumerable.Empty<Patente_90DI>()))
                    .FirstOrDefault(p => idsOtra.Contains(p.IdPatente_90DI));
                if (pConflicto != null)
                {
                    MessageBox.Show(
                        string.Format(LanguageManager_90DI.T("roles_msg_familia_dup"), pConflicto.Nombre_90DI, otraFamilia.Nombre_90DI, familiaCompleta.Nombre_90DI),
                        LanguageManager_90DI.T("roles_msg_familia_dup_title"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            tempRol.Familias.Add(tempFamilia);
            RefrescarListasAsignadas();
        }

        private void btnQuitarFamilia_Click(object sender, EventArgs e)
        {
            if (LstFamiliasAsignadas.SelectedItem is not Familia_90DI f) return;

            tempRol.Familias.Remove(f);
            RefrescarListasAsignadas();
        }

        // ── Validación de redundancia ─────────────────────────────────────────

        // Retorna mensaje de error si una patente directa del rol ya está cubierta
        // por alguna familia, o si dos familias comparten una patente. Null = OK.
        private string? ValidarRedundanciaRol()
        {
            var familiasCompletas = tempRol.Familias
                .Select(f => _rolBLL.GetFamiliaCompleta_90DI(f.IdFamilia_90DI))
                .Where(f => f != null)
                .ToList();

            foreach (var familia in familiasCompletas)
            {
                foreach (var p in familia!.Patentes)
                {
                    if (tempRol.Patentes.Any(dp => dp.IdPatente_90DI == p.IdPatente_90DI))
                        return string.Format(LanguageManager_90DI.T("roles_msg_patente_dup"), p.Nombre_90DI, familia.Nombre_90DI);
                }
            }

            for (int i = 0; i < familiasCompletas.Count; i++)
            {
                var idsI = familiasCompletas[i]!.Patentes.Select(p => p.IdPatente_90DI).ToHashSet();
                for (int j = i + 1; j < familiasCompletas.Count; j++)
                {
                    foreach (var p in familiasCompletas[j]!.Patentes)
                    {
                        if (idsI.Contains(p.IdPatente_90DI))
                            return string.Format(LanguageManager_90DI.T("roles_msg_familia_dup"), p.Nombre_90DI, familiasCompletas[i]!.Nombre_90DI, familiasCompletas[j]!.Nombre_90DI);
                    }
                }
            }
            return null;
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
                var conflicto = ValidarRedundanciaRol();
                if (conflicto != null)
                {
                    MessageBox.Show(conflicto, LanguageManager_90DI.T("roles_msg_patente_dup_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                var conflictoMod = ValidarRedundanciaRol();
                if (conflictoMod != null)
                {
                    MessageBox.Show(conflictoMod, LanguageManager_90DI.T("roles_msg_patente_dup_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        private void FrmAdminROLES_90DI_Load(object sender, EventArgs e)
        {
            UI_90DI.LayoutHelper_90DI.AjustarAPantalla_90DI(this);
        }

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
