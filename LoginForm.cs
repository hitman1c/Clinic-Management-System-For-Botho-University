using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using MCPMS.Classes;

namespace MCPMS.Forms.Authentication
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            LoadFooterFeedbacks();
        }

        private void btnInstallDb_Click(object sender, EventArgs e)
        {
            try
            {
                var sqlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database", "BothoUniversityClinic.sql");
                TableInitializer.EnsureDatabase(sqlPath);
                MessageBox.Show("Database created/updated successfully.", "Installer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to install DB: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var user = AuthManager.ValidateUser(txtUsername.Text.Trim(), txtPassword.Text);
            if (user == null)
            {
                MessageBox.Show("Invalid credentials.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!user.IsActive)
            {
                MessageBox.Show("Account inactive.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (user.Role == Role.Administrator)
            {
                var d = new MCPMS.Forms.Admin.DashboardAdmin(user);
                d.Show();
                this.Hide();
            }
            else if (user.Role == Role.Student)
            {
                var d = new MCPMS.Forms.Student.DashboardStudent(user);
                d.Show();
                this.Hide();
            }
            else
            {
                var d = new MCPMS.Forms.Provider.DashboardProvider(user);
                d.Show();
                this.Hide();
            }
        }

        private void LoadFooterFeedbacks()
        {
            // Simple footer: read last 3 feedback messages from DB and show in label (non-blocking simple approach)
            try
            {
                var sb = new System.Text.StringBuilder();
                using (var conn = DbConnection.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT f.Message, u.Fullname FROM Feedbacks f LEFT JOIN Users u ON f.FromUserId=u.UserId ORDER BY f.CreatedAt DESC LIMIT 3";
                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            var msg = r.IsDBNull(0) ? "" : r.GetString(0);
                            var who = r.IsDBNull(1) ? "Anonymous" : r.GetString(1);
                            sb.AppendLine($"{who}: {msg}"); 
                        }
                    }
                }
                lblFooter.Text = sb.Length == 0 ? "No feedback yet." : sb.ToString();
            }
            catch { lblFooter.Text = "Feedbacks currently unavailable."; }
        }
    }
}
