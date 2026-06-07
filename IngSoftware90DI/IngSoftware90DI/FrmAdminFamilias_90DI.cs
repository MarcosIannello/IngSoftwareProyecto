using BLL;
using Services_90DI.entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI_90DI;

namespace Capital_
{
    public partial class FrmAdminFamilias : Form
    {
        public List<Patente_90DI> PatentesBd = new List<Patente_90DI>();
        public RolBLL_90DI _rolBLL = new RolBLL_90DI();
        Familia_90DI ComboFamilia = new Familia_90DI();
        Familia_90DI FamiliaHijaSeleccionada = new Familia_90DI();
        Patente_90DI tempPatente = new Patente_90DI();
        Familia_90DI tempFamilia = new Familia_90DI();
        List<Familia_90DI> FamiliasHijas = new List<Familia_90DI>();
        public string displayMember = "Nombre_90DI";


        public FrmAdminFamilias()
        {
            InitializeComponent();
            PatentesBd = _rolBLL.GetAllPatentes_90DI();
            rdbModoConsulta.Checked = true; // dispara CheckedChanged → LoadModoConsulta
        }

        private void FrmAdminRoles_90DI_Load(object sender, EventArgs e)
        {

        }

        //Carga lst con patentes de bd (creacion / edicion)
        private void LoadPatentesLst()
        {
            lstPatentes.DataSource = null;
            lstPatentes.DataSource = PatentesBd;
            lstPatentes.DisplayMember = displayMember;
        }

        private void LoadFamiliaLst()
        {
            var familias = _rolBLL.GetAllFamilias_90DI();
            lstFamilias.DataSource = null;
            lstFamilias.DataSource = familias;
            lstFamilias.DisplayMember = displayMember;
        }

        public void LoadModoConsulta()
        {
            EstadoInicial();
            LoadFamiliaLst();
            btnAgregarPatente.Enabled = false;
            btnQuitarPatente.Enabled = false;
            btnAplicarCambios.Enabled = false;
            lstFamilias.Visible = true;
            // seleccionar la primera familia y mostrar sus patentes
            if (lstFamilias.Items.Count > 0)
            {
                lstFamilias.SelectedIndex = 0;  // dispara SelectedValueChanged → carga patentes
                txtNombreFamilia.Text = tempFamilia.Nombre_90DI;
            }

            pnlFamiliaNombre.Visible = true;
            txtNombreFamilia.Enabled = false;
            pnlAgregarQuitarFamilias.Visible = false;

        }

        public void LoadModoCreacion()
        {
            EstadoInicial();
            LoadPatentesLst();
            EnableComponents();

            var familias = _rolBLL.GetAllFamilias_90DI();
            lstFamilias.Visible = false;
            cmbFamiliasDisponibles.DataSource = familias;
            cmbFamiliasDisponibles.DisplayMember = displayMember;
            pnlFamiliaNombre.Visible = true;
            txtNombreFamilia.Enabled = true;
            txtNombreFamilia.Text = "";
            pnlAgregarQuitarFamilias.Visible = familias.Count > 0;
        }

        public void LoadModoEdicion()
        {
            EstadoInicial();
            LoadFamiliaLst();
            EnableComponents();

            cmbFamiliasDisponibles.DataSource = _rolBLL.GetAllFamilias_90DI().Where(f => f != tempFamilia).ToList();
            cmbFamiliasDisponibles.DisplayMember = displayMember;
            pnlFamiliaNombre.Visible = true;
            pnlAgregarQuitarFamilias.Visible = true;

            if (lstFamilias.Items.Count > 0)
                lstFamilias.SelectedIndex = 0; // dispara SelectedValueChanged → carga patentes y filtra lstPatentes
        }

        public void EnableComponents()
        {
            btnAgregarPatente.Enabled = true;
            btnQuitarPatente.Enabled = true;
            btnAplicarCambios.Enabled = true;
            lstFamilias.Visible = true;
        }

        private void rdbModificarFamilia_CheckedChanged(object sender, EventArgs e)
        {
            LoadModoEdicion();
        }

