using Microsoft.Data.SqlClient;
using System.Text.Json;

namespace DAL
{
    public class ConexionSql_90DI
    {
        // Cadena por defecto para desarrollo, se usa si no hay config al lado del .exe.
        // Con la app instalada, la cadena real la deja el instalador en dal.settings.json.
        private const string ConexionPorDefecto =
            "Server=.\\SQLEXPRESS;Database=IngSoftware90DI;Trusted_Connection=True;TrustServerCertificate=True;";

        private readonly string _connectionString;

        public ConexionSql_90DI()
        {
            _connectionString = LeerConexionDesdeConfig() ?? ConexionPorDefecto;
        }

        // Lee ConnectionStrings:IngSoftware90DI de dal.settings.json (el config propio de la DAL,
        // que se copia junto al ejecutable). Si el archivo no está, le falta la clave o el JSON
        // está roto, devuelve null y se usa la cadena por defecto. Así el instalador configura
        // la conexión de cada máquina sin tener que recompilar.
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
                // Si la config está mal no queremos que explote al arrancar, usamos la de defecto.
            }
            return null;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        // Nombre de la base que figura en la cadena de conexión.
        public string NombreBaseDatos =>
            new SqlConnectionStringBuilder(_connectionString).InitialCatalog;

        // Misma conexión (servidor y credenciales) pero a otra base, normalmente master.
        // Hace falta para BACKUP/RESTORE, que no se pueden correr estando conectado a la base que se restaura.
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
