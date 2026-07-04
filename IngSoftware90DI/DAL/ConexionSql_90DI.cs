using Microsoft.Data.SqlClient;
using System.Text.Json;

namespace DAL
{
    public class ConexionSql_90DI
    {
        // Fallback para desarrollo: si NO hay appsettings.json junto al .exe, se usa esta.
        // En producción/instalador, la cadena real viene de appsettings.json (por máquina).
        private const string ConexionPorDefecto =
            "Server=.\\SQLEXPRESS;Database=IngSoftware90DI;Trusted_Connection=True;TrustServerCertificate=True;";

        private readonly string _connectionString;

        public ConexionSql_90DI()
        {
            _connectionString = LeerConexionDesdeConfig() ?? ConexionPorDefecto;
        }

        // Lee ConnectionStrings:IngSoftware90DI de dal.settings.json, el archivo de config
        // PROPIO de la capa DAL (se copia junto al ejecutable, pero lo declara/posee la DAL).
        // Si el archivo no existe, la clave falta o el JSON es inválido, devuelve null → se usa
        // el fallback. Esto permite que el instalador escriba la cadena por máquina sin recompilar.
        private static string? LeerConexionDesdeConfig()
        {
            try
            {
                var ruta = Path.Combine(AppContext.BaseDirectory, "dal.settings.json");
                if (!File.Exists(ruta)) return null;

                using var doc = JsonDocument.Parse(File.ReadAllText(ruta));
                if (doc.RootElement.TryGetProperty("ConnectionStrings", out var cs) &&
                    cs.TryGetProperty("IngSoftware90DI", out var val))
                {
                    var s = val.GetString();
                    return string.IsNullOrWhiteSpace(s) ? null : s;
                }
            }
            catch
            {
                // Config inválida → no romper el arranque, caer al default.
            }
            return null;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        // Nombre de la base configurada (Initial Catalog / Database del connection string).
        public string NombreBaseDatos =>
            new SqlConnectionStringBuilder(_connectionString).InitialCatalog;

        // Conexión apuntando a OTRA base (típicamente "master") reutilizando el mismo
        // servidor y credenciales. Necesaria para BACKUP/RESTORE: esas sentencias no
        // pueden ejecutarse estando conectado a la propia base que se quiere restaurar.
        public SqlConnection GetConnection(string database)
        {
            var builder = new SqlConnectionStringBuilder(_connectionString)
            {
                InitialCatalog = database
            };
            return new SqlConnection(builder.ConnectionString);
        }
    }
}
