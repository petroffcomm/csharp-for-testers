using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_tests_white
{
    public class GroupRemovalTests : TestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            // That's because application requires at lest one group
            // to be alive.
            if (oldGroups.Count() == 1)
            {
                GroupData group = new GroupData()
                {
                    Name = "group_to_remove"
                };

                app.Groups.Create(group);

                // Refresh oldGroups after new group creation
                oldGroups = app.Groups.GetGroupList();
            }


            int index = oldGroups.Count - 1;
            GroupData groupToRemove = oldGroups[index];
            app.Groups.Delete(index);

            List<GroupData> newGroups = app.Groups.GetGroupList();

            oldGroups.Remove(groupToRemove);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
