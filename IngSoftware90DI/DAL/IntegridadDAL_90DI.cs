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

        // Recalcula y persiste el dígito verificador (TOTAL + FILA + COLUMNA) de una tabla.
        // Se llama desde BLL después de cada operación de escritura exitosa.
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

        // Recalcula el dígito verificador de TODAS las tablas registradas en RegistroDV_90DI.
        // Solo debe ser llamado por un administrador (setup inicial, migración, corrección autorizada).
        // La BD (RegistroDV_90DI) es la fuente autoritativa de qué tablas están bajo DV.
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

        // Verifica la integridad de todas las tablas del catálogo de integridad sin persistir.
        // Retorna TODOS los resultados (INTEGRO + CORRUPTO + ERROR) con drill-down
        // a fila y columna afectada cuando hay corrupción.
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
