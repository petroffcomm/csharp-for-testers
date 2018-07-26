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
            int contactToRemove = 0;

            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.Delete(contactToRemove);

            List<ContactData> newContacts = app.Contacts.GetContactsList();

            oldContacts.RemoveAt(contactToRemove);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
