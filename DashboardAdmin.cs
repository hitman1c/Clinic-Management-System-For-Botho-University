using System;
using System.Windows.Forms;
using MCPMS.Classes;
using System.Windows.Forms.DataVisualization.Charting;

namespace MCPMS.Forms.Admin
{
    public partial class DashboardAdmin : Form
    {
        private UserModel _user;
        public DashboardAdmin(UserModel user)
        {
            _user = user;
            InitializeComponent();
            LoadKpis();
            LoadCharts();
        }

        private void LoadKpis()
        {
            try
            {
                using (var conn = DbConnection.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT COUNT(DISTINCT PatientId) FROM Consultations WHERE DATE(ConsultationDate)=CURDATE()";
                    conn.Open();
                    var v = cmd.ExecuteScalar();
                    lblPatientsToday.Text = v == null ? "0" : v.ToString();
                }
            }
            catch { lblPatientsToday.Text = "N/A"; }
        }

        private void LoadCharts()
        {
            try
            {
                chartAilments.Series.Clear();
                var s = chartAilments.Series.Add("Ailments");
                s.ChartType = SeriesChartType.Pie;
                s.Points.AddXY("Flu",30);
                s.Points.AddXY("Headache",20);
                s.Points.AddXY("Cold",35);
                s.Points.AddXY("Allergy",15);
            }
            catch { }
        }

        private void btnOpenMessaging_Click(object sender, EventArgs e)
        {
            var f = new MCPMS.Forms.Messaging.MessagingForm(_user);
            f.ShowDialog();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            var r = new MCPMS.Forms.Reports.ReportsForm(_user);
            r.ShowDialog();
        }
    }
}
