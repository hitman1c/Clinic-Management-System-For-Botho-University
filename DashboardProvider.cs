using System;
using System.Windows.Forms;
using MCPMS.Classes;

namespace MCPMS.Forms.
    Provider { public partial class DashboardProvider:Form 
    { private UserModel _user; public DashboardProvider(UserModel user)
        { _user=user; InitializeComponent(); } } }