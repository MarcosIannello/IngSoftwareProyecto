using System.Data;
using NLog;
using Services_90DI.entities;

namespace DAL
{
    public class IntegridadDAL_90DI
    {
        private readonly ConexionSql_90DI _conexion = new ConexionSql_90DI();
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public IntegridadDAL_90DI() { }

        // Recalcula y guarda el DV de una tabla (TOTAL, FILA y COLUMNA).
        // La BLL lo llama después de cada escritura que salió bien.
        public bool RecalcularTabla_90DI(string nombreTabla)
        {
            try
            {
                using (var con = _conexion.GetConnection())
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "sp_RecalcularDV_90DI";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NombreTabla", nombreTabla);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[IntegridadDAL] RecalcularTabla_90DI - Tabla: {nombreTabla}");
                return false;
            }
        }

        // Recalcula el DV de todas las tablas cargadas en RegistroDV_90DI.
        // Es cosa del admin (setup inicial, migración o reparar a mano). Qué tablas
        // entran en el DV lo define esa tabla en la base, no el código.
        public bool RecalcularTodo_90DI()
        {
            try
            {
                using (var con = _conexion.GetConnection())
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "sp_RecalcularTodo_90DI";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "[IntegridadDAL] RecalcularTodo_90DI");
                return false;
            }
        }

        // Verifica todas las tablas del catálogo sin guardar nada. Devuelve todos los resultados
        // (íntegros, corruptos y errores) y, si algo está corrupto, en qué fila y columna.
        // Se llama al iniciar sesión.
        public List<ResultadoIntegridad_90DI> Verificar_90DI()
        {
            var resultados = new List<ResultadoIntegridad_90DI>();

            try
            {
                using (var con = _conexion.GetConnection())
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "sp_VerificarTodo_90DI";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                resultados.Add(new ResultadoIntegridad_90DI
                                {
                                    NombreTabla_90DI     = reader.IsDBNull(0) ? "" : reader.GetString(0),
                                    Estado_90DI          = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                    FilaAfectada_90DI    = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    ColumnaAfectada_90DI = reader.IsDBNull(3) ? null : reader.GetString(3)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "[IntegridadDAL] Verificar_90DI");
            }

            return resultados;
        }
    }
}
