using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MantisTests
{
    public class LoginHelper : HelperBase
    {
        private string baseUrl;
        private string loginBtnXPath = "//input[@type='submit']";

        public LoginHelper(ApplicationManager appmanager, string baseUrl)
            : base(appmanager)
        {
            this.baseUrl = baseUrl;
        }


        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedInAs(account))
                {
                    // Don't need to do anything if we logged in
                    // under the required account
                    return;
                }

                // Do logout if the account is other than we need.
                Logout();
            }

            Type(By.Id("username"), account.Username);
            driver.FindElement(By.XPath(loginBtnXPath)).Click();

            // Wait for password-field to appear
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
            wait.Until(ExpectedConditions.ElementExists(By.Id("password")));

            Type(By.Id("password"), account.Password);
            driver.FindElement(By.XPath(loginBtnXPath)).Click();
        }


        public bool IsLoggedIn()
        {
            return IsElementPresent(By.CssSelector("span.user-info"));
        }


        public bool IsLoggedInAs(AccountData account)
        {
            return IsLoggedIn()
                && GetLoggedUsername() == account.Username;
        }


        public void Logout()
        {
            if (IsLoggedIn())
            {
                driver.Navigate().GoToUrl(baseUrl + "logout_page.php");
            }
        }


        public string GetLoggedUsername()
        {
            return driver.FindElement(By.CssSelector("span.user-info")).Text;
        }
    }
}
