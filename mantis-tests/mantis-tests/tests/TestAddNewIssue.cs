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
    public class AddNewIssueTests : TestBase
    {
        [Test]
        public void AddMewIssue()
        {
            AccountData account = new AccountData()
            {
                Username = "administrator",
                Password = "root"
            };

            ProjectData projectData = new ProjectData()
            {
                Id = "23"
            };

            Mantis.IssueData issueData = new Mantis.IssueData()
            {
                summary = "some text", description = "another piece of text", category = "General"
            };

            app.API.CreateNewIssue(account, projectData, issueData);
        }
    }
}
