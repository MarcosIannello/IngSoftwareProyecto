using Entities_90DI;
using NLog;

namespace DAL
{
    public class BitacoraDAL_90DI
    {
        private readonly ConexionSql200MI _conexion = new ConexionSql200MI();
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public BitacoraDAL_90DI() { }

        public bool CreateLogEvent_90DI(LogEvent_90DI logEvent_90DI)
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
                            INSERT INTO EVENTOS_90DI (Login, Fecha, Hora, Modulo, Evento, Criticidad)
                            VALUES (@Login, @Fecha, @Hora, @Modulo, @Evento, @Criticidad)
                            """;
                        cmd.Parameters.AddWithValue("@Login", logEvent_90DI.Login_90DI);
                        cmd.Parameters.AddWithValue("@Fecha", logEvent_90DI.Fecha_90DI.Date);
                        cmd.Parameters.AddWithValue("@Hora", logEvent_90DI.Hora_90DI);
                        cmd.Parameters.AddWithValue("@Modulo", logEvent_90DI.Modulo_90DI);
                        cmd.Parameters.AddWithValue("@Evento", logEvent_90DI.Evento_90DI);
                        cmd.Parameters.AddWithValue("@Criticidad", logEvent_90DI.Criticidad_90DI);

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

        public List<LogEvent_90DI> GetLogEvent_90DI()
        {
            var eventos = new List<LogEvent_90DI>();

            try
            {
                using (var con = _conexion.GetConnection())
                {
                    con.Open();

                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText =
                            """
                            SELECT Id_Evento, Login, Fecha, Hora, Modulo, Evento, Criticidad
                            FROM EVENTOS_90DI
                            ORDER BY Fecha DESC, Hora DESC
                            """;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                eventos.Add(new LogEvent_90DI
                                {
                                    IdEvento_90DI   = reader.GetInt32(0),
                                    Login_90DI      = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                    Fecha_90DI      = reader.IsDBNull(2) ? DateTime.MinValue : reader.GetDateTime(2),
                                    Hora_90DI       = reader.IsDBNull(3) ? TimeSpan.Zero : reader.GetTimeSpan(3),
                                    Modulo_90DI     = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                    Evento_90DI     = reader.IsDBNull(5) ? "" : reader.GetString(5),
                                    Criticidad_90DI = reader.IsDBNull(6) ? (byte)0 : reader.GetByte(6)
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

            return eventos;
        }
    }
}
