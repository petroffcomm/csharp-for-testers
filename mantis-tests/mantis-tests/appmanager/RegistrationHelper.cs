using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MantisTests
{
    public class RegUserHelper : HelperBase
    {
        public RegUserHelper(ApplicationManager appmanager) : base(appmanager) { }

        public void Register(AccountData account)
        {
            OpenMainPage();
            OpenRegistrationForm();
            FillRegistrationForm(account);
            SubmitRegistration();

            String url = GetConfirmationUrl(account);
            driver.Url = url;
            FillPasswordForm(account);
            SubmitPasswordForm();
        }


        private string GetConfirmationUrl(AccountData account)
        {
            string msg = appmanager.Mail.GetLastMail(account);
            Match match = Regex.Match(msg, @"http://\S*");

            return match.Value;
        }


        private void FillPasswordForm(AccountData account)
        {
            Type(By.Id("password"), account.Password);
            Type(By.Id("password-confirm"), account.Password);
        }


        private void SubmitPasswordForm()
        {
            driver.FindElement(By.ClassName("submit-button"))
                  .FindElement(By.TagName("button")).Click();
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
