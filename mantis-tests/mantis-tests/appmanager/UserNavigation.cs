using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace MantisTests
{
    public class UserNavigation : HelperBase
    {
        private string baseUrl;

        public UserNavigation(ApplicationManager appmanager, string baseUrl)
            : base(appmanager)
        {
            this.baseUrl = baseUrl;
        }


        public void OpenLoginPage()
        {
            if ((driver.Url == baseUrl + "login_page.php"))
            {
                return;
            }
            driver.Navigate().GoToUrl(baseUrl + "login_page.php");
        }
    }
}
