using System;
using System.Text;
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

            app.Contacts.Edit(1, contact);
        }
    }
}
