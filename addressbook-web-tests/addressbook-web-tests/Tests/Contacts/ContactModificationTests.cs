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

            int contactNumToModify = 5;
            string postfix = DateTime.Now.ToString();
            ContactData contact = new ContactData()
            {
                FirstName = "New FirstName - " + postfix,
                LastName = "New LastName - " + postfix
            };

            List<ContactData> oldContacts = ContactData.GetAllRecordsFromDB();

            contact.Id = oldContacts[contactNumToModify].Id;
            app.Contacts.EditById(contact);

            List<ContactData> newContacts = ContactData.GetAllRecordsFromDB();

            // Perform contact modification in old list
            oldContacts[contactNumToModify].FirstName = contact.FirstName;
            oldContacts[contactNumToModify].LastName = contact.LastName;

            // Compare results
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
