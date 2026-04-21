using System;
using System.Windows.Forms;

namespace MCPMS
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Forms.Authentication.LoginForm());
        }
    }
}