        private void rdbCrearFamilia_CheckedChanged(object sender, EventArgs e)
        {
            LoadModoCreacion();
        }

        private void rdbModoConsulta_CheckedChanged(object sender, EventArgs e)
        {
            LoadModoConsulta();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var frmMenu = new FrmMenu_90DI();
            frmMenu.Show();
            this.Close();

        }


        private void lstPatentes_SelectedIndexChanged(object sender, EventArgs e)
        {

            tempPatente = (Patente_90DI)lstPatentes.SelectedItem ?? new Patente_90DI();
            Console.WriteLine(tempPatente);
        }

        private void btnAgregarPatente_Click(object sender, EventArgs e)
        {
            if (tempPatente == null || tempPatente.IdPatente_90DI == 0) return;
            if (tempFamilia.Patentes.Any(p => p.IdPatente_90DI == tempPatente.IdPatente_90DI)) return;

            tempFamilia.Patentes.Add(tempPatente);
            PatentesBd.Remove(tempPatente);

            lstPatentes.DataSource = null;
            lstPatentes.DataSource = PatentesBd;
            lstPatentes.DisplayMember = displayMember;
            if (lstPatentes.Items.Count > 0) lstPatentes.SelectedIndex = 0;

            LstFamiliasPatentes.DataSource = null;
            LstFamiliasPatentes.DataSource = tempFamilia.Patentes;
            LstFamiliasPatentes.DisplayMember = displayMember;
        }

