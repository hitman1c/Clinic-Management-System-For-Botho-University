namespace MCPMS.Forms.Authentication
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnInstallDb;
        private System.Windows.Forms.Label lblFooter;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnInstallDb = new System.Windows.Forms.Button();
            this.lblFooter = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // txtUsername
            this.txtUsername.Location = new System.Drawing.Point(30,30);
            this.txtUsername.PlaceholderText = "Username";
            this.txtUsername.Width = 260;
            // txtPassword
            this.txtPassword.Location = new System.Drawing.Point(30,70);
            this.txtPassword.PlaceholderText = "Password";
            this.txtPassword.Width = 260;
            this.txtPassword.UseSystemPasswordChar = true;
            // btnLogin
            this.btnLogin.Location = new System.Drawing.Point(30,110);
            this.btnLogin.Text = "Login";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // btnInstallDb
            this.btnInstallDb.Location = new System.Drawing.Point(130,110);
            this.btnInstallDb.Text = "Install DB (XAMPP)";
            this.btnInstallDb.Click += new System.EventHandler(this.btnInstallDb_Click);
            // lblFooter
            this.lblFooter.Location = new System.Drawing.Point(20,160);
            this.lblFooter.Width = 760;
            this.lblFooter.Height = 60;
            this.lblFooter.Text = "Feedbacks will appear here after users log out.";
            // LoginForm
            this.ClientSize = new System.Drawing.Size(800,300);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnInstallDb);
            this.Controls.Add(this.lblFooter);
            this.Text = "Botho University Clinic - Login";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
