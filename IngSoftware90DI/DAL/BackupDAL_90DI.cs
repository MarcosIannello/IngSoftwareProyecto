using Microsoft.Data.SqlClient;
using NLog;

namespace DAL
{
    public class BackupDAL_90DI
    {
        private readonly ConexionSql_90DI _conexion = new ConexionSql_90DI();
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public BackupDAL_90DI() { }

        // Le pregunta al SQL Server cuál es su carpeta de backups y arma la ruta completa (@ruta)
        // con el nombre del archivo (@archivo). Se resuelve del lado del servidor porque así no
        // importa en qué máquina corra la app: es una carpeta que existe y donde SQL puede escribir.
        private const string ResolverRuta = @"
            DECLARE @dir NVARCHAR(512) = CONVERT(NVARCHAR(512), SERVERPROPERTY('InstanceDefaultBackupPath'));
            IF @dir IS NULL OR @dir = N'' SET @dir = CONVERT(NVARCHAR(512), SERVERPROPERTY('InstanceDefaultDataPath'));
            IF RIGHT(@dir, 1) NOT IN (N'\', N'/') SET @dir = @dir + N'\';
            DECLARE @ruta NVARCHAR(600) = @dir + @archivo;";

        // Backup completo (.bak) en la carpeta de backups del servidor.
        public bool Backup_90DI(string nombreArchivo)
        {
            try
            {
                using (var con = _conexion.GetConnection("master"))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        // El nombre de la base no se puede pasar como parámetro (es un identificador), pero sale
                        // de la config, no de algo que escriba el usuario. El archivo sí va como parámetro.
                        cmd.CommandText = ResolverRuta +
                            $"BACKUP DATABASE [{_conexion.NombreBaseDatos}] " +
                            "TO DISK = @ruta WITH FORMAT, INIT, NAME = N'Backup IngSoftware_90DI (full)';";
                        cmd.CommandTimeout = 0; // sin timeout, un backup puede tardar bastante
                        cmd.Parameters.AddWithValue("@archivo", nombreArchivo);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[BackupDAL] Backup_90DI - Archivo: {nombreArchivo}");
                return false;
            }
        }

        // Restaura desde el .bak. Para el RESTORE no puede haber nadie más conectado a la base.
        public bool Restore_90DI(string nombreArchivo)
        {
            var db = _conexion.NombreBaseDatos;
            try
            {
                using (var con = _conexion.GetConnection("master"))
                {
                    con.Open();

                    Ejecutar(con, $"ALTER DATABASE [{db}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;");
                    try
                    {
                        using (var cmd = con.CreateCommand())
                        {
                            cmd.CommandText = ResolverRuta +
                                $"RESTORE DATABASE [{db}] FROM DISK = @ruta WITH REPLACE;";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@archivo", nombreArchivo);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    finally
                    {
                        // Esto va siempre, aunque el RESTORE falle: si la base queda en SINGLE_USER
                        // la app no se puede volver a conectar.
                        Ejecutar(con, $"ALTER DATABASE [{db}] SET MULTI_USER;");
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[BackupDAL] Restore_90DI - Archivo: {nombreArchivo}");
                return false;
            }
        }

        // Fecha del último backup full de la base, o null si todavía no se hizo ninguno.
        public DateTime? ObtenerFechaUltimoBackup_90DI()
        {
            try
            {
                using (var con = _conexion.GetConnection("master"))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText =
                            "SELECT MAX(bs.backup_finish_date) " +
                            "FROM msdb.dbo.backupset bs " +
                            "WHERE bs.database_name = @db AND bs.type = 'D';";
                        cmd.Parameters.AddWithValue("@db", _conexion.NombreBaseDatos);
                        var result = cmd.ExecuteScalar();
                        return result == null || result == DBNull.Value ? null : (DateTime)result;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "[BackupDAL] ObtenerFechaUltimoBackup_90DI");
                return null;
            }
        }

        private static void Ejecutar(SqlConnection con, string sql)
        {
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
