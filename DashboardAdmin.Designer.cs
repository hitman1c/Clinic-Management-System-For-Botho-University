namespace MCPMS.Forms.Admin
{
    partial class DashboardAdmin
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblPatientsToday;
        private System.Windows.Forms.Button btnOpenMessaging;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAilments;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblPatientsToday = new System.Windows.Forms.Label();
            this.btnOpenMessaging = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.chartAilments = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartAilments)).BeginInit();
            this.SuspendLayout();
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Location = new System.Drawing.Point(20,20);
            this.lblWelcome.Text = "Welcome, Administrator";
            this.lblPatientsToday.AutoSize = true;
            this.lblPatientsToday.Location = new System.Drawing.Point(20,60);
            this.lblPatientsToday.Text = "0";
            this.btnOpenMessaging.Location = new System.Drawing.Point(20,100);
            this.btnOpenMessaging.Text = "Messaging";
            this.btnOpenMessaging.Click += new System.EventHandler(this.btnOpenMessaging_Click);
            this.btnReports.Location = new System.Drawing.Point(120,100);
            this.btnReports.Text = "Reports";
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);
            // chart
            this.chartAilments.Location = new System.Drawing.Point(300,20);
            this.chartAilments.Size = new System.Drawing.Size(450,300);
            this.ClientSize = new System.Drawing.Size(1000,700);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.lblPatientsToday);
            this.Controls.Add(this.btnOpenMessaging);
            this.Controls.Add(this.btnReports);
            this.Controls.Add(this.chartAilments);
            ((System.ComponentModel.ISupportInitialize)(this.chartAilments)).EndInit();
            this.Text = "Admin Dashboard";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
