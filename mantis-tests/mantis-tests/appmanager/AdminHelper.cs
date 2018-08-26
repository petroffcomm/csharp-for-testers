using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantisTests
{
    public class AdminHelper : HelperBase
    {
        public AdminHelper(ApplicationManager appmanager) : base(appmanager){}


        public void GoToProjectsPage()
        {
            appmanager.Driver.Navigate().GoToUrl(@"http://localhost/mantisbt-2.16.0/manage_proj_page.php");
        }
    }
}
