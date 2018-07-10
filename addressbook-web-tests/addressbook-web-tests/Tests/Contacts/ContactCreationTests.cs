using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData();
            contact.FirstName = "Tester";
            contact.LastName = "Tester";

            AccountData account = new AccountData("admin", "secret");

            app.Naviator.OpenHomePage();
            app.Auth.Login(account);
            app.Contacts.InitContactCreation();
            app.Contacts.FillContactForm(contact);
            app.Contacts.SubmitContactCreation();
            app.Contacts.ReturnToHomePage();
            app.Auth.Logout();
        }
    }
}
