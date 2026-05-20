using NLog;
using Services_90DI;

namespace DAL
{
    public class UserDAL_90DI
    {
        private readonly ConexionSql_90DI _conexion = new ConexionSql_90DI();
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public UserDAL_90DI() { }

        public List<User_90DI> GetAllUsers90DI()
        {
            var result = new List<User_90DI>();
            try
            {
                using (var con = _conexion.GetConnection())
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText =
                        """
                            SELECT IdUsuario_90DI, NombreUsuario_90DI, Password_90DI,
                                   Activo_90DI, FechaAlta_90DI, DNI_90DI,
                                   Apellidos_90DI, Nombre_90DI, Rol_90DI,
                                   Email_90DI, Bloqueo_90DI
                            FROM User_90DI
                        """;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new User_90DI
                                {
                                    IdUsuario_90DI     = reader.GetInt32(0),
                                    NombreUsuario_90DI = reader.IsDBNull(1)  ? "" : reader.GetString(1),
                                    Password_90DI      = reader.IsDBNull(2)  ? "" : reader.GetString(2),
                                    Activo_90DI        = reader.IsDBNull(3)  ? false : reader.GetBoolean(3),
                                    FechaAlta_90DI     = reader.IsDBNull(4)  ? DateTime.MinValue : reader.GetDateTime(4),
                                    DNI_90DI           = reader.IsDBNull(5)  ? "" : reader.GetString(5).Trim(),
                                    Apellidos_90DI     = reader.IsDBNull(6)  ? "" : reader.GetString(6),
                                    Nombre_90DI        = reader.IsDBNull(7)  ? "" : reader.GetString(7),
                                    Rol_90DI           = reader.IsDBNull(8)  ? "0" : reader.GetInt32(8).ToString(),
                                    Email_90DI         = reader.IsDBNull(9)  ? "" : reader.GetString(9),
                                    Bloqueo_90DI       = reader.IsDBNull(10) ? false : reader.GetBoolean(10)
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

        public User_90DI? GetUserByUsername_90DI(string Username)
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
                            SELECT IdUsuario_90DI, NombreUsuario_90DI, Password_90DI,
                                   Activo_90DI, FechaAlta_90DI, DNI_90DI,
                                   Apellidos_90DI, Nombre_90DI, Rol_90DI,
                                   Email_90DI, Bloqueo_90DI
                            FROM User_90DI
                            WHERE NombreUsuario_90DI = @Username
                        """;
                        cmd.Parameters.AddWithValue("@Username", Username);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new User_90DI
                                {
                                    IdUsuario_90DI     = reader.GetInt32(0),
                                    NombreUsuario_90DI = reader.IsDBNull(1)  ? "" : reader.GetString(1),
                                    Password_90DI      = reader.IsDBNull(2)  ? "" : reader.GetString(2),
                                    Activo_90DI        = reader.IsDBNull(3)  ? false : reader.GetBoolean(3),
                                    FechaAlta_90DI     = reader.IsDBNull(4)  ? DateTime.MinValue : reader.GetDateTime(4),
                                    DNI_90DI           = reader.IsDBNull(5)  ? "" : reader.GetString(5).Trim(),
                                    Apellidos_90DI     = reader.IsDBNull(6)  ? "" : reader.GetString(6),
                                    Nombre_90DI        = reader.IsDBNull(7)  ? "" : reader.GetString(7),
                                    Rol_90DI           = reader.IsDBNull(8)  ? "0" : reader.GetInt32(8).ToString(),
                                    Email_90DI         = reader.IsDBNull(9)  ? "" : reader.GetString(9),
                                    Bloqueo_90DI       = reader.IsDBNull(10) ? false : reader.GetBoolean(10)
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

        public List<User_90DI> SearchUsers90DI(string? dni = null, string? apellidos = null, string? nombre = null, string? email = null, string? rol = null, string? login = null)
        {
            var result = new List<User_90DI>();
            try
            {
                using (var con = _conexion.GetConnection())
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText =
                        """
                            SELECT IdUsuario_90DI, NombreUsuario_90DI, Password_90DI,
                                   Activo_90DI, FechaAlta_90DI, DNI_90DI,
                                   Apellidos_90DI, Nombre_90DI, Rol_90DI,
                                   Email_90DI, Bloqueo_90DI
                            FROM User_90DI
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
                                result.Add(new User_90DI
                                {
                                    IdUsuario_90DI     = reader.GetInt32(0),
                                    NombreUsuario_90DI = reader.IsDBNull(1)  ? "" : reader.GetString(1),
                                    Password_90DI      = reader.IsDBNull(2)  ? "" : reader.GetString(2),
                                    Activo_90DI        = reader.IsDBNull(3)  ? false : reader.GetBoolean(3),
                                    FechaAlta_90DI     = reader.IsDBNull(4)  ? DateTime.MinValue : reader.GetDateTime(4),
                                    DNI_90DI           = reader.IsDBNull(5)  ? "" : reader.GetString(5).Trim(),
                                    Apellidos_90DI     = reader.IsDBNull(6)  ? "" : reader.GetString(6),
                                    Nombre_90DI        = reader.IsDBNull(7)  ? "" : reader.GetString(7),
                                    Rol_90DI           = reader.IsDBNull(8)  ? "0" : reader.GetInt32(8).ToString(),
                                    Email_90DI         = reader.IsDBNull(9)  ? "" : reader.GetString(9),
                                    Bloqueo_90DI       = reader.IsDBNull(10) ? false : reader.GetBoolean(10)
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

        public bool CreateUser90DI(User_90DI usuario)
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
                                INSERT INTO User_90DI
                                    (NombreUsuario_90DI, Password_90DI, IdPerfil_90DI,
                                     Activo_90DI, FechaAlta_90DI, DNI_90DI,
                                     Apellidos_90DI, Nombre_90DI, Rol_90DI,
                                     Email_90DI, Bloqueo_90DI)
                                VALUES
                                    (@nombreUsuario, @password, @idPerfil,
                                     @activo, @fechaAlta, @dni,
                                     @apellidos, @nombre, @rol,
                                     @email, @bloqueado)
                            """;
                        cmd.Parameters.AddWithValue("@nombreUsuario", usuario.NombreUsuario_90DI);
                        cmd.Parameters.AddWithValue("@password",      usuario.Password_90DI);
                        cmd.Parameters.AddWithValue("@idPerfil",      usuario.IdPerfil_90DI);
                        cmd.Parameters.AddWithValue("@activo",        usuario.Activo_90DI);
                        cmd.Parameters.AddWithValue("@fechaAlta",     DateTime.Now);
                        cmd.Parameters.AddWithValue("@dni",           usuario.DNI_90DI);
                        cmd.Parameters.AddWithValue("@apellidos",     usuario.Apellidos_90DI);
                        cmd.Parameters.AddWithValue("@nombre",        usuario.Nombre_90DI);
                        cmd.Parameters.AddWithValue("@rol",           usuario.Rol_90DI);
                        cmd.Parameters.AddWithValue("@email",         usuario.Email_90DI);
                        cmd.Parameters.AddWithValue("@bloqueado",     usuario.Bloqueo_90DI);

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

        public bool UpdateUser90DI(User_90DI usuario)
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
                                UPDATE User_90DI
                                SET NombreUsuario_90DI = @nombreUsuario,
                                    DNI_90DI           = @dni,
                                    Apellidos_90DI     = @apellidos,
                                    Nombre_90DI        = @nombre,
                                    Rol_90DI           = @rol,
                                    Email_90DI         = @email
                                WHERE IdUsuario_90DI = @idUsuario
                            """;
                        cmd.Parameters.AddWithValue("@nombreUsuario", usuario.NombreUsuario_90DI);
                        cmd.Parameters.AddWithValue("@dni",           usuario.DNI_90DI);
                        cmd.Parameters.AddWithValue("@apellidos",     usuario.Apellidos_90DI);
                        cmd.Parameters.AddWithValue("@nombre",        usuario.Nombre_90DI);
                        cmd.Parameters.AddWithValue("@rol",           usuario.Rol_90DI);
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
                                UPDATE User_90DI
                                SET Password_90DI = @password
                                WHERE IdUsuario_90DI = @idUsuario
                            """;
                        cmd.Parameters.AddWithValue("@password",  hashPassword);
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
                                UPDATE User_90DI
                                SET Bloqueo_90DI = 1
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
                                UPDATE User_90DI
                                SET Bloqueo_90DI = 0
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
                                UPDATE User_90DI
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
                                UPDATE User_90DI
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
    }
}
