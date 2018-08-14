using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : ContactTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            CreateContactForTestIfNecessary();
            int contactNumToRemove = 0;

            List<ContactData> oldContacts = ContactData.GetAllRecordsFromDB();

            ContactData contactToRemove = oldContacts[contactNumToRemove];
            app.Contacts.DeleteById(contactToRemove.Id);

            List<ContactData> newContacts = ContactData.GetAllRecordsFromDB();

            oldContacts.RemoveAt(contactNumToRemove);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
