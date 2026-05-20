using BLL_90DI;
using DAL;
using Services_90DI;
using Service_90DI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using UI_90DI;

namespace UI_90DI
{
    public partial class FrmGestionUsuarios : Form
    {
        UsersBLL_90DI _users      = new UsersBLL_90DI();
        BitacoraBLL_90DI _bitacora   = new BitacoraBLL_90DI();
        List<User_90DI> usersList     = new List<User_90DI>();
        User_90DI newUser             = new User_90DI();
        private readonly FrmMenu_90DI _menu;

        private void RegistrarEvento(string evento, byte criticidad = 3)
        {
            _bitacora.CreateLogEvent_90DI(new Event_90DI
            {
                Login_90DI      = SessionManager_90DI.Instancia.UserName,
                Fecha_90DI      = DateTime.Now,
                Hora_90DI       = DateTime.Now.TimeOfDay,
                Modulo_90DI     = "Usuarios",
                Evento_90DI     = evento,
                Criticidad_90DI = criticidad
            });
        }
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
            GetUsers();
            EnableQueryFields();
            CleanForm();
            // Arrancar mostrando solo activos y con radio marcado
            rdbActive.Checked = true;
            btnCancelar.Enabled = false;
            txtActiveMode.Text = "Modo Consulta";
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
            txtActiveMode.Text = "Modo Consulta";
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
            txtRol.Enabled        = false;
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
            txtRol.Enabled        = true;
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
            txtRol.Enabled        = true;
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
            txtRol.Text           = "";
            chkActiveUSer.Checked = false;
            chkBlock.Checked      = false;
        }

        // ─── Validación ──────────────────────────────────────────────────────

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtLogin.Text))
            {
                MessageBox.Show("El nombre de usuario no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLogin.Focus();
                return false;
            }
            if (!Regex.IsMatch(txtDni.Text.Trim(), @"^\d+$"))
            {
                MessageBox.Show("El DNI solo puede contener números.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDni.Focus();
                return false;
            }
            if (!Regex.IsMatch(txtEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("El email no tiene un formato válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        // Auto-completar Login = Nombre + Apellido en modo creación
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
            RefreshGrid(usersList);
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
            txtRol.Text           = user.Rol_90DI;
            txtEmail.Text         = user.Email_90DI;
            chkActiveUSer.Checked = user.Activo_90DI;
            chkBlock.Checked      = user.Bloqueo_90DI;
            btnDesbloquear.Enabled = user.Bloqueo_90DI;
        }

        // ─── Radio buttons ───────────────────────────────────────────────────

        private void rdbActive_CheckedChanged(object sender, EventArgs e)
        {
            if (!rdbActive.Checked) return;
            var filtered = usersList.Where(u => u.Activo_90DI).ToList();
            RefreshGrid(filtered);
        }

        private void rdbTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (!rdbTodos.Checked) return;
            RefreshGrid(usersList);
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
            txtActiveMode.Text = "Creando Usuario";
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (temp == null) { MessageBox.Show("Seleccione un usuario primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            SetMode(edit: true);
            UnblockForm();
            btnCancelar.Enabled = true;
            SetActionButtons(false);
            txtActiveMode.Text = "Editando Usuario";
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            if (temp == null) { MessageBox.Show("Seleccione un usuario primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            SetMode(unblock: true);
            btnCancelar.Enabled = true;
            SetActionButtons(false);
            txtActiveMode.Text = "Desbloquear Usuario";
        }

        private void btnActive_Click(object sender, EventArgs e)
        {
            if (temp == null) { MessageBox.Show("Seleccione un usuario primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            SetMode(activate: true);
            btnCancelar.Enabled = true;
            SetActionButtons(false);
            txtActiveMode.Text = "Activar/Desactivar Usuario";
        }

        // ─── Aplicar ─────────────────────────────────────────────────────────

        public void createUser()
        {
            if (!ValidateForm()) return;

            newUser = new User_90DI
            {
                IdUsuario_90DI     = 0,
                NombreUsuario_90DI = txtLogin.Text,
                DNI_90DI           = txtDni.Text,
                Apellidos_90DI     = txtApellido.Text,
                Nombre_90DI        = txtNombre.Text,
                Activo_90DI        = chkActiveUSer.Checked,
                FechaAlta_90DI     = DateTime.Now,
                IdPerfil_90DI      = 1, // TODO: reemplazar cuando estén desarrollados los perfiles
                Rol_90DI           = "Admin", //CAMBIAR cuando este desarrollado roles!!!
                Bloqueo_90DI       = chkBlock.Checked,
                Password_90DI      = txtDni.Text + txtApellido.Text,  // DNI + Apellido
                Email_90DI         = txtEmail.Text
            };

            _users.CreateUser_90DI(newUser);
            GetUsers();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            try
            {
                if (viewMode)
                {
                    // En modo consulta solo filtra, sin ir a BD
                    rdbActive.Checked = false;
                    rdbTodos.Checked  = false;
                    searchByQuery();
                    return;
                }

                var response = false;

                if (createMode)
                {
                    createUser();
                    response = true;
                    if (response) RegistrarEvento("Crear Usuario", 2);
                }
                else if (editMode)
                {
                    if (!ValidateForm()) return;
                    temp.NombreUsuario_90DI = txtLogin.Text;
                    temp.DNI_90DI           = txtDni.Text;
                    temp.Nombre_90DI        = txtNombre.Text;
                    temp.Apellidos_90DI     = txtApellido.Text;
                    temp.Activo_90DI        = chkActiveUSer.Checked;
                    temp.Bloqueo_90DI       = chkBlock.Checked;
                    temp.Email_90DI         = txtEmail.Text;
                    response = _users.UpdateUser_90DI(temp);
                    if (response) RegistrarEvento($"Modificar Usuario: {temp.NombreUsuario_90DI}", 2);
                    GetUsers();
                }
                else if (unblockMode)
                {
                    var confirm = MessageBox.Show(
                        $"¿Desea desbloquear al usuario '{temp.NombreUsuario_90DI}'?",
                        "Confirmar desbloqueo",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (confirm != DialogResult.Yes) return;
                    response = _users.UnblockUser_90DI(temp.IdUsuario_90DI);
                    if (response) RegistrarEvento($"Desbloquear Usuario: {temp.NombreUsuario_90DI}", 1);
                    GetUsers();
                }
                else if (activateMode)
                {
                    var accion = temp.Activo_90DI ? "desactivar" : "activar";
                    var confirm = MessageBox.Show(
                        $"¿Desea {accion} al usuario '{temp.NombreUsuario_90DI}'?",
                        "Confirmar acción",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (confirm != DialogResult.Yes) return;
                    response = _users.ActivateUser_90DI(temp);
                    var accionLog = temp.Activo_90DI ? "Desactivar Usuario" : "Activar Usuario";
                    if (response) RegistrarEvento($"{accionLog}: {temp.NombreUsuario_90DI}", 1);
                    GetUsers();
                }

                if (response)
                {
                    MessageBox.Show("Operacion realizada con exito", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GoToConsultaMode();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al realizar la operacion: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (!string.IsNullOrWhiteSpace(txtRol.Text))
                filtered = filtered.Where(u => u.Rol_90DI.Contains(txtRol.Text.Trim(), StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(txtLogin.Text))
                filtered = filtered.Where(u => u.NombreUsuario_90DI.Contains(txtLogin.Text.Trim(), StringComparison.OrdinalIgnoreCase));

            RefreshGrid(filtered.ToList());
        }
    }
}
