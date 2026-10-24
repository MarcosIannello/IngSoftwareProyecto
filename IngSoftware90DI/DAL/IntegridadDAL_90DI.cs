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

        // Recalcula el DV solo de las filas donde [columna] = valor (lo que tocó el ABM) y ajusta
        // COLUMNA y TOTAL con la diferencia. El resto de la tabla no se toca: si alguien modificó
        // otra fila a mano, sigue sin coincidir y la verificación la detecta.
        public bool RecalcularFilas_90DI(string nombreTabla, string columna, string valor)
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
                        cmd.Parameters.AddWithValue("@Columna", columna);
                        cmd.Parameters.AddWithValue("@Valor", valor);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[IntegridadDAL] RecalcularFilas_90DI - Tabla: {nombreTabla}, {columna} = {valor}");
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
        // Se llama al iniciar sesión. Si falla devuelve la lista vacía; la BLL la completa con
        // ERROR para que no quede como íntegro.
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
