using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : GroupTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            CreateGroupForTestIfNecessary();

            List<GroupData> oldGroups = GroupData.GetAllRecordsFromDB();

            int groupNumToRemove = 0;
            string groupIdToRemove = oldGroups[groupNumToRemove].Id;
            app.Groups.DeleteById(groupIdToRemove);
            
            // First check if lists' sizes are equal
            Assert.AreEqual(oldGroups.Count - 1, app.Groups.Count());

            List<GroupData> newGroups = GroupData.GetAllRecordsFromDB();

            oldGroups.RemoveAt(groupNumToRemove);
            Assert.AreEqual(newGroups, oldGroups);
        }
    }
}
