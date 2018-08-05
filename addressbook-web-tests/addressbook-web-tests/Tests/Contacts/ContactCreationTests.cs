using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : ContactTestBase
    {
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();

            for (int i = 0; i < 3; i++)
            {
                contacts.Add(new ContactData() {
                    FirstName = GetRandomAllowedStringFor(GENERAL, 20),
                    LastName = GetRandomAllowedStringFor(GENERAL, 20),
                    Email1 = GetRandomAllowedStringFor(EMAIL, 30),
                    Email2 = GetRandomAllowedStringFor(EMAIL, 30),
                    Email3 = GetRandomAllowedStringFor(EMAIL, 30),
                    HomePhone = GetRandomAllowedStringFor(PHONE, 9),
                    MobilePhone = GetRandomAllowedStringFor(PHONE, 9),
                    WorkPhone = GetRandomAllowedStringFor(PHONE, 9),
                });
            }

            return contacts;
        }


        [Test, TestCaseSource("RandomContactDataProvider")]
        public void ContactCreationTest(ContactData contact)
        {
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
