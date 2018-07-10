﻿using System;
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

        public NavigationHelper(IWebDriver driver, string baseUrl)
            : base(driver)
        {
            this.baseUrl = baseUrl;
        }
        public void OpenHomePage()
        {
            driver.Navigate().GoToUrl(baseUrl + "addressbook/index.php");
        }

        public void GoToGroupsPage()
        {
            driver.FindElement(By.LinkText("groups")).Click();
        }
    }
}
