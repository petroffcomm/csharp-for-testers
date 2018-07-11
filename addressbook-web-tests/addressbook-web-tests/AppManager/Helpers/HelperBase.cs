using OpenQA.Selenium;

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
    }
}