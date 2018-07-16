﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : GroupTestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            GroupData group = new GroupData("gname_1");
            group.Header = "gheader_1";
            group.Footer = "gfooter_1";

            app.Groups.Create(group);
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            GroupData group = new GroupData("");
            group.Header = "";
            group.Footer = "";

            app.Groups.Create(group);
        }
    }
}
