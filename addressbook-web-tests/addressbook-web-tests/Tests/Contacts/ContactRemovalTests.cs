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

            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.Delete(0);

            List<ContactData> newContacts = app.Contacts.GetContactsList();

            oldContacts.RemoveAt(0);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
