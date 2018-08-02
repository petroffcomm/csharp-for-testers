using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    [TestFixture]
    public class SearchTests : AuthTestBase
    {
        [Test]
        public void TestContactSearch()
        {
            int resultAmount = app.Contacts.GetNumberOfSearchResults();
            int recordsAmount = app.Contacts.Count();
            Assert.AreEqual(resultAmount, recordsAmount);
        }
    }
}
