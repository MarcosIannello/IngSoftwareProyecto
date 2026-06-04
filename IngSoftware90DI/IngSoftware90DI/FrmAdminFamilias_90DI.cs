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
        public Familia_90DI FamiliaActual = new Familia_90DI();
        public RolBLL_90DI _rolBLL = new RolBLL_90DI();
        Familia_90DI ComboFamilia = new Familia_90DI();
        Patente_90DI tempPatente;
        Familia_90DI tempFamilia = new Familia_90DI();
        List<Familia_90DI> FamiliasHijas = new List<Familia_90DI>();
        Familia_90DI CreateFamilia = new Familia_90DI();
        public string displayMember = "Nombre_90DI";


        public FrmAdminFamilias()
        {
            InitializeComponent();
            PatentesBd = _rolBLL.GetAllPatentes_90DI();
            rdbModoConsulta.Checked = true;
            LoadModoConsulta();


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
            LstFamiliasPatentes.DataSource = tempFamilia.Patentes;
            txtNombreFamilia.Text = tempFamilia.Nombre_90DI;
            pnlFamiliaNombre.Visible = true;
            txtNombreFamilia.Enabled = false;
            pnlAgregarQuitarFamilias.Visible = false;

        }

        public void LoadModoCreacion()
        {
            EstadoInicial();
            LoadPatentesLst();
            EnableComponents();

            lstFamilias.Visible = true;
            cmbFamiliasDisponibles.DataSource = _rolBLL.GetAllFamilias_90DI();
            cmbFamiliasDisponibles.DisplayMember = displayMember;
            pnlFamiliaNombre.Visible = true;
            txtNombreFamilia.Enabled = true;
            txtNombreFamilia.Text = "Ingrese nombre Familia";

            if (_rolBLL.GetAllFamilias_90DI().Count > 0)
                pnlAgregarQuitarFamilias.Visible = true;

        }

        public void LoadModoEdicion()
        {
            EstadoInicial();
            LoadPatentesLst();
            EnableComponents();

            cmbFamiliasDisponibles.DataSource = _rolBLL.GetAllFamilias_90DI();
            cmbFamiliasDisponibles.DisplayMember = displayMember;
            LstFamiliasPatentes.DataSource = tempFamilia.Patentes;
            txtNombreFamilia.Text = tempFamilia.Nombre_90DI;
            pnlFamiliaNombre.Visible = true;
            pnlAgregarQuitarFamilias.Visible = true;
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
            tempFamilia.Patentes.Add(tempPatente);
            PatentesBd.Remove(tempPatente);
            lstPatentes.DataSource = null;
            lstPatentes.DataSource = PatentesBd;
            LstFamiliasPatentes.DataSource = null;
            LstFamiliasPatentes.DataSource = tempFamilia.Patentes;
        }

        private void btnAplicarCambios_Click(object sender, EventArgs e)
        {
            bool response = false;
            if (rdbCrearFamilia.Checked)
            {
                CreateFamilia = tempFamilia;
                CreateFamilia.Nombre_90DI = txtNombreFamilia.Text;

                Console.WriteLine(CreateFamilia);
                response = _rolBLL.CreateFamilia_90DI(CreateFamilia);

                if (response)
                {
                    MessageBox.Show("Familia creada con exito");
                }
            }

            if (rdbModificarFamilia.Checked)
            {
                //response = _rolBLL.UpdateFamilia_90DI(tempFamilia);

                if (response)
                {
                    MessageBox.Show("Familia creada con exito");
                }
            }

            LoadModoConsulta();
        }

        public void EstadoInicial()
        {
            PatentesBd = _rolBLL.GetAllPatentes_90DI();
            LstFamiliasPatentes.DataSource = null;
            lstPatentes.DataSource = null;
            lstPatentes.DataSource = PatentesBd;
            tempFamilia = new Familia_90DI();
            tempPatente = new Patente_90DI();
            txtNombreFamilia.Text = "";
        }

        private void lstFamilias_SelectedValueChanged(object sender, EventArgs e)
        {
            tempFamilia = (Familia_90DI)lstFamilias.SelectedItem;
            if (tempFamilia != null)
            {
                LstFamiliasPatentes.DataSource = null;
                var familiaCompleta = _rolBLL.GetFamiliaCompleta_90DI(tempFamilia.IdFamilia_90DI);
                if (familiaCompleta.Patentes.Count > 0 && !rdbCrearFamilia.Checked)
                {
                    LstFamiliasPatentes.DataSource = familiaCompleta.Patentes;
                }
            }
        }

        //Agregar / Quitar Familias Hijas En moficacion y creacion
        private void button2_Click(object sender, EventArgs e)
        {
            FamiliasHijas.Add(tempFamilia);
            lstFamiliasFamilia.DataSource = FamiliasHijas;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FamiliasHijas.Remove(tempFamilia);
            lstFamiliasFamilia.DataSource = null;
            lstFamiliasFamilia.DataSource = FamiliasHijas;
            lstFamiliasFamilia.DisplayMember = displayMember;

        }

        private void btnQuitarPatente_Click(object sender, EventArgs e)
        {
            tempFamilia.Patentes.Remove(tempPatente);
            PatentesBd.Remove(tempPatente);
            lstPatentes.DataSource = null;
            lstPatentes.DataSource = PatentesBd;
            LstFamiliasPatentes.DataSource = null;
            LstFamiliasPatentes.DataSource = tempFamilia.Patentes;
        }

        private void LstFamiliasPatentes_SelectedIndexChanged(object sender, EventArgs e)
        {
            tempPatente = (Patente_90DI)LstFamiliasPatentes.SelectedItem ?? new Patente_90DI();
        }

        private void cmbFamiliasDisponibles_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboFamilia = (Familia_90DI)cmbFamiliasDisponibles.SelectedItem ?? new Familia_90DI();
        }
    }
}
