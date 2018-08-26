using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MantisTests
{
    public class RegUserHelper : HelperBase
    {
        public RegUserHelper(ApplicationManager appmanager) : base(appmanager) { }

        internal void Register(AccountData account)
        {
            OpenMainPage();
            OpenRegistrationForm();
            FillRegistrationForm(account);
            SubmitRegistration();
        }


        private void OpenMainPage()
        {
            driver.Navigate().GoToUrl(@"http://localhost/mantisbt-2.16.0/login_page.php");
        }

        
        private void OpenRegistrationForm()
        {
            driver.FindElement(By.PartialLinkText("зарегистрировать")).Click();
        }


        private void FillRegistrationForm(AccountData account)
        {
            Type(By.Name("username"), account.Username);
            Type(By.Name("email"), account.EMail);
        }


        private void SubmitRegistration()
        {
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();
        }
    }
}