        private void btnAplicarCambios_Click(object sender, EventArgs e)
        {
            bool response = false;
            if (rdbCrearFamilia.Checked)
            {
                tempFamilia.Nombre_90DI = txtNombreFamilia.Text;
                tempFamilia.SubFamilias = FamiliasHijas;
                response = _rolBLL.CreateFamilia_90DI(tempFamilia);

                if (response)
                    MessageBox.Show("Familia creada con éxito");
                else
                    MessageBox.Show("Error al crear la familia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (rdbModificarFamilia.Checked)
            {
                tempFamilia.SubFamilias = FamiliasHijas;
                response = _rolBLL.UpdateFamilia_90DI(tempFamilia);
                if (response)
                    MessageBox.Show("Familia modificada con éxito");
                else
                    MessageBox.Show("Error al modificar la familia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            rdbModoConsulta.Checked = true;
        }

        public void EstadoInicial()
        {
            PatentesBd = _rolBLL.GetAllPatentes_90DI();
            LstFamiliasPatentes.DataSource = null;
            lstPatentes.DataSource = null;
            lstPatentes.DataSource = PatentesBd;
            lstFamiliasFamilia.DataSource = null;
            tempFamilia = new Familia_90DI();
            tempPatente = new Patente_90DI();
            ComboFamilia = new Familia_90DI();
            FamiliaHijaSeleccionada = new Familia_90DI();
            FamiliasHijas = new List<Familia_90DI>();  // reset familias hijas
            txtNombreFamilia.Text = "";
        }

        private void lstFamilias_SelectedValueChanged(object sender, EventArgs e)
        {
            if (lstFamilias.SelectedItem is not Familia_90DI familia) return;
            if (rdbCrearFamilia.Checked) return;

            var familiaCompleta = _rolBLL.GetFamiliaCompleta_90DI(familia.IdFamilia_90DI);
            if (familiaCompleta == null) return;

            tempFamilia = familiaCompleta;

            // mostrar patentes de la familia
            LstFamiliasPatentes.DataSource = null;
            LstFamiliasPatentes.DataSource = tempFamilia.Patentes;
            LstFamiliasPatentes.DisplayMember = displayMember;

            // mostrar sub-familias
            FamiliasHijas = tempFamilia.SubFamilias.ToList();
            lstFamiliasFamilia.DataSource = null;
            lstFamiliasFamilia.DataSource = FamiliasHijas;
            lstFamiliasFamilia.DisplayMember = displayMember;

            // modo edicion: cargar en lstPatentes las patentes de la familia
            // y quitar del combo la familia actual
            if (rdbModificarFamilia.Checked)
            {
                // filtrar patentes que ya pertenecen a la familia
                var idsEnFamilia = tempFamilia.Patentes.Select(p => p.IdPatente_90DI).ToHashSet();
                PatentesBd = _rolBLL.GetAllPatentes_90DI()
                    .Where(p => !idsEnFamilia.Contains(p.IdPatente_90DI))
                    .ToList();
                lstPatentes.DataSource = null;
                lstPatentes.DataSource = PatentesBd;
                lstPatentes.DisplayMember = displayMember;

                var familiasDisponibles = _rolBLL.GetAllFamilias_90DI()
                    .Where(f => f.IdFamilia_90DI != tempFamilia.IdFamilia_90DI)
                    .ToList();
                cmbFamiliasDisponibles.DataSource = null;
                cmbFamiliasDisponibles.DataSource = familiasDisponibles;
                cmbFamiliasDisponibles.DisplayMember = displayMember;

                txtNombreFamilia.Text = tempFamilia.Nombre_90DI;
            }
        }

        //Agregar / Quitar Familias Hijas En moficacion y creacion
        private void button2_Click(object sender, EventArgs e)
        {
            if (ComboFamilia == null || FamiliasHijas.Any(f => f.IdFamilia_90DI == ComboFamilia.IdFamilia_90DI)) return;

            FamiliasHijas.Add(ComboFamilia);
            lstFamiliasFamilia.DataSource = null;
            lstFamiliasFamilia.DataSource = FamiliasHijas;
            lstFamiliasFamilia.DisplayMember = displayMember;

            RefrescarPatentesDisponibles();
        }

        // Filtra lstPatentes quitando patentes ya usadas en familias hijas asignadas
        private void RefrescarPatentesDisponibles()
        {
            var idsEnFamiliaActual = tempFamilia.Patentes.Select(p => p.IdPatente_90DI).ToHashSet();
            var idsEnFamiliasHijas = FamiliasHijas
                .SelectMany(f => f.Patentes)
                .Select(p => p.IdPatente_90DI)
                .ToHashSet();

            PatentesBd = _rolBLL.GetAllPatentes_90DI()
                .Where(p => !idsEnFamiliaActual.Contains(p.IdPatente_90DI)
                         && !idsEnFamiliasHijas.Contains(p.IdPatente_90DI))
                .ToList();

            lstPatentes.DataSource = null;
            lstPatentes.DataSource = PatentesBd;
            lstPatentes.DisplayMember = displayMember;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (FamiliaHijaSeleccionada == null || FamiliaHijaSeleccionada.IdFamilia_90DI == 0) return;

            FamiliasHijas.Remove(FamiliaHijaSeleccionada);
            lstFamiliasFamilia.DataSource = null;
            lstFamiliasFamilia.DataSource = FamiliasHijas;
            lstFamiliasFamilia.DisplayMember = displayMember;

            RefrescarPatentesDisponibles(); // devuelve patentes de la familia hija quitada
        }

        private void btnQuitarPatente_Click(object sender, EventArgs e)
        {
            if (tempPatente == null || tempPatente.IdPatente_90DI == 0) return;

            tempFamilia.Patentes.Remove(tempPatente);
            PatentesBd.Add(tempPatente);  // devuelve la patente al listado disponible

            lstPatentes.DataSource = null;
            lstPatentes.DataSource = PatentesBd;
            lstPatentes.DisplayMember = displayMember;

            LstFamiliasPatentes.DataSource = null;
            LstFamiliasPatentes.DataSource = tempFamilia.Patentes;
            LstFamiliasPatentes.DisplayMember = displayMember;
        }

        private void LstFamiliasPatentes_SelectedIndexChanged(object sender, EventArgs e)
        {
            tempPatente = (Patente_90DI)LstFamiliasPatentes.SelectedItem ?? new Patente_90DI();
        }

        private void cmbFamiliasDisponibles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFamiliasDisponibles.SelectedItem is Familia_90DI f)
                ComboFamilia = f;
        }

        private void lstFamiliasFamilia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstFamiliasFamilia.SelectedItem is Familia_90DI f)
                FamiliaHijaSeleccionada = f;
        }

        private void lstFamilias_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
