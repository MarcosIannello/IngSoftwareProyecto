using BLL;
using BLL_90DI;
using DAL;
using Service_90DI;
using Services_90DI;
using Services_90DI.entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace UI_90DI
{
    public partial class FrmGestionUsuarios : Form, IObserver_90DI
    {
        UsersBLL_90DI _users      = new UsersBLL_90DI();
        BitacoraBLL_90DI _bitacora   = new BitacoraBLL_90DI();
        RolBLL_90DI _rolesBLL         = new RolBLL_90DI();
        List<User_90DI> usersList     = new List<User_90DI>();
        List<Rol_90DI> rolesList      = new List<Rol_90DI>();
        User_90DI newUser             = new User_90DI();
        private readonly FrmMenu_90DI _menu;

      

        private string _modoKey = "users_mode_consulta";

        User_90DI temp;
        bool createMode = false;
        bool editMode = false;
        bool viewMode = true;
        bool unblockMode = false;
        bool activateMode = false;

        public FrmGestionUsuarios(FrmMenu_90DI menu)
        {
            _menu = menu;
            InitializeComponent();

            // Grilla de solo lectura
            dataGridUsers.ReadOnly              = true;
            dataGridUsers.AllowUserToAddRows    = false;
            dataGridUsers.AllowUserToDeleteRows = false;
            dataGridUsers.SelectionMode         = DataGridViewSelectionMode.CellSelect;
            dataGridUsers.MultiSelect           = false;
            // Para copiar la celda con Ctrl+C
            dataGridUsers.ClipboardCopyMode     = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;

            GetUsers();
            LoadRoles();
            EnableQueryFields();
            CleanForm();
            rdbActive.Checked = true;
            btnCancelar.Enabled = false;
            txtActiveMode.Text = LanguageManager_90DI.T(_modoKey);

            LanguageManager_90DI.Current.AddObserver_90DI(this);
            var t = LanguageManager_90DI.Current.TraduccionesActuales_90DI;
            if (t.Count > 0) UpdateLanguage_90DI(t);
        }

        public void UpdateLanguage_90DI(Dictionary<string, string> traducciones)
        {
            if (traducciones.TryGetValue("users_title",          out var v)) label1.Text          = v;
            if (traducciones.TryGetValue("users_btn_crear",      out v))     btnCrear.Text         = v;
            if (traducciones.TryGetValue("users_btn_desbloquear",out v))     btnDesbloquear.Text   = v;
            if (traducciones.TryGetValue("users_btn_modificar",  out v))     btnModificar.Text     = v;
            if (traducciones.TryGetValue("users_btn_activar",    out v))     btnActive.Text        = v;
            if (traducciones.TryGetValue("users_btn_aplicar",    out v))     btnAplicar.Text       = v;
            if (traducciones.TryGetValue("users_btn_cancelar",   out v))     btnCancelar.Text      = v;
            if (traducciones.TryGetValue("users_btn_salir",      out v))     button1.Text          = v;
            if (traducciones.TryGetValue("users_lbl_dni",        out v))     label2.Text           = v;
            if (traducciones.TryGetValue("users_lbl_apellido",   out v))     label3.Text           = v;
            if (traducciones.TryGetValue("users_lbl_email",      out v))     label4.Text           = v;
            if (traducciones.TryGetValue("users_lbl_nombre",     out v))     label5.Text           = v;
            if (traducciones.TryGetValue("users_lbl_login",      out v))     label6.Text           = v;
            if (traducciones.TryGetValue("users_lbl_rol",        out v))     label7.Text           = v;
            if (traducciones.TryGetValue("users_lbl_bloqueado",  out v))     label9.Text           = v;
            if (traducciones.TryGetValue("users_lbl_activo",     out v))     label8.Text           = v;
            if (traducciones.TryGetValue("users_lbl_modo",       out v))     label10.Text          = v;
            if (traducciones.TryGetValue("users_chk_block",      out v))     chkBlock.Text         = v;
            if (traducciones.TryGetValue("users_chk_activo",     out v))     chkActiveUSer.Text    = v;
            if (traducciones.TryGetValue("users_rdb_activos",    out v))     rdbActive.Text        = v;
            if (traducciones.TryGetValue("users_rdb_todos",      out v))     rdbTodos.Text         = v;
            if (traducciones.TryGetValue("users_lbl_cant",       out v))     lblCantUsers.Text     = v + (usersList?.Count ?? 0);
            txtActiveMode.Text = LanguageManager_90DI.T(_modoKey);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            LanguageManager_90DI.Current.Unsubscribe_90DI(this);
            base.OnFormClosed(e);
        }

        private void CrudUsers_Load(object sender, EventArgs e) { }
        private void radioButton1_CheckedChanged(object sender, EventArgs e) { }
        private void textBox4_TextChanged(object sender, EventArgs e) { }

        // ─── Helpers de estado de botones ───────────────────────────────────

        private void SetActionButtons(bool enabled)
        {
            btnCrear.Enabled       = enabled;
            btnModificar.Enabled   = enabled;
            btnDesbloquear.Enabled = enabled;
            btnActive.Enabled      = enabled;
        }

        private void GoToConsultaMode()
        {
            SetMode();
            EnableQueryFields();
            CleanForm();
            btnCancelar.Enabled = false;
            SetActionButtons(true);
            rdbActive.Checked = true;
            _modoKey = "users_mode_consulta";
            txtActiveMode.Text = LanguageManager_90DI.T(_modoKey);
        }

        // ─── Modos de pantalla ───────────────────────────────────────────────

        private void SetMode(bool create = false, bool edit = false, bool unblock = false, bool activate = false)
        {
            createMode   = create;
            editMode     = edit;
            unblockMode  = unblock;
            activateMode = activate;
            viewMode     = !create && !edit && !unblock && !activate;
        }

        // ─── Habilitación de campos ──────────────────────────────────────────

        public void BlockForm()
        {
            txtApellido.Enabled   = false;
            txtDni.Enabled        = false;
            txtEmail.Enabled      = false;
            txtLogin.Enabled      = false;
            txtNombre.Enabled     = false;
            cmbRolActual.Enabled  = false;
            chkActiveUSer.Enabled = false;
            chkBlock.Enabled      = false;
        }

        public void EnableQueryFields()
        {
            txtApellido.Enabled   = true;
            txtDni.Enabled        = true;
            txtEmail.Enabled      = true;
            txtLogin.Enabled      = true;
            txtNombre.Enabled     = true;
            cmbRolActual.Enabled  = false; // en consulta el rol no se toca
            chkActiveUSer.Enabled = false;
            chkBlock.Enabled      = false;
        }

        public void UnblockForm()
        {
            txtApellido.Enabled   = true;
            txtDni.Enabled        = true;
            txtEmail.Enabled      = true;
            txtLogin.Enabled      = true;
            txtNombre.Enabled     = true;
            cmbRolActual.Enabled  = true;
            chkActiveUSer.Enabled = true;
            chkBlock.Enabled      = true;
        }

        public void CleanForm()
        {
            txtApellido.Text      = "";
            txtDni.Text           = "";
            txtEmail.Text         = "";
            txtLogin.Text         = "";
            txtNombre.Text        = "";
            cmbRolActual.SelectedIndex = rolesList.Count > 0 ? 0 : -1;
            chkActiveUSer.Checked = false;
            chkBlock.Checked      = false;
        }

        // ─── Validación ──────────────────────────────────────────────────────

        private bool ValidateForm(int excluirId = 0)
        {
            if (string.IsNullOrWhiteSpace(txtLogin.Text))
            {
                MessageBox.Show(LanguageManager_90DI.T("users_msg_val_login_empty"), LanguageManager_90DI.T("users_msg_val_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLogin.Focus();
                return false;
            }
            if (!Regex.IsMatch(txtDni.Text.Trim(), @"^\d+$"))
            {
                MessageBox.Show(LanguageManager_90DI.T("users_msg_val_dni_numbers"), LanguageManager_90DI.T("users_msg_val_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDni.Focus();
                return false;
            }
            bool dniDuplicado = usersList.Any(u => u.DNI_90DI == txtDni.Text.Trim() && u.IdUsuario_90DI != excluirId);
            if (dniDuplicado)
            {
                MessageBox.Show(LanguageManager_90DI.T("users_msg_val_dni_dup"), LanguageManager_90DI.T("users_msg_dni_dup_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDni.Focus();
                return false;
            }
            if (!Regex.IsMatch(txtEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show(LanguageManager_90DI.T("users_msg_val_email"), LanguageManager_90DI.T("users_msg_val_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }
            return true;
        }

        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        // Arma el login como nombre+apellido (solo al crear)
        private void AutoFillLogin()
        {
            if (!createMode) return;
            txtLogin.Text = (txtNombre.Text.Trim() + txtApellido.Text.Trim())
                            .Replace(" ", "")
                            .ToLower();
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)   => AutoFillLogin();
        private void txtApellido_TextChanged(object sender, EventArgs e) => AutoFillLogin();

        // ─── Grilla ──────────────────────────────────────────────────────────

        public void GetUsers()
        {
            usersList = _users.GetAllUsers_90DI();
            AplicarFiltroGrid();
        }

        // Filtra la grilla según el radio. Lo llamamos a mano: si rdbActive ya
        // estaba tildado, re-tildarlo no dispara el evento y quedarían todos.
        private void AplicarFiltroGrid()
        {
            if (rdbTodos.Checked)
                RefreshGrid(usersList);
            else
                RefreshGrid(usersList.Where(u => u.Activo_90DI).ToList());
        }

        // Carga los roles en el combo (el value es el IdRol)
        private void LoadRoles()
        {
            rolesList = _rolesBLL.GetAllRoles_90DI();
            cmbRolActual.DisplayMember = "Nombre_90DI";
            cmbRolActual.ValueMember   = "IdRol_90DI";
            cmbRolActual.DataSource    = rolesList;
            cmbRolActual.SelectedIndex = rolesList.Count > 0 ? 0 : -1;
        }

        // IdRol elegido en el combo, como string
        private string GetSelectedRolId()
        {
            return cmbRolActual.SelectedValue?.ToString() ?? "0";
        }

        private void RefreshGrid(List<User_90DI> list)
        {
            dataGridUsers.DataSource = list;
            lblCantUsers.Text = "Numero Usuarios: " + list.Count;

            // Pintar inactivos de rojo
            foreach (DataGridViewRow row in dataGridUsers.Rows)
            {
                var user = row.DataBoundItem as User_90DI;
                if (user != null && !user.Activo_90DI)
                    row.DefaultCellStyle.BackColor = Color.LightCoral;
                else
                    row.DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void dataGridUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            temp = (User_90DI)dataGridUsers.CurrentRow.DataBoundItem;
            CompleteForm(temp);
        }

        private void CompleteForm(User_90DI user)
        {
            txtApellido.Text      = user.Apellidos_90DI;
            txtDni.Text           = user.DNI_90DI;
            txtLogin.Text         = user.NombreUsuario_90DI;
            txtNombre.Text        = user.Nombre_90DI;
            // Pone el combo en el rol del usuario
            if (int.TryParse(user.Rol_90DI, out var idRolUser))
                cmbRolActual.SelectedValue = idRolUser;
            else
                cmbRolActual.SelectedIndex = rolesList.Count > 0 ? 0 : -1;
            txtEmail.Text         = user.Email_90DI;
            chkActiveUSer.Checked = user.Activo_90DI;
            chkBlock.Checked      = user.Bloqueo_90DI;
            btnDesbloquear.Enabled = user.Bloqueo_90DI;
        }

        // ─── Radio buttons ───────────────────────────────────────────────────

        private void rdbActive_CheckedChanged(object sender, EventArgs e)
        {
            if (!rdbActive.Checked) return;
            AplicarFiltroGrid();
        }

        private void rdbTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (!rdbTodos.Checked) return;
            AplicarFiltroGrid();
        }

        // ─── Botones de acción ───────────────────────────────────────────────

        private void btnCrear_Click(object sender, EventArgs e)
        {
            SetMode(create: true);
            UnblockForm();
            CleanForm();
            btnCancelar.Enabled = true;
            SetActionButtons(false);
            btnCrear.Enabled = false;
            _modoKey = "users_mode_crear";
            txtActiveMode.Text = LanguageManager_90DI.T(_modoKey);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (temp == null) { MessageBox.Show(LanguageManager_90DI.T("users_msg_select_first"), LanguageManager_90DI.T("users_msg_aviso_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            SetMode(edit: true);
            UnblockForm();
            btnCancelar.Enabled = true;
            SetActionButtons(false);
            _modoKey = "users_mode_editar";
            txtActiveMode.Text = LanguageManager_90DI.T(_modoKey);
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            if (temp == null) { MessageBox.Show(LanguageManager_90DI.T("users_msg_select_first"), LanguageManager_90DI.T("users_msg_aviso_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            SetMode(unblock: true);
            btnCancelar.Enabled = true;
            SetActionButtons(false);
            _modoKey = "users_mode_desbloquear";
            txtActiveMode.Text = LanguageManager_90DI.T(_modoKey);
        }

        private void btnActive_Click(object sender, EventArgs e)
        {
            if (temp == null) { MessageBox.Show(LanguageManager_90DI.T("users_msg_select_first"), LanguageManager_90DI.T("users_msg_aviso_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            SetMode(activate: true);
            btnCancelar.Enabled = true;
            SetActionButtons(false);
            _modoKey = "users_mode_activar";
            txtActiveMode.Text = LanguageManager_90DI.T(_modoKey);
        }

        // ─── Aplicar ─────────────────────────────────────────────────────────

        public bool createUser()
        {
            if (!ValidateForm()) return false;

            newUser = new User_90DI
            {
                IdUsuario_90DI     = 0,
                NombreUsuario_90DI = txtLogin.Text,
                DNI_90DI           = txtDni.Text,
                Apellidos_90DI     = txtApellido.Text,
                Nombre_90DI        = txtNombre.Text,
                Activo_90DI        = chkActiveUSer.Checked,
                FechaAlta_90DI     = DateTime.Now,
                Rol_90DI           = GetSelectedRolId(), // rol elegido en el combo
                Bloqueo_90DI       = chkBlock.Checked,
                Password_90DI      = txtDni.Text + txtApellido.Text,  // DNI + Apellido
                Email_90DI         = txtEmail.Text
            };

            var response = _users.CreateUser_90DI(newUser);

            if (response)
            {
                GetUsers();
                return response;
            }
            return false;
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            try
            {
                if (viewMode)
                {
                    // En consulta solo filtra, no toca la BD
                    rdbActive.Checked = false;
                    rdbTodos.Checked  = false;
                    searchByQuery();
                    return;
                }

                var response = false;

                if (createMode)
                {
                    response = createUser();
                    
                    
                }
                else if (editMode)
                {
                    if (!ValidateForm(temp.IdUsuario_90DI)) return;
                    temp.NombreUsuario_90DI = txtLogin.Text;
                    temp.DNI_90DI           = txtDni.Text;
                    temp.Nombre_90DI        = txtNombre.Text;
                    temp.Apellidos_90DI     = txtApellido.Text;
                    temp.Activo_90DI        = chkActiveUSer.Checked;
                    temp.Bloqueo_90DI       = chkBlock.Checked;
                    temp.Email_90DI         = txtEmail.Text;
                    temp.Rol_90DI           = GetSelectedRolId(); // rol elegido en el combo
                    response = _users.UpdateUser_90DI(temp);
                    
                    GetUsers();
                }
                else if (unblockMode)
                {
                    var confirm = MessageBox.Show(
                        string.Format(LanguageManager_90DI.T("users_msg_confirm_desbloquear"), temp.NombreUsuario_90DI),
                        LanguageManager_90DI.T("users_msg_confirm_desbloquear_title"),
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (confirm != DialogResult.Yes) return;
                    response = _users.UnblockUser_90DI(temp.IdUsuario_90DI);
                    GetUsers();
                }
                else if (activateMode)
                {
                    var accion = temp.Activo_90DI
                        ? LanguageManager_90DI.T("users_msg_desactivar")
                        : LanguageManager_90DI.T("users_msg_activar");
                    var confirm = MessageBox.Show(
                        string.Format(LanguageManager_90DI.T("users_msg_confirm_activar"), accion, temp.NombreUsuario_90DI),
                        LanguageManager_90DI.T("users_msg_confirm_activar_title"),
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (confirm != DialogResult.Yes) return;
                    response = _users.ActivateUser_90DI(temp);
                    GetUsers();
                }

                if (response)
                {
                    MessageBox.Show(LanguageManager_90DI.T("users_msg_success"), LanguageManager_90DI.T("users_msg_success_title"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GoToConsultaMode();
                }
            }
            catch (InvalidOperationException ex)
            {
                // Errores de negocio (ej: DNI repetido)
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Cancelar / Salir ────────────────────────────────────────────────

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            GoToConsultaMode();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _menu.Show();
            this.Close();
        }

        private void CrudUsers_FormClosed(object sender, FormClosedEventArgs e)
        {
            _menu.Show();
        }

        // ─── Búsqueda LINQ ───────────────────────────────────────────────────

        public void searchByQuery()
        {
            var filtered = usersList.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(txtDni.Text))
                filtered = filtered.Where(u => u.DNI_90DI.Contains(txtDni.Text.Trim()));

            if (!string.IsNullOrWhiteSpace(txtApellido.Text))
                filtered = filtered.Where(u => u.Apellidos_90DI.Contains(txtApellido.Text.Trim(), StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(txtNombre.Text))
                filtered = filtered.Where(u => u.Nombre_90DI.Contains(txtNombre.Text.Trim(), StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
                filtered = filtered.Where(u => u.Email_90DI.Contains(txtEmail.Text.Trim(), StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(txtLogin.Text))
                filtered = filtered.Where(u => u.NombreUsuario_90DI.Contains(txtLogin.Text.Trim(), StringComparison.OrdinalIgnoreCase));

            RefreshGrid(filtered.ToList());
        }
    }
}
