using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class GroupTestBase : AuthTestBase
    {
        public void CreateGroupForTestIfNecessary()
        {
            if (app.Groups.Count() == 0)
            {
                GroupData group = new GroupData("group_for_some_action");
                app.Groups.Create(group);
            }
        }
    }
}
