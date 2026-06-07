using BLL;
using Services_90DI.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Capital_
{
    public partial class FrmAdminROLES_90DI : Form
    {
        private readonly RolBLL_90DI _rolBLL = new RolBLL_90DI();
        private readonly string _dm = "Nombre_90DI";

        private Rol_90DI tempRol = new Rol_90DI();
        private Patente_90DI tempPatente = new Patente_90DI();
        private Familia_90DI tempFamilia = new Familia_90DI();

        public FrmAdminROLES_90DI()
        {
            InitializeComponent();
            rdbModoConsulta.Checked = true; //Modo consulta es default.
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
                    MessageBox.Show("Ingresá un nombre para el rol.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                tempRol.Nombre_90DI = txtNombreRol.Text.Trim();
                response = _rolBLL.CreateRol_90DI(tempRol);
                MessageBox.Show(response ? "Rol creado con éxito." : "Error al crear el rol.",
                                response ? "Éxito" : "Error", MessageBoxButtons.OK,
                                response ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            }

            if (rdbModificarFamilia.Checked)
            {
                response = _rolBLL.UpdateRol_90DI(tempRol);
                MessageBox.Show(response ? "Rol modificado con éxito." : "Error al modificar el rol.",
                                response ? "Éxito" : "Error", MessageBoxButtons.OK,
                                response ? MessageBoxIcon.Information : MessageBoxIcon.Error);
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
                $"¿Estás seguro de que querés eliminar el rol \"{rol.Nombre_90DI}\"?\nEsta acción eliminará todas sus relaciones y no se puede deshacer.",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            bool response = _rolBLL.DeleteRol_90DI(rol.IdRol_90DI, rol.Nombre_90DI);
            MessageBox.Show(
                response ? "Rol eliminado con éxito." : "Error al eliminar el rol.",
                response ? "Éxito" : "Error",
                MessageBoxButtons.OK,
                response ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            rdbModoConsulta.Checked = true;
        }
    }
}
