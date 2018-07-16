﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace WebAddressbookTests
{
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager appmanager) 
            : base(appmanager)
        {
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
            Type(By.Name("user"), account.Username);
            Type(By.Name("pass"), account.Password);
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.Name("logout"));
        }

        public bool IsLoggedInAs(AccountData account)
        {
            return IsLoggedIn()
                && driver.FindElement(By.Name("logout"))
                         .FindElement(By.TagName("b")).Text == "(" + account.Username + ")";
        }

        public void Logout()
        {
            if (IsLoggedIn())
            {
                driver.FindElement(By.LinkText("Logout")).Click();
            }
        }
    }
}
