using Microsoft.Data.SqlClient;
using System.Configuration;

namespace DAL
{
    public class ConexionSql200MI
    {
        private readonly string _connectionString;

        public ConexionSql200MI()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["CapitalPlus"].ConnectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
