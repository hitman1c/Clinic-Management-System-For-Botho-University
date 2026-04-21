using System;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;

namespace MCPMS.Classes
{
    public enum Role { Student = 1, Provider = 2, Administrator = 3, Receptionist = 4 }

    public class UserModel
    {
        public int UserId { get; set; }
        public string Username { get; set; } = "";
        public string Fullname { get; set; } = "";
        public Role Role { get; set; }
        public bool IsActive { get; set; }
    }

    public static class AuthManager
    {
        public static UserModel? ValidateUser(string username, string password)
        {
            using (var conn = DbConnection.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT UserId, Username, Fullname, RoleId, PasswordHash, IsActive FROM Users WHERE Username=@u LIMIT 1";
                cmd.Parameters.AddWithValue("@u", username);
                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    if (!r.Read()) return null;
                    var storedHash = r.GetString("PasswordHash");
                    // The SQL script stores SHA2(...,256) hex. Verify by computing SHA256 hex.
                    using (var sha = SHA256.Create())
                    {
                        var hashBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                        var hex = BitConverter.ToString(hashBytes).Replace("-","").ToLower();
                        if (storedHash.ToLower().Contains(hex))
                        {
                            return new UserModel
                            {
                                UserId = r.GetInt32("UserId"),
                                Username = r.GetString("Username"),
                                Fullname = r.IsDBNull(r.GetOrdinal("Fullname")) ? "" : r.GetString("Fullname"),
                                Role = (Role)r.GetInt32("RoleId"),
                                IsActive = r.GetBoolean("IsActive")
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}
