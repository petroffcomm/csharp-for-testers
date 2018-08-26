using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MantisTests
{
    [TestFixture]
    public class AccountCreationTests : TestBase
    {
        [TestFixtureSetUp]
        public void SetUpNewMantisConfig()
        {
            app.Ftp.BackupFile("/config_inc.php");

            using (Stream localFile = File.Open("config_inc.php", FileMode.Open))
            {
                app.Ftp.Upload("/config_inc.php", localFile);
            }
        }


        [Test]
        public void TestAccountRegistration()
        {
            AccountData account = new AccountData()
            {
                Username = "testuser",
                Password = "passwd",
                EMail = "testuser@localhost.localdomain"

            };

            app.Registration.Register(account);
        }


        [TestFixtureTearDown]
        public void RestoreMantisConfig()
        {
            app.Ftp.RestoreBackupFile("/config_inc.php");
        }
    }
}
