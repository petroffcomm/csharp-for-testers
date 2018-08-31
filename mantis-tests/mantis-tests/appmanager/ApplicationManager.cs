using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace MantisTests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected string baseURL;

        public LoginHelper Auth { get; }
        public ProjectManagementHelper ProjectsAdministration { get; }
        public AdminHelper AdminNavigator { get; }
        public UserNavigation UserNavigator { get; }
        public APIHelper API { get; }

        public RegUserHelper Registration { get; }
        public FtpHelper Ftp { get; }

        private static ApplicationManager instance;

        // We want to store app-manager for each individual thread
        // just for the parallel test execution case (when we need
        // a few independent browsers).
        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();


        private ApplicationManager()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.BrowserExecutableLocation = @"c:\Program Files\Mozilla Firefox\firefox.exe";
            options.UseLegacyImplementation = true;
            driver = new FirefoxDriver(options);
            baseURL = "http://localhost/mantisbt-2.16.0";

            Auth =  new LoginHelper(this, baseURL);
            ProjectsAdministration = new ProjectManagementHelper(this);
            AdminNavigator = new AdminHelper(this);
            UserNavigator = new UserNavigation(this, baseURL);
            API = new APIHelper(this);

            Registration = new RegUserHelper(this);
            Ftp = new FtpHelper(this);

        }

        // destructor. Called automatically.
        ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        public static ApplicationManager GetInstance()
        {
            if (! app.IsValueCreated)
            {
                app.Value = new ApplicationManager();
            }

            return app.Value;
        }

        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }
    }
}
