using Microsoft.Data.SqlClient;
using NLog;

namespace DAL
{
    public class BackupDAL_90DI
    {
        private readonly ConexionSql_90DI _conexion = new ConexionSql_90DI();
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public BackupDAL_90DI() { }

        // T-SQL que resuelve la carpeta de backups del propio SQL Server y arma la ruta
        // completa (@ruta) a partir del nombre de archivo (@archivo). Se calcula en el
        // SERVIDOR, así no depende del SO ni de la máquina: siempre apunta a una carpeta
        // que existe y donde la cuenta de servicio de SQL puede escribir.
        private const string ResolverRuta = @"
            DECLARE @dir NVARCHAR(512) = CONVERT(NVARCHAR(512), SERVERPROPERTY('InstanceDefaultBackupPath'));
            IF @dir IS NULL OR @dir = N'' SET @dir = CONVERT(NVARCHAR(512), SERVERPROPERTY('InstanceDefaultDataPath'));
            IF RIGHT(@dir, 1) NOT IN (N'\', N'/') SET @dir = @dir + N'\';
            DECLARE @ruta NVARCHAR(600) = @dir + @archivo;";

        // Genera un backup completo (.bak) en la carpeta de backups del servidor SQL.
        public bool Backup_90DI(string nombreArchivo)
        {
            try
            {
                using (var con = _conexion.GetConnection("master"))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        // El nombre de la base es un identificador: no puede ir como parámetro
                        // (proviene de config, no de input del usuario). El archivo sí va parametrizado.
                        cmd.CommandText = ResolverRuta +
                            $"BACKUP DATABASE [{_conexion.NombreBaseDatos}] " +
                            "TO DISK = @ruta WITH FORMAT, INIT, NAME = N'Backup IngSoftware_90DI (full)';";
                        cmd.CommandTimeout = 0; // un backup puede tardar más que el timeout default
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

        // Restaura la base desde el .bak. El RESTORE necesita acceso EXCLUSIVO
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
                        // Siempre reabrir la base, aunque el RESTORE haya fallado, para no
                        // dejarla en SINGLE_USER e inaccesible para el resto de la app.
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

        // Devuelve la fecha/hora de creación del último backup full de la base, o null si todavía no se generó ninguno.
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
