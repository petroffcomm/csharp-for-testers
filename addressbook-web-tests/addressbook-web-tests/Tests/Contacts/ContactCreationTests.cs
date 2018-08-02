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
            contact.Email1 = "test1@test.test";
            contact.Email2 = "test2@test.test";
            contact.Email3 = "test3@test.test";
            contact.HomePhone = "+38(057)846569 ";
            contact.MobilePhone = "+38(066)666-55-78";
            contact.WorkPhone = "+38(057)524-56-99";

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

            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.Create(contact);

            List<ContactData> newContacts = app.Contacts.GetContactsList();

            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
