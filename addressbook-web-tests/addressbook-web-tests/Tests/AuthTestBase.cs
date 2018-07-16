using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AuthTestBase : TestBase
    {
        [SetUp]
        public void DoLogin()
        {
            // This part of code was moved here from TestBase inheritants because
            // all our tests require these actions at the same beginning.
            app.Auth.Login(new AccountData("admin", "secret"));
        }
    }
}
