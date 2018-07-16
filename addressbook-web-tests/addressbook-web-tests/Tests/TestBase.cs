using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class TestBase
    {
        protected ApplicationManager app;

        [SetUp]
        public void SetupApplicationManager()
        {
            app = ApplicationManager.GetInstance();

            // This part of code was moved here from TestBase inheritants because
            // all our tests require these actions at the same beginning.
            app.Naviator.OpenHomePage();
        }

        [TearDown]
        public void TeardownTest()
        {
            app = ApplicationManager.GetInstance();
            app.Auth.Logout();

            // Method implemenentation was moved to the ApplicationManager's destructor.
            //app.Stop();
        }
    }
}
