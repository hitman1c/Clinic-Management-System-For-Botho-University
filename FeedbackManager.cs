using System;
using MySql.Data.MySqlClient;

namespace MCPMS.Classes
{
    public static class FeedbackManager
    {
        public static void SubmitFeedback(int fromUserId, int? toUserId, string message)
        {
            using (var conn = DbConnection.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO Feedbacks (FromUserId, ToUserId, Message) VALUES (@f,@t,@m)";
                cmd.Parameters.AddWithValue("@f", fromUserId);
                cmd.Parameters.AddWithValue("@t", toUserId.HasValue ? (object)toUserId.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@m", message);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
