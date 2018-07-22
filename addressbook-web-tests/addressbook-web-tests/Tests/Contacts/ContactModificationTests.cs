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

            ContactData contact = new ContactData();
            string postfix = DateTime.Now.ToString();
            contact.FirstName = "New FirstName - " + postfix;
            contact.LastName = "New LastName - " + postfix;

            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.Edit(0, contact);

            List<ContactData> newContacts = app.Contacts.GetContactsList();

            oldContacts[0].FirstName = contact.FirstName;
            oldContacts[0].LastName = contact.LastName;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
