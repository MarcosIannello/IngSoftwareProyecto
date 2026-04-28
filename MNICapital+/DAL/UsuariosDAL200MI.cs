using BE;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class UsuariosDAL200MI
    {
        private readonly ConexionSql200MI _conexion = new ConexionSql200MI();
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public UsuariosDAL200MI()
        {
        }

        public Usuario200MI? GetUser200MI(string Username)
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
                                SELECT IdUsuario_200MI, NombreUsuario_200MI, IdPerfil_200MI, Activo_200MI, FechaAlta_200MI, Clave_200MI FROM Usuarios_200MI 
                                WHERE NombreUsuario_200MI = @Username
                            """;
                        cmd.Parameters.AddWithValue("@Username", Username);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Usuario200MI
                                {
                                    IdUsuario_200MI = reader.GetInt32(0),
                                    NombreUsuario_200MI = reader.GetString(1),
                                    IdPerfil_200MI = reader.GetInt32(2),
                                    Activo_200MI = reader.GetBoolean(3),
                                    FechaAlta_200MI = reader.GetDateTime(4),
                                    Clave_200MI = reader.GetString(5)
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


        public bool UpdatePassword200MI(int idUsuario200MI, string hashPassword)
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
                                Update Usuarios_200MI 
                                set Clave_200MI = @password
                                where IdUsuario_200MI = @idUsuario                             
                            """;
                        cmd.Parameters.AddWithValue("@password", hashPassword);
                        cmd.Parameters.AddWithValue("@idUsuario", idUsuario200MI);

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
