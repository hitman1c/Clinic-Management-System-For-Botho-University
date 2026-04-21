using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MCPMS.Classes;

namespace MCPMS.Forms.Messaging
{
    public partial class MessagingForm : Form
    {
        private UserModel _user;

        public MessagingForm(UserModel user)
        {
            _user = user;
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            // For demo, send to all students if checkbox checked
            var ids = new List<int>();
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    if (chkAllStudents.Checked)
                    {
                        cmd.CommandText = "SELECT UserId FROM Users WHERE RoleId=1";
                        using (var r = cmd.ExecuteReader())
                        {
                            while (r.Read())
                                ids.Add(r.GetInt32(0));
                        }
                    }
                    else
                    {
                        // if a single username provided, try resolve
                        string target = txtTargetUsername.Text.Trim();
                        if (!string.IsNullOrEmpty(target))
                        {
                            cmd.CommandText = "SELECT UserId FROM Users WHERE Username=@u";
                            cmd.Parameters.AddWithValue("@u", target);
                            using (var r = cmd.ExecuteReader())
                            {
                                if (r.Read())
                                    ids.Add(r.GetInt32(0));
                            }
                        }
                    }
                }
            }

            if (ids.Count > 0)
            {
                NotificationManager.SendNotification(_user.UserId, ids, txtTitle.Text, txtMessage.Text);
                MessageBox.Show("Message sent.");
            }
            else
            {
                MessageBox.Show("No recipients found.");
            }
        }
    }
}