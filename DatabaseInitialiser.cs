using System;
using System.IO;
using MySql.Data.MySqlClient;

namespace MCPMS.Helpers
{
    public static class DatabaseInitializer
    {
        // Connection string to the MySQL server (no database specified)
        private static string serverConn = "server=localhost;user id=root;password=;SslMode=none;";

        // Name of the database
        public static string DatabaseName = "bothouniversityclinic";

        public static void EnsureDatabaseExists()
        {
            try
            {
                // 1️⃣ Connect to MySQL server and check if the database exists
                using (var conn = new MySqlConnection(serverConn))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $"SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = '{DatabaseName}'";
                        var result = cmd.ExecuteScalar();

                        if (result == null)
                        {
                            // Database does not exist, create it
                            cmd.CommandText = $"CREATE DATABASE `{DatabaseName}` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;";
                            cmd.ExecuteNonQuery();
                            Console.WriteLine($"Database '{DatabaseName}' created successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Database '{DatabaseName}' already exists.");
                        }
                    }
                }

                // 2️⃣ Execute BothoUniversityClinic.sql if it exists in the Database folder
                string[] possiblePaths =
                {
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database", "BothoUniversityClinic.sql"),
                    Path.Combine(Directory.GetCurrentDirectory(), "Database", "BothoUniversityClinic.sql"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BothoUniversityClinic.sql"), // Fallback without folder
                    Path.Combine(Directory.GetCurrentDirectory(), "BothoUniversityClinic.sql") // Fallback without folder
                };

                string sqlPath = null;
                foreach (var p in possiblePaths)
                {
                    if (File.Exists(p))
                    {
                        sqlPath = p;
                        break;
                    }
                }

                if (sqlPath == null)
                {
                    Console.WriteLine("SQL script file not found. Please ensure 'BothoUniversityClinic.sqghuiopiuygfl' is in the Database folder.");
                    return;
                }

                string sqlContent = File.ReadAllText(sqlPath);
                if (string.IsNullOrWhiteSpace(sqlContent))
                {
                    Console.WriteLine("BothoUniversityClinic.sql file is empty. Skipping schema initialization.");
                    return;
                }

                string dbConn = $"server=localhost;user id=root;password=;database={DatabaseName};SslMode=none;";
                using (var conn = new MySqlConnection(dbConn))
                {
                    conn.Open();
                    var statements = sqlContent.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var stmt in statements)
                    {
                        var trimmedStmt = stmt.Trim();
                        if (string.IsNullOrWhiteSpace(trimmedStmt)) continue;

                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = trimmedStmt;
                            try
                            {
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Failed executing SQL statement: {trimmedStmt}");
                                Console.WriteLine($"Error: {ex.Message}");
                                throw;
                            }
                        }
                    }

                    Console.WriteLine("Database schema initialized successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database initialization failed:");
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
