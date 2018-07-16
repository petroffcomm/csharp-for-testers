using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : GroupTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            CreateGroupForTestIfNecessary();

            string postfix = DateTime.Now.ToString();
            GroupData newGroupData = new GroupData("gname_new - " + postfix);
            newGroupData.Header = null;
            newGroupData.Footer = null;
            //newGroupData.Header = "gheader_new";
            //newGroupData.Footer = "gfooter_new";

            app.Groups.Modify(1, newGroupData);
        }
    }
}
