using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using Entities90MI;

namespace DAL
{
    public class UsuariosDAL200MI
    {
        private readonly ConexionSql200MI _conexion = new ConexionSql200MI();
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public UsuariosDAL200MI()
        {
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
                                   Rol_90DI, Email_90DI, Bloqueo_90DI
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
                                    NombreUsuario_90DI = reader.GetString(1),
                                    Clave_90DI = reader.GetString(2),
                                    IdPerfil_90DI = reader.GetInt32(3),
                                    Activo_90DI = reader.GetBoolean(4),
                                    FechaAlta_90DI = reader.GetDateTime(5),
                                    DNI_90DI = reader.GetString(6),
                                    Apellidos_90DI = reader.GetString(7),
                                    Nombre_90DI = reader.GetString(8),
                                    Rol_90DI = reader.GetString(9),
                                    Email_90DI = reader.GetString(10),
                                    Bloqueo_90DI = reader.GetBoolean(11)
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
                                set Bloqueo_90DI = 1
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
