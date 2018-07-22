using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : ContactTestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData();
            string postfix = DateTime.Now.ToString();
            contact.FirstName = "TesterFname " + postfix;
            contact.LastName = "TesterLname " + postfix;

            List<ContactData> oldContacts = app.Contacts.GetContactsList();
            app.Contacts.Create(contact);

            List<ContactData> newContacts = app.Contacts.GetContactsList();

            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]
        public void EmptyContactCreationTest()
        {
            ContactData contact = new ContactData();
            /**contact.FirstName = null;
            contact.LastName = null;**/

            app.Contacts.Create(contact);
        }
    }
}
