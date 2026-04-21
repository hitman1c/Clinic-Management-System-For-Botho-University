using System.Configuration;
using MySql.Data.MySqlClient;

namespace MCPMS.Classes
{
    public static class DbConnection
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["MCPMSDb"].ConnectionString;
        }

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(GetConnectionString());
        }
    }
}
