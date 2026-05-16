
using NLog;
using Entities_90DI;

namespace DAL
{
    public class UsuariosDAL200MI
    {
        private readonly ConexionSql200MI _conexion = new ConexionSql200MI();
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public UsuariosDAL200MI() { }

        public List<User90DI> SearchUsers90DI(string? dni = null, string? apellidos = null, string? nombre = null, string? email = null, string? rol = null, string? login = null)
        {
            var result = new List<User90DI>();
            try
            {
                using (var con = _conexion.GetConnection())
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText =
                        """
                            SELECT IdUsuario_90DI, NombreUsuario_90DI, Clave_90DI,
                                   IdPerfil_90DI, Activo_90DI, FechaAlta_90DI,
                                   DNI_90DI, Apellidos_90DI, Nombre_90DI,
                                   Rol_90DI, Email_90DI, Bloquo_90DI
                            FROM Usuarios_90DI
                            WHERE (@dni       IS NULL OR DNI_90DI           LIKE '%' + @dni       + '%')
                              AND (@apellidos IS NULL OR Apellidos_90DI     LIKE '%' + @apellidos + '%')
                              AND (@nombre    IS NULL OR Nombre_90DI        LIKE '%' + @nombre    + '%')
                              AND (@email     IS NULL OR Email_90DI         LIKE '%' + @email     + '%')
                              AND (@rol       IS NULL OR CAST(Rol_90DI AS NVARCHAR) LIKE '%' + @rol + '%')
                              AND (@login     IS NULL OR NombreUsuario_90DI LIKE '%' + @login     + '%')
                        """;
                        cmd.Parameters.AddWithValue("@dni",       string.IsNullOrWhiteSpace(dni)       ? (object)DBNull.Value : dni.Trim());
                        cmd.Parameters.AddWithValue("@apellidos", string.IsNullOrWhiteSpace(apellidos) ? (object)DBNull.Value : apellidos.Trim());
                        cmd.Parameters.AddWithValue("@nombre",    string.IsNullOrWhiteSpace(nombre)    ? (object)DBNull.Value : nombre.Trim());
                        cmd.Parameters.AddWithValue("@email",     string.IsNullOrWhiteSpace(email)     ? (object)DBNull.Value : email.Trim());
                        cmd.Parameters.AddWithValue("@rol",       string.IsNullOrWhiteSpace(rol)       ? (object)DBNull.Value : rol.Trim());
                        cmd.Parameters.AddWithValue("@login",     string.IsNullOrWhiteSpace(login)     ? (object)DBNull.Value : login.Trim());

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new User90DI
                                {
                                    IdUsuario_90DI     = reader.GetInt32(0),
                                    NombreUsuario_90DI = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                    Clave_90DI         = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                    IdPerfil_90DI      = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                                    Activo_90DI        = reader.IsDBNull(4) ? false : reader.GetBoolean(4),
                                    FechaAlta_90DI     = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5),
                                    DNI_90DI           = reader.IsDBNull(6) ? "" : reader.GetString(6).Trim(),
                                    Apellidos_90DI     = reader.IsDBNull(7) ? "" : reader.GetString(7),
                                    Nombre_90DI        = reader.IsDBNull(8) ? "" : reader.GetString(8),
                                    Rol_90DI           = reader.IsDBNull(9) ? "0" : reader.GetInt32(9).ToString(),
                                    Email_90DI         = reader.IsDBNull(10) ? "" : reader.GetString(10),
                                    Bloqueo_90DI       = reader.IsDBNull(11) ? false : reader.GetBoolean(11)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return result;
        }

        public User90DI? GetUser90DI(string Username)
        {
            try
            {
                using (var con = _conexion.GetConnection())
                {
                    con.Open();

                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText =
                        """
                            SELECT IdUsuario_90DI, NombreUsuario_90DI, Clave_90DI,
                                   IdPerfil_90DI, Activo_90DI, FechaAlta_90DI,
                                   DNI_90DI, Apellidos_90DI, Nombre_90DI,
                                   Rol_90DI, Email_90DI, Bloquo_90DI
                            FROM Usuarios_90DI
                            WHERE NombreUsuario_90DI = @Username
                        """;
                        cmd.Parameters.AddWithValue("@Username", Username);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new User90DI
                                {
                                    IdUsuario_90DI = reader.GetInt32(0),
                                    NombreUsuario_90DI = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                    Clave_90DI = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                    IdPerfil_90DI = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                                    Activo_90DI = reader.IsDBNull(4) ? false : reader.GetBoolean(4),
                                    FechaAlta_90DI = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5),
                                    DNI_90DI = reader.IsDBNull(6) ? "" : reader.GetString(6).Trim(),
                                    Apellidos_90DI = reader.IsDBNull(7) ? "" : reader.GetString(7),
                                    Nombre_90DI = reader.IsDBNull(8) ? "" : reader.GetString(8),
                                    Rol_90DI = reader.IsDBNull(9) ? "0" : reader.GetInt32(9).ToString(),
                                    Email_90DI = reader.IsDBNull(10) ? "" : reader.GetString(10),
                                    Bloqueo_90DI = reader.IsDBNull(11) ? false : reader.GetBoolean(11)
                                };
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return null;
            }
        }

        public bool UpdatePassword90DI(int idUsuario, string hashPassword)
        {
            try
            {
                using (var con = _conexion.GetConnection())
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText =
                            """
                                Update Usuarios_90DI
                                set Clave_90DI = @password
                                where IdUsuario_90DI = @idUsuario
                            """;
                        cmd.Parameters.AddWithValue("@password", hashPassword);
                        cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                        var rows = cmd.ExecuteNonQuery();

                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
        }

        public bool blockUser90DI(int idUsuario)
        {
            try
            {
                using (var con = _conexion.GetConnection())
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText =
                            """
                                Update Usuarios_90DI
                                set Bloquo_90DI = 1
                                where IdUsuario_90DI = @idUsuario
                            """;
                        cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                        var rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
        }

        public bool ActivateUser90DI(int idUsuario)
        {
            try
            {
                using (var con = _conexion.GetConnection())
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText =
                            """
                                UPDATE Usuarios_90DI
                                SET Activo_90DI = 1
                                WHERE IdUsuario_90DI = @idUsuario
                            """;
                        cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                        var rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
        }

        public bool DesactivateUser90DI(int idUsuario)
        {
            try
            {
                using (var con = _conexion.GetConnection())
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText =
                            """
                                UPDATE Usuarios_90DI
                                SET Activo_90DI = 0
                                WHERE IdUsuario_90DI = @idUsuario
                            """;
                        cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                        var rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
        }

        public bool UnblockUser90DI(int idUsuario)
        {
            try
            {
                using (var con = _conexion.GetConnection())
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText =
                            """
                                UPDATE Usuarios_90DI
                                SET Bloquo_90DI = 0
                                WHERE IdUsuario_90DI = @idUsuario
                            """;
                        cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                        var rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
        }

        public bool EditUser90DI(User90DI usuario)
        {
            try
            {
                using (var con = _conexion.GetConnection())
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText =
                            """
                                UPDATE Usuarios_90DI
                                SET NombreUsuario_90DI = @nombreUsuario,
                                    IdPerfil_90DI      = @idPerfil,
                                    DNI_90DI           = @dni,
                                    Apellidos_90DI     = @apellidos,
                                    Nombre_90DI        = @nombre,
                                    Email_90DI         = @email
                                WHERE IdUsuario_90DI = @idUsuario
                            """;
                        cmd.Parameters.AddWithValue("@nombreUsuario", usuario.NombreUsuario_90DI);
                        cmd.Parameters.AddWithValue("@idPerfil",      usuario.IdPerfil_90DI);
                        cmd.Parameters.AddWithValue("@dni",           usuario.DNI_90DI);
                        cmd.Parameters.AddWithValue("@apellidos",     usuario.Apellidos_90DI);
                        cmd.Parameters.AddWithValue("@nombre",        usuario.Nombre_90DI);
                        cmd.Parameters.AddWithValue("@email",         usuario.Email_90DI);
                        cmd.Parameters.AddWithValue("@idUsuario",     usuario.IdUsuario_90DI);
                        var rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
        }

        public bool CreateUser90DI(User90DI usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.NombreUsuario_90DI))
                throw new ArgumentException("El nombre de usuario no puede estar vacío.");

            try
            {
                using (var con = _conexion.GetConnection())
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText =
                            """
                                INSERT INTO Usuarios_90DI
                                    (NombreUsuario_90DI, Clave_90DI, IdPerfil_90DI,
                                     Activo_90DI, FechaAlta_90DI, DNI_90DI,
                                     Apellidos_90DI, Nombre_90DI, Email_90DI, Bloquo_90DI, Rol_90DI)
                                VALUES
                                    (@nombreUsuario, @clave, @idPerfil,
                                     @activo, @fechaAlta, @dni,
                                     @apellidos, @nombre, @email, @bloqueado, @rol)
                            """;
                        cmd.Parameters.AddWithValue("@nombreUsuario", usuario.NombreUsuario_90DI);
                        cmd.Parameters.AddWithValue("@clave",         usuario.Clave_90DI);
                        cmd.Parameters.AddWithValue("@idPerfil",      usuario.IdPerfil_90DI);
                        cmd.Parameters.AddWithValue("@activo",        usuario.Activo_90DI);
                        cmd.Parameters.AddWithValue("@fechaAlta",     DateTime.Now);
                        cmd.Parameters.AddWithValue("@dni",           usuario.DNI_90DI);
                        cmd.Parameters.AddWithValue("@apellidos",     usuario.Apellidos_90DI);
                        cmd.Parameters.AddWithValue("@nombre",        usuario.Nombre_90DI);
                        cmd.Parameters.AddWithValue("@email",         usuario.Email_90DI);
                        cmd.Parameters.AddWithValue("@bloqueado",     usuario.Bloqueo_90DI);
                        cmd.Parameters.AddWithValue("@rol",           usuario.Rol_90DI);
                        var rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
        }

        public List<User90DI> GetAllUsers90DI()
        {
            var result = new List<User90DI>();
            try
            {
                using (var con = _conexion.GetConnection())
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText =
                        """
                            SELECT IdUsuario_90DI, NombreUsuario_90DI, Clave_90DI,
                                   IdPerfil_90DI, Activo_90DI, FechaAlta_90DI,
                                   DNI_90DI, Apellidos_90DI, Nombre_90DI,
                                   Rol_90DI, Email_90DI, Bloquo_90DI
                            FROM Usuarios_90DI
                        """;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new User90DI
                                {
                                    IdUsuario_90DI    = reader.GetInt32(0),
                                    NombreUsuario_90DI = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                    Clave_90DI        = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                    IdPerfil_90DI     = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                                    Activo_90DI       = reader.IsDBNull(4) ? false : reader.GetBoolean(4),
                                    FechaAlta_90DI    = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5),
                                    DNI_90DI          = reader.IsDBNull(6) ? "" : reader.GetString(6).Trim(),
                                    Apellidos_90DI    = reader.IsDBNull(7) ? "" : reader.GetString(7),
                                    Nombre_90DI       = reader.IsDBNull(8) ? "" : reader.GetString(8),
                                    Rol_90DI          = reader.IsDBNull(9) ? "0" : reader.GetInt32(9).ToString(),
                                    Email_90DI        = reader.IsDBNull(10) ? "" : reader.GetString(10),
                                    Bloqueo_90DI      = reader.IsDBNull(11) ? false : reader.GetBoolean(11)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return result;
        }
    }
}
