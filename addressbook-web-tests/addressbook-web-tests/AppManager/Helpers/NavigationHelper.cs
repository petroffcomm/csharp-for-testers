using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace WebAddressbookTests
{
    public class NavigationHelper : HelperBase
    {
        private string baseUrl;

        public NavigationHelper(ApplicationManager appmanager, string baseUrl)
            : base(appmanager)
        {
            this.baseUrl = baseUrl;
        }
        public void OpenHomePage()
        {
            if ((driver.Url == baseUrl + "addressbook/"))
            {
                return;
            }
            driver.Navigate().GoToUrl(baseUrl + "addressbook/");
        }

        public void GoToGroupsPage()
        {
            if ( (driver.Url == baseUrl + "addressbook/group.php")
                && (IsElementPresent(By.Name("new"))) )
            {
                // If we are already on that page then nothing to do.
                return;
            }

            driver.FindElement(By.LinkText("groups")).Click();
        }
    }
}
