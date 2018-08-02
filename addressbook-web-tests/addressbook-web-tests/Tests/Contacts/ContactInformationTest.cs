using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : ContactTestBase
    {
        [Test]
        public void ContactInformationTest()
        {
            CreateContactForTestIfNecessary();

            int recordToCheck = 0;
            ContactData fromTable = app.Contacts.GetContactInfoFromTableByIndex(recordToCheck);
            ContactData fromForm = app.Contacts.GetContactInformationFormEditForm(recordToCheck);

            //verification
            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
        }
    }
}
