using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : TestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            GroupData newGroupData = new GroupData("gname_new");
            newGroupData.Header = "gheader_new";
            newGroupData.Footer = "gfooter_new";

            app.Groups.Modify(1, newGroupData);
        }
    }
}
