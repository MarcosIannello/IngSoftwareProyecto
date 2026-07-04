using Microsoft.Data.SqlClient;
using System.Configuration;

namespace DAL
{
    public class ConexionSql_90DI
    {
        //private readonly string _connectionString = "Server=Juanbe-Mate4b\\MSSQLSERVER01;Database=IngSoftware90DI;Integrated Security=True;TrustServerCertificate=True;";

        //Docker connectionString
        //private readonly string _connectionString = "Server=10.211.55.2,1433;Database=IngSoftware90DI;User Id=sa;Password=TuPasswordFuerte123!;TrustServerCertificate=True;";

        private readonly string _connectionString = "Server=.\\SQLEXPRESS;Database=IngSoftware90DI;Trusted_Connection=True;TrustServerCertificate=True;";


        public ConexionSql_90DI()
        {
            //_connectionString = ConfigurationManager.ConnectionStrings["IngSoftware90DI"].ConnectionString;
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
