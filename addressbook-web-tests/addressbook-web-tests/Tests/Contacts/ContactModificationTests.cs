using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : ContactTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            CreateContactForTestIfNecessary();
            int contactToModify = 0;
            ContactData contact = new ContactData();
            string postfix = DateTime.Now.ToString();
            contact.FirstName = "New FirstName - " + postfix;
            contact.LastName = "New LastName - " + postfix;

            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.Edit(contactToModify, contact);

            List<ContactData> newContacts = app.Contacts.GetContactsList();

            oldContacts[contactToModify].FirstName = contact.FirstName;
            oldContacts[contactToModify].LastName = contact.LastName;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
