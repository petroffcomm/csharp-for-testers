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
            
            List<GroupData> oldGroups = GroupData.GetAllRecordsFromDB();

            int groupNumToModify = 0;
            string groupIdToModify = oldGroups[groupNumToModify].Id;

            string postfix = DateTime.Now.ToString();
            GroupData newGroupData = new GroupData("gname_new - " + postfix);
            newGroupData.Header = null;
            newGroupData.Footer = null;
            //newGroupData.Header = "gheader_new";
            //newGroupData.Footer = "gfooter_new";

            app.Groups.ModifyById(groupIdToModify, newGroupData);

            List<GroupData> newGroups = GroupData.GetAllRecordsFromDB();

            // First check if lists' sizes are equal
            Assert.AreEqual(oldGroups.Count, app.Groups.Count());

            oldGroups[groupNumToModify].Name = newGroupData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
