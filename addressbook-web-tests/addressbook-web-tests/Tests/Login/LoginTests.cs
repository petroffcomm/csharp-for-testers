using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void LoginWithValidCredentials()
        {
            app.Auth.Logout();

            AccountData acc = new AccountData("admin", "secret");
            app.Auth.Login(acc);
            Assert.IsTrue(app.Auth.IsLoggedInAs(acc));
        }

        [Test]
        public void LoginWithInvalidCredentials()
        {
            app.Auth.Logout();

            AccountData acc = new AccountData("admin", "123456");
            app.Auth.Login(acc);
            Assert.IsFalse(app.Auth.IsLoggedInAs(acc));
        }

    }
}
