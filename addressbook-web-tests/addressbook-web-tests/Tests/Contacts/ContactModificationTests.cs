using System;
using System.Text;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : TestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData contact = new ContactData();
            string postfix = DateTime.Now.ToString();
            contact.FirstName = "New FirstName - " + postfix;
            contact.LastName = "New LastName - " + postfix;

            app.Contacts.Edit(1, contact);
        }
    }
}
