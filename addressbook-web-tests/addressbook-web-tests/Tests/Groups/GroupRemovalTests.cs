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

            List<GroupData> oldGroups = app.Groups.GetGroupList();
            int groupToRemove = 0;
            app.Groups.Delete(groupToRemove);
            // First check if lists' sizes are equal
            Assert.AreEqual(oldGroups.Count - 1, app.Groups.Count());

            List<GroupData> newGroups = app.Groups.GetGroupList();

            oldGroups.RemoveAt(groupToRemove);
            Assert.AreEqual(newGroups, oldGroups);
        }
    }
}
