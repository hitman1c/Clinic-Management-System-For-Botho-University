using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace MCPMS.Classes
{
    public static class NotificationManager
    {
        public static void SendNotification(int senderUserId, List<int> receiverUserIds, string title, string message)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                foreach (var rid in receiverUserIds)
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO Notifications (SenderUserId, ReceiverUserId, TargetType, Title, Message) VALUES (@s,@r,'individual',@t,@m)";
                        cmd.Parameters.AddWithValue("@s", senderUserId);
                        cmd.Parameters.AddWithValue("r", rid);
                        cmd.Parameters.AddWithValue("t", title);
                        cmd.Parameters.AddWithValue("m", message);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
