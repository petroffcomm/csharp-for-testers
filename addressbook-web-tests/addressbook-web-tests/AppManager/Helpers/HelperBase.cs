using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class HelperBase
    {
        protected ApplicationManager appmanager;
        protected IWebDriver driver;

        public HelperBase(ApplicationManager appmanager)
        {
            this.appmanager = appmanager;
            this.driver = appmanager.Driver;
        }

        protected void Type(By locator, string text)
        {
            if (text != null)
            {
                driver.FindElement(locator).Clear();
                driver.FindElement(locator).SendKeys(text);
            }
        }

        protected void SelectDropdownItemByText(By locator, string text)
        {
            if (text != null && text != "")
            {
                new SelectElement(driver.FindElement(locator)).SelectByText(text);
            }
        }

        public bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch(NoSuchElementException)
            {
                return false;
            }
        }
    }
}