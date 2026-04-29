using BE;
using Entidades;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class UsuariosDAL90DI
    {
        private readonly ConexionSql200MI _conexion = new ConexionSql200MI();
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public UsuariosDAL90DI() { }

        public Usuario90DI? getUser90DI(string username)
        {
            try
            {
                using var con = _conexion.GetConnection();
                con.Open();

                using var cmd = con.CreateCommand();
                cmd.CommandText =
                    """
                    SELECT IdUsuario_90DI, NombreUsuario_90DI, Clave_90DI, IdPerfil_90DI, 
                           Activo_90DI, FechaAlta_90DI, DNI_90DI, Apellidos_90DI, 
                           Nombre_90DI, Rol_90DI, Email_90DI, Bloquo_90DI
                    FROM Usuarios_90DI
                    WHERE NombreUsuario_90DI = @Username
                    """;
                cmd.Parameters.AddWithValue("@Username", username);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Usuario90DI
                    {
                        IdUsuario_90DI = reader.GetInt32(0),
                        NombreUsuario_90DI = reader.GetString(1),
                        Clave_90DI = reader.GetString(2),
                        IdPerfil_90DI = reader.GetInt32(3),
                        Activo_90DI = reader.GetBoolean(4),
                        FechaAlta_90DI = reader.GetDateTime(5),
                        DNI_90DI = reader.GetString(6),
                        Apellidos_90DI = reader.IsDBNull(7) ? null : reader.GetString(7),
                        Nombre_90DI = reader.IsDBNull(8) ? null : reader.GetString(8),
                        Rol_90DI = reader.GetInt32(9),
                        Email_90DI = reader.IsDBNull(10) ? null : reader.GetString(10),
                        Bloquo_90DI = reader.IsDBNull(11) ? null : reader.GetBoolean(11)
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return null;
            }
        }

        public bool updatePassword90DI(int idUsuario, string hashPassword)
        {
            try
            {
                using var con = _conexion.GetConnection();
                con.Open();

                using var cmd = con.CreateCommand();
                cmd.CommandText =
                    """
                    UPDATE Usuarios_90DI
                    SET Clave_90DI = @password
                    WHERE IdUsuario_90DI = @idUsuario
                    """;
                cmd.Parameters.AddWithValue("@password", hashPassword);
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
        }
    }
}