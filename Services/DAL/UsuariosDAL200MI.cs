using NLog;
using Services_90DI.Entidades;

namespace Services_90DI.DAL
{
    internal class UsuariosDAL200MI
    {
        private readonly ConexionSql200MI _conexion = new ConexionSql200MI();
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public UsuariosDAL200MI() { }

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
    }
}
