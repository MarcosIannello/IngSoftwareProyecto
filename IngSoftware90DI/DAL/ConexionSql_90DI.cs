using Microsoft.Data.SqlClient;
using System.Configuration;

namespace DAL
{
    public class ConexionSql_90DI
    {
        //private readonly string _connectionString = "Server=Juanbe-Mate4b\\MSSQLSERVER01;Database=IngSoftware90DI;Integrated Security=True;TrustServerCertificate=True;";

        private readonly string _connectionString = "Server=10.211.55.2,1433;Database=IngSoftware90DI;User Id=sa;Password=TuPasswordFuerte123!;TrustServerCertificate=True;";

        public ConexionSql_90DI()
        {
            //_connectionString = ConfigurationManager.ConnectionStrings["IngSoftware90DI"].ConnectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
