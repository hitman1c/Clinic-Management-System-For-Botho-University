using System;
using System.IO;
using MySql.Data.MySqlClient;

namespace MCPMS.Classes
{
    public static class TableInitializer
    {
        // Executes the SQL script file to create DB/tables if they don't exist.
        public static void EnsureDatabase(string sqlFilePath)
        {
            if (!File.Exists(sqlFilePath))
                throw new FileNotFoundException("SQL file not found: " + sqlFilePath);

            string script = File.ReadAllText(sqlFilePath);
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    // MySQL allows multiple statements if using MySqlScript helper
                    var ms = new MySqlScript(conn, script);
                    ms.Execute();
                }
            }
        }
    }
}
