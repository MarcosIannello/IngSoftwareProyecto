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

            // Wiring de eventos (el Designer de Familias no los suscribe, a diferencia
            // del de Roles). Debe ir antes de marcar el modo inicial para que la
            // selección inicial dispare las cargas correspondientes.
            rdbModoConsulta.CheckedChanged     += rdbModoConsulta_CheckedChanged;
            rdbModificarFamilia.CheckedChanged += rdbModificarFamilia_CheckedChanged;
            rdbCrearFamilia.CheckedChanged     += rdbCrearFamilia_CheckedChanged;

            btnAgregarPatente.Click  += btnAgregarPatente_Click;
            btnQuitarPatente.Click   += btnQuitarPatente_Click;
            btnAplicarCambios.Click  += btnAplicarCambios_Click;
            btnEliminarFamilia.Click += btnEliminarFamilia_Click;
            button2.Click            += button2_Click; // Agregar Familia
            button1.Click            += button1_Click; // Quitar Familia

            lstFamilias.SelectedValueChanged         += lstFamilias_SelectedValueChanged;
            lstPatentes.SelectedIndexChanged         += lstPatentes_SelectedIndexChanged;
            LstFamiliasPatentes.SelectedIndexChanged += LstFamiliasPatentes_SelectedIndexChanged;
            lstFamiliasFamilia.SelectedIndexChanged  += lstFamiliasFamilia_SelectedIndexChanged;
            cmbFamiliasDisponibles.SelectedIndexChanged += cmbFamiliasDisponibles_SelectedIndexChanged;

            PatentesBd = _rolBLL.GetAllPatentes_90DI();
            rdbModoConsulta.Checked = true;

            LanguageManager_90DI.Current.AddObserver_90DI(this);
            var t = LanguageManager_90DI.Current.TraduccionesActuales_90DI;
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
            LanguageManager_90DI.Current.Unsubscribe_90DI(this);
            base.OnFormClosed(e);
        }

        private void FrmAdminRoles_90DI_Load(object sender, EventArgs e)
        {
            UI_90DI.LayoutHelper_90DI.AjustarAPantalla_90DI(this);
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
            btnEliminarFamilia.Visible = false;
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
            if (tempFamilia.Patentes.Any(p => p.IdPatente_90DI == tempPatente.IdPatente_90DI)) return;

            // Bloquear si la patente ya está cubierta por alguna subfamilia asignada
            var hijaConflicto = FamiliasHijas.FirstOrDefault(f => f.Patentes.Any(p => p.IdPatente_90DI == tempPatente.IdPatente_90DI));
            if (hijaConflicto != null)
            {
                MessageBox.Show(
                    string.Format(LanguageManager_90DI.T("familias_msg_patente_dup"), tempPatente.Nombre_90DI, hijaConflicto.Nombre_90DI),
                    LanguageManager_90DI.T("familias_msg_patente_dup_title"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            tempFamilia.Patentes.Add(tempPatente);
            PatentesBd.Remove(tempPatente);

            lstPatentes.DataSource = null;
            lstPatentes.DataSource = PatentesBd;
            lstPatentes.DisplayMember = displayMember;
            if (lstPatentes.Items.Count > 0) lstPatentes.SelectedIndex = 0;

            // Sincronizar tempPatente con el ítem mostrado: al re-bindear el combo
            // ya auto-selecciona el índice 0, por lo que SelectedIndex = 0 no dispara
            // SelectedIndexChanged y tempPatente quedaría con la patente anterior.
            tempPatente = lstPatentes.SelectedItem as Patente_90DI ?? new Patente_90DI();

            LstFamiliasPatentes.DataSource = null;
            LstFamiliasPatentes.DataSource = tempFamilia.Patentes;
            LstFamiliasPatentes.DisplayMember = displayMember;
        }

        // Retorna mensaje de error si hay patentes redundantes entre directas y subfamilias,
        // o entre subfamilias entre sí. Null = sin conflicto.
        private string? ValidarRedundanciaFamilia()
        {
            // Patente directa vs subfamilia
            foreach (var hija in FamiliasHijas)
            {
                foreach (var p in hija.Patentes)
                {
                    if (tempFamilia.Patentes.Any(dp => dp.IdPatente_90DI == p.IdPatente_90DI))
                        return string.Format(LanguageManager_90DI.T("familias_msg_patente_dup"), p.Nombre_90DI, hija.Nombre_90DI);
                }
            }
            // Patente compartida entre dos subfamilias
            for (int i = 0; i < FamiliasHijas.Count; i++)
            {
                var idsI = FamiliasHijas[i].Patentes.Select(p => p.IdPatente_90DI).ToHashSet();
                for (int j = i + 1; j < FamiliasHijas.Count; j++)
                {
                    foreach (var p in FamiliasHijas[j].Patentes)
                    {
                        if (idsI.Contains(p.IdPatente_90DI))
                            return string.Format(LanguageManager_90DI.T("familias_msg_subfamilia_dup"), p.Nombre_90DI, FamiliasHijas[i].Nombre_90DI, FamiliasHijas[j].Nombre_90DI);
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
                    MessageBox.Show(LanguageManager_90DI.T("familias_msg_nombre_empty"), LanguageManager_90DI.T("familias_msg_nombre_empty_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (tempFamilia.Patentes.Count == 0 && FamiliasHijas.Count == 0)
                {
                    MessageBox.Show(LanguageManager_90DI.T("familias_msg_contenido_empty"), LanguageManager_90DI.T("familias_msg_nombre_empty_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var conflicto = ValidarRedundanciaFamilia();
                if (conflicto != null)
                {
                    MessageBox.Show(conflicto, LanguageManager_90DI.T("familias_msg_patente_dup_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string nombreTrim = txtNombreFamilia.Text.Trim();
                bool nombreDuplicado = _rolBLL.GetAllFamilias_90DI()
                    .Any(f => f.Nombre_90DI.Equals(nombreTrim, StringComparison.OrdinalIgnoreCase));
                if (nombreDuplicado)
                {
                    MessageBox.Show(string.Format(LanguageManager_90DI.T("familias_msg_nombre_dup"), nombreTrim), LanguageManager_90DI.T("familias_msg_nombre_dup_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                tempFamilia.Nombre_90DI = nombreTrim;
                tempFamilia.SubFamilias = FamiliasHijas;
                response = _rolBLL.CreateFamilia_90DI(tempFamilia);

                if (response)
                    MessageBox.Show(LanguageManager_90DI.T("familias_msg_creada"), LanguageManager_90DI.T("familias_msg_exito_title"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(LanguageManager_90DI.T("familias_msg_error_crear"), LanguageManager_90DI.T("familias_msg_error_title"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (rdbModificarFamilia.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtNombreFamilia.Text))
                {
                    MessageBox.Show(LanguageManager_90DI.T("familias_msg_nombre_empty"), LanguageManager_90DI.T("familias_msg_nombre_empty_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (tempFamilia.Patentes.Count == 0 && FamiliasHijas.Count == 0)
                {
                    MessageBox.Show(LanguageManager_90DI.T("familias_msg_contenido_empty"), LanguageManager_90DI.T("familias_msg_nombre_empty_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var conflictoMod = ValidarRedundanciaFamilia();
                if (conflictoMod != null)
                {
                    MessageBox.Show(conflictoMod, LanguageManager_90DI.T("familias_msg_patente_dup_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string nombreTrim = txtNombreFamilia.Text.Trim();
                bool nombreDuplicado = _rolBLL.GetAllFamilias_90DI()
                    .Any(f => f.Nombre_90DI.Equals(nombreTrim, StringComparison.OrdinalIgnoreCase)
                           && f.IdFamilia_90DI != tempFamilia.IdFamilia_90DI);
                if (nombreDuplicado)
                {
                    MessageBox.Show(string.Format(LanguageManager_90DI.T("familias_msg_nombre_dup2"), nombreTrim), LanguageManager_90DI.T("familias_msg_nombre_dup_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                tempFamilia.Nombre_90DI = nombreTrim;
                tempFamilia.SubFamilias = FamiliasHijas;
                response = _rolBLL.UpdateFamilia_90DI(tempFamilia);
                if (response)
                    MessageBox.Show(LanguageManager_90DI.T("familias_msg_modificada"), LanguageManager_90DI.T("familias_msg_exito_title"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(LanguageManager_90DI.T("familias_msg_error_modificar"), LanguageManager_90DI.T("familias_msg_error_title"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            // mostrar sub-familias — cargar cada una completa para tener sus Patentes disponibles
            FamiliasHijas = tempFamilia.SubFamilias
                .Select(f => _rolBLL.GetFamiliaCompleta_90DI(f.IdFamilia_90DI) ?? f)
                .ToList();
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
            if (ComboFamilia == null || ComboFamilia.IdFamilia_90DI == 0) return;
            if (FamiliasHijas.Any(f => f.IdFamilia_90DI == ComboFamilia.IdFamilia_90DI)) return;

            var familiaCompleta = _rolBLL.GetFamiliaCompleta_90DI(ComboFamilia.IdFamilia_90DI);
            if (familiaCompleta == null) return;

            // Bloquear si la subfamilia contiene una patente ya asignada directamente
            foreach (var p in familiaCompleta.Patentes)
            {
                if (tempFamilia.Patentes.Any(dp => dp.IdPatente_90DI == p.IdPatente_90DI))
                {
                    MessageBox.Show(
                        string.Format(LanguageManager_90DI.T("familias_msg_patente_dup"), p.Nombre_90DI, familiaCompleta.Nombre_90DI),
                        LanguageManager_90DI.T("familias_msg_patente_dup_title"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Bloquear si la subfamilia comparte una patente con otra subfamilia ya agregada
            foreach (var hija in FamiliasHijas)
            {
                var idsHija = hija.Patentes.Select(p => p.IdPatente_90DI).ToHashSet();
                foreach (var p in familiaCompleta.Patentes)
                {
                    if (idsHija.Contains(p.IdPatente_90DI))
                    {
                        MessageBox.Show(
                            string.Format(LanguageManager_90DI.T("familias_msg_subfamilia_dup"), p.Nombre_90DI, hija.Nombre_90DI, familiaCompleta.Nombre_90DI),
                            LanguageManager_90DI.T("familias_msg_subfamilia_dup_title"),
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

        // Muestra todas las patentes excepto las ya asignadas directamente a la familia.
        // Las patentes de subfamilias siguen visibles: el warning se dispara al intentar agregarlas.
        private void RefrescarPatentesDisponibles()
        {
            var idsDirectos = tempFamilia.Patentes.Select(p => p.IdPatente_90DI).ToHashSet();

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

            // Sincronizar tempPatente con la selección actual de la lista de asignadas,
            // para poder quitar varias seguidas sin tener que re-clickear.
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
                    string.Format(LanguageManager_90DI.T("familias_msg_en_rol"), tempFamilia.Nombre_90DI),
                    LanguageManager_90DI.T("familias_msg_en_rol_title"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                string.Format(LanguageManager_90DI.T("familias_msg_confirm_eliminar"), tempFamilia.Nombre_90DI),
                LanguageManager_90DI.T("familias_msg_confirm_eliminar_title"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            bool response = _rolBLL.DeleteFamilia_90DI(tempFamilia.IdFamilia_90DI, tempFamilia.Nombre_90DI);
            MessageBox.Show(
                response ? LanguageManager_90DI.T("familias_msg_eliminada") : LanguageManager_90DI.T("familias_msg_error_eliminar"),
                response ? LanguageManager_90DI.T("familias_msg_exito_title") : LanguageManager_90DI.T("familias_msg_error_title"),
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
