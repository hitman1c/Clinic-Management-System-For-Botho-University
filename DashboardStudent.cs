using System;
using System.Windows.Forms;
using MCPMS.Classes; 
namespace MCPMS.Forms.Student 
{ 
    public partial class DashboardStudent:Form 
    { private UserModel _user; public DashboardStudent(UserModel user)
        { _user=user; InitializeComponent(); } } }