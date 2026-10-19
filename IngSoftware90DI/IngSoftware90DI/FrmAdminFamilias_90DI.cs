using BLL;
using Services_90DI;
using Services_90DI.entities;
using UI_90DI;
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

namespace Capital_
{
    public partial class FrmAdminFamilias : Form, IObserver_90DI
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

            // Los eventos se enganchan a mano porque el Designer de Familias no los tiene
            // (el de Roles sí). Tiene que ir antes de setear el modo inicial, si no la primera
            // selección no carga nada.
            rdbModoConsulta.CheckedChanged     += rdbModoConsulta_CheckedChanged;
            rdbModificarFamilia.CheckedChanged += rdbModificarFamilia_CheckedChanged;
            rdbCrearFamilia.CheckedChanged     += rdbCrearFamilia_CheckedChanged;

            btnAgregarPatente.Click  += btnAgregarPatente_Click;
            btnQuitarPatente.Click   += btnQuitarPatente_Click;
            btnAplicarCambios.Click  += btnAplicarCambios_Click;
            btnEliminarFamilia.Click += btnEliminarFamilia_Click;
            button2.Click            += button2_Click; // agregar familia
            button1.Click            += button1_Click; // quitar familia

            lstFamilias.SelectedValueChanged         += lstFamilias_SelectedValueChanged;
            lstPatentes.SelectedIndexChanged         += lstPatentes_SelectedIndexChanged;
            LstFamiliasPatentes.SelectedIndexChanged += LstFamiliasPatentes_SelectedIndexChanged;
            lstFamiliasFamilia.SelectedIndexChanged  += lstFamiliasFamilia_SelectedIndexChanged;
            cmbFamiliasDisponibles.SelectedIndexChanged += cmbFamiliasDisponibles_SelectedIndexChanged;

            PatentesBd = _rolBLL.GetAllPatentes_90DI();
            rdbModoConsulta.Checked = true;

            LanguageManager_90DI.Current_90DI.AddObserver_90DI(this);
            var t = LanguageManager_90DI.Current_90DI.TraduccionesActuales_90DI;
            if (t.Count > 0) UpdateLanguage_90DI(t);
        }

        public void UpdateLanguage_90DI(Dictionary<string, string> traducciones)
        {
            if (traducciones.TryGetValue("familias_title",                out var v)) label1.Text             = v;
            if (traducciones.TryGetValue("familias_lbl_patentes_familia", out v))     label2.Text             = v;
            if (traducciones.TryGetValue("familias_lbl_patentes",         out v))     label3.Text             = v;
            if (traducciones.TryGetValue("familias_lbl_nombre",           out v))     label4.Text             = v;
            if (traducciones.TryGetValue("familias_lbl_familias_disponibles", out v)) label5.Text             = v;
            if (traducciones.TryGetValue("familias_btn_agregar_patente",  out v))     btnAgregarPatente.Text  = v;
            if (traducciones.TryGetValue("familias_btn_quitar_patente",   out v))     btnQuitarPatente.Text   = v;
            if (traducciones.TryGetValue("familias_btn_aplicar",          out v))     btnAplicarCambios.Text  = v;
            if (traducciones.TryGetValue("familias_btn_salir",            out v))     button5.Text            = v;
            if (traducciones.TryGetValue("familias_btn_agregar_familia",  out v))     button2.Text            = v;
            if (traducciones.TryGetValue("familias_btn_quitar_familia",   out v))     button1.Text            = v;
            if (traducciones.TryGetValue("familias_rdb_consulta",         out v))     rdbModoConsulta.Text    = v;
            if (traducciones.TryGetValue("familias_rdb_modificar",        out v))     rdbModificarFamilia.Text= v;
            if (traducciones.TryGetValue("familias_rdb_crear",            out v))     rdbCrearFamilia.Text    = v;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            LanguageManager_90DI.Current_90DI.Unsubscribe_90DI(this);
            base.OnFormClosed(e);
        }

        private void FrmAdminFamilias_90DI_Load(object sender, EventArgs e)
        {
            UI_90DI.LayoutHelper_90DI.AjustarAPantalla_90DI(this);
        }

        // Carga la lista con las patentes de la base (para crear o editar)
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
            btnEliminarFamilia.Visible = false;
            lstFamilias.Visible = true;
            // Arrancamos con la primera familia seleccionada
            if (lstFamilias.Items.Count > 0)
            {
                lstFamilias.SelectedIndex = 0;  // esto dispara SelectedValueChanged y carga sus patentes
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
            btnEliminarFamilia.Visible = false;

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
            btnEliminarFamilia.Visible = true;

            cmbFamiliasDisponibles.DataSource = _rolBLL.GetAllFamilias_90DI().Where(f => f != tempFamilia).ToList();
            cmbFamiliasDisponibles.DisplayMember = displayMember;
            pnlFamiliaNombre.Visible = true;
            pnlAgregarQuitarFamilias.Visible = true;

            if (lstFamilias.Items.Count > 0)
                lstFamilias.SelectedIndex = 0; // dispara SelectedValueChanged: carga las patentes y filtra lstPatentes
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
            if (!rdbModificarFamilia.Checked) return;
            LoadModoEdicion();
        }

        private void rdbCrearFamilia_CheckedChanged(object sender, EventArgs e)
        {
            if (!rdbCrearFamilia.Checked) return;
            LoadModoCreacion();
        }

        private void rdbModoConsulta_CheckedChanged(object sender, EventArgs e)
        {
            if (!rdbModoConsulta.Checked) return;
            LoadModoConsulta();
        }

        private void button5_Click(object sender, EventArgs e) => this.Hide();


        private void lstPatentes_SelectedIndexChanged(object sender, EventArgs e)
        {

            tempPatente = (Patente_90DI)lstPatentes.SelectedItem ?? new Patente_90DI();
        }

        private void btnAgregarPatente_Click(object sender, EventArgs e)
        {
            if (tempPatente == null || tempPatente.IdPatente_90DI == 0) return;
            if (tempFamilia.Patentes_90DI.Any(p => p.IdPatente_90DI == tempPatente.IdPatente_90DI)) return;

            // No dejar agregarla si ya la trae alguna subfamilia
            var hijaConflicto = FamiliasHijas.FirstOrDefault(f => f.Patentes_90DI.Any(p => p.IdPatente_90DI == tempPatente.IdPatente_90DI));
            if (hijaConflicto != null)
            {
                MessageBox.Show(
                    string.Format(LanguageManager_90DI.T_90DI("familias_msg_patente_dup"), tempPatente.Nombre_90DI, hijaConflicto.Nombre_90DI),
                    LanguageManager_90DI.T_90DI("familias_msg_patente_dup_title"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            tempFamilia.Patentes_90DI.Add(tempPatente);
            PatentesBd.Remove(tempPatente);

            lstPatentes.DataSource = null;
            lstPatentes.DataSource = PatentesBd;
            lstPatentes.DisplayMember = displayMember;
            if (lstPatentes.Items.Count > 0) lstPatentes.SelectedIndex = 0;

            // Hay que actualizar tempPatente a mano: al re-bindear, el combo ya queda en el índice 0,
            // entonces SelectedIndex = 0 no dispara SelectedIndexChanged y tempPatente
            // se quedaría con la patente anterior.
            tempPatente = lstPatentes.SelectedItem as Patente_90DI ?? new Patente_90DI();

            LstFamiliasPatentes.DataSource = null;
            LstFamiliasPatentes.DataSource = tempFamilia.Patentes_90DI;
            LstFamiliasPatentes.DisplayMember = displayMember;
        }

        // Devuelve un mensaje si hay patentes repetidas entre las directas y las subfamilias,
        // o entre dos subfamilias. Si está todo bien devuelve null.
        private string? ValidarRedundanciaFamilia()
        {
            // Directa repetida en alguna subfamilia
            foreach (var hija in FamiliasHijas)
            {
                foreach (var p in hija.Patentes_90DI)
                {
                    if (tempFamilia.Patentes_90DI.Any(dp => dp.IdPatente_90DI == p.IdPatente_90DI))
                        return string.Format(LanguageManager_90DI.T_90DI("familias_msg_patente_dup"), p.Nombre_90DI, hija.Nombre_90DI);
                }
            }
            // Misma patente en dos subfamilias
            for (int i = 0; i < FamiliasHijas.Count; i++)
            {
                var idsI = FamiliasHijas[i].Patentes_90DI.Select(p => p.IdPatente_90DI).ToHashSet();
                for (int j = i + 1; j < FamiliasHijas.Count; j++)
                {
                    foreach (var p in FamiliasHijas[j].Patentes_90DI)
                    {
                        if (idsI.Contains(p.IdPatente_90DI))
                            return string.Format(LanguageManager_90DI.T_90DI("familias_msg_subfamilia_dup"), p.Nombre_90DI, FamiliasHijas[i].Nombre_90DI, FamiliasHijas[j].Nombre_90DI);
                    }
                }
            }
            return null;
        }

        private void btnAplicarCambios_Click(object sender, EventArgs e)
        {
            bool response = false;
            if (rdbCrearFamilia.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtNombreFamilia.Text))
                {
                    MessageBox.Show(LanguageManager_90DI.T_90DI("familias_msg_nombre_empty"), LanguageManager_90DI.T_90DI("familias_msg_nombre_empty_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (tempFamilia.Patentes_90DI.Count == 0 && FamiliasHijas.Count == 0)
                {
                    MessageBox.Show(LanguageManager_90DI.T_90DI("familias_msg_contenido_empty"), LanguageManager_90DI.T_90DI("familias_msg_nombre_empty_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var conflicto = ValidarRedundanciaFamilia();
                if (conflicto != null)
                {
                    MessageBox.Show(conflicto, LanguageManager_90DI.T_90DI("familias_msg_patente_dup_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string nombreTrim = txtNombreFamilia.Text.Trim();
                bool nombreDuplicado = _rolBLL.GetAllFamilias_90DI()
                    .Any(f => f.Nombre_90DI.Equals(nombreTrim, StringComparison.OrdinalIgnoreCase));
                if (nombreDuplicado)
                {
                    MessageBox.Show(string.Format(LanguageManager_90DI.T_90DI("familias_msg_nombre_dup"), nombreTrim), LanguageManager_90DI.T_90DI("familias_msg_nombre_dup_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                tempFamilia.Nombre_90DI = nombreTrim;
                tempFamilia.SubFamilias_90DI = FamiliasHijas;
                response = _rolBLL.CreateFamilia_90DI(tempFamilia);

                if (response)
                    MessageBox.Show(LanguageManager_90DI.T_90DI("familias_msg_creada"), LanguageManager_90DI.T_90DI("familias_msg_exito_title"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(LanguageManager_90DI.T_90DI("familias_msg_error_crear"), LanguageManager_90DI.T_90DI("familias_msg_error_title"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (rdbModificarFamilia.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtNombreFamilia.Text))
                {
                    MessageBox.Show(LanguageManager_90DI.T_90DI("familias_msg_nombre_empty"), LanguageManager_90DI.T_90DI("familias_msg_nombre_empty_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (tempFamilia.Patentes_90DI.Count == 0 && FamiliasHijas.Count == 0)
                {
                    MessageBox.Show(LanguageManager_90DI.T_90DI("familias_msg_contenido_empty"), LanguageManager_90DI.T_90DI("familias_msg_nombre_empty_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var conflictoMod = ValidarRedundanciaFamilia();
                if (conflictoMod != null)
                {
                    MessageBox.Show(conflictoMod, LanguageManager_90DI.T_90DI("familias_msg_patente_dup_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string nombreTrim = txtNombreFamilia.Text.Trim();
                bool nombreDuplicado = _rolBLL.GetAllFamilias_90DI()
                    .Any(f => f.Nombre_90DI.Equals(nombreTrim, StringComparison.OrdinalIgnoreCase)
                           && f.IdFamilia_90DI != tempFamilia.IdFamilia_90DI);
                if (nombreDuplicado)
                {
                    MessageBox.Show(string.Format(LanguageManager_90DI.T_90DI("familias_msg_nombre_dup2"), nombreTrim), LanguageManager_90DI.T_90DI("familias_msg_nombre_dup_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                tempFamilia.Nombre_90DI = nombreTrim;
                tempFamilia.SubFamilias_90DI = FamiliasHijas;
                response = _rolBLL.UpdateFamilia_90DI(tempFamilia);
                if (response)
                    MessageBox.Show(LanguageManager_90DI.T_90DI("familias_msg_modificada"), LanguageManager_90DI.T_90DI("familias_msg_exito_title"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(LanguageManager_90DI.T_90DI("familias_msg_error_modificar"), LanguageManager_90DI.T_90DI("familias_msg_error_title"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            FamiliasHijas = new List<Familia_90DI>();
            txtNombreFamilia.Text = "";
        }

        private void lstFamilias_SelectedValueChanged(object sender, EventArgs e)
        {
            if (lstFamilias.SelectedItem is not Familia_90DI familia) return;
            if (rdbCrearFamilia.Checked) return;

            var familiaCompleta = _rolBLL.GetFamiliaCompleta_90DI(familia.IdFamilia_90DI);
            if (familiaCompleta == null) return;

            tempFamilia = familiaCompleta;

            // Patentes de la familia
            LstFamiliasPatentes.DataSource = null;
            LstFamiliasPatentes.DataSource = tempFamilia.Patentes_90DI;
            LstFamiliasPatentes.DisplayMember = displayMember;

            // Subfamilias: las cargamos completas para tener sus patentes a mano
            FamiliasHijas = tempFamilia.SubFamilias_90DI
                .Select(f => _rolBLL.GetFamiliaCompleta_90DI(f.IdFamilia_90DI) ?? f)
                .ToList();
            lstFamiliasFamilia.DataSource = null;
            lstFamiliasFamilia.DataSource = FamiliasHijas;
            lstFamiliasFamilia.DisplayMember = displayMember;

            // Si está editando: cargamos sus patentes en lstPatentes y sacamos
            // del combo la propia familia (no puede ser hija de sí misma)
            if (rdbModificarFamilia.Checked)
            {
                // sacamos las que ya tiene
                var idsEnFamilia = tempFamilia.Patentes_90DI.Select(p => p.IdPatente_90DI).ToHashSet();
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

        // Agregar / quitar familias hijas (alta y modificación)
        private void button2_Click(object sender, EventArgs e)
        {
            if (ComboFamilia == null || ComboFamilia.IdFamilia_90DI == 0) return;
            if (FamiliasHijas.Any(f => f.IdFamilia_90DI == ComboFamilia.IdFamilia_90DI)) return;

            var familiaCompleta = _rolBLL.GetFamiliaCompleta_90DI(ComboFamilia.IdFamilia_90DI);
            if (familiaCompleta == null) return;

            // No dejar si la subfamilia trae una patente que ya está asignada directo
            foreach (var p in familiaCompleta.Patentes_90DI)
            {
                if (tempFamilia.Patentes_90DI.Any(dp => dp.IdPatente_90DI == p.IdPatente_90DI))
                {
                    MessageBox.Show(
                        string.Format(LanguageManager_90DI.T_90DI("familias_msg_patente_dup"), p.Nombre_90DI, familiaCompleta.Nombre_90DI),
                        LanguageManager_90DI.T_90DI("familias_msg_patente_dup_title"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Ni si comparte alguna patente con otra subfamilia ya agregada
            foreach (var hija in FamiliasHijas)
            {
                var idsHija = hija.Patentes_90DI.Select(p => p.IdPatente_90DI).ToHashSet();
                foreach (var p in familiaCompleta.Patentes_90DI)
                {
                    if (idsHija.Contains(p.IdPatente_90DI))
                    {
                        MessageBox.Show(
                            string.Format(LanguageManager_90DI.T_90DI("familias_msg_subfamilia_dup"), p.Nombre_90DI, hija.Nombre_90DI, familiaCompleta.Nombre_90DI),
                            LanguageManager_90DI.T_90DI("familias_msg_subfamilia_dup_title"),
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            FamiliasHijas.Add(familiaCompleta);
            lstFamiliasFamilia.DataSource = null;
            lstFamiliasFamilia.DataSource = FamiliasHijas;
            lstFamiliasFamilia.DisplayMember = displayMember;

            RefrescarPatentesDisponibles();
        }

        // Muestra todas las patentes menos las que la familia ya tiene directo. Las de las
        // subfamilias se siguen viendo; si intenta agregarlas salta el aviso.
        private void RefrescarPatentesDisponibles()
        {
            var idsDirectos = tempFamilia.Patentes_90DI.Select(p => p.IdPatente_90DI).ToHashSet();

            PatentesBd = _rolBLL.GetAllPatentes_90DI()
                .Where(p => !idsDirectos.Contains(p.IdPatente_90DI))
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

            RefrescarPatentesDisponibles(); // vuelven las patentes de la subfamilia que se quitó
        }

        private void btnQuitarPatente_Click(object sender, EventArgs e)
        {
            if (tempPatente == null || tempPatente.IdPatente_90DI == 0) return;

            tempFamilia.Patentes_90DI.Remove(tempPatente);
            PatentesBd.Add(tempPatente);  // vuelve a la lista de disponibles

            lstPatentes.DataSource = null;
            lstPatentes.DataSource = PatentesBd;
            lstPatentes.DisplayMember = displayMember;

            LstFamiliasPatentes.DataSource = null;
            LstFamiliasPatentes.DataSource = tempFamilia.Patentes_90DI;
            LstFamiliasPatentes.DisplayMember = displayMember;

            // Actualizamos tempPatente con lo que quedó seleccionado, así se pueden quitar
            // varias seguidas sin volver a hacer click.
            tempPatente = LstFamiliasPatentes.SelectedItem as Patente_90DI ?? new Patente_90DI();
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

        private void btnEliminarFamilia_Click(object sender, EventArgs e)
        {
            if (tempFamilia == null || tempFamilia.IdFamilia_90DI == 0) return;

            if (_rolBLL.FamiliaEstaEnRol_90DI(tempFamilia.IdFamilia_90DI))
            {
                MessageBox.Show(
                    string.Format(LanguageManager_90DI.T_90DI("familias_msg_en_rol"), tempFamilia.Nombre_90DI),
                    LanguageManager_90DI.T_90DI("familias_msg_en_rol_title"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                string.Format(LanguageManager_90DI.T_90DI("familias_msg_confirm_eliminar"), tempFamilia.Nombre_90DI),
                LanguageManager_90DI.T_90DI("familias_msg_confirm_eliminar_title"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            bool response = _rolBLL.DeleteFamilia_90DI(tempFamilia.IdFamilia_90DI, tempFamilia.Nombre_90DI);
            MessageBox.Show(
                response ? LanguageManager_90DI.T_90DI("familias_msg_eliminada") : LanguageManager_90DI.T_90DI("familias_msg_error_eliminar"),
                response ? LanguageManager_90DI.T_90DI("familias_msg_exito_title") : LanguageManager_90DI.T_90DI("familias_msg_error_title"),
                MessageBoxButtons.OK,
                response ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            rdbModoConsulta.Checked = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            var frmMenu = new FrmMenu_90DI();
            frmMenu.Show();
            this.Close();
            
        }
    }
}
