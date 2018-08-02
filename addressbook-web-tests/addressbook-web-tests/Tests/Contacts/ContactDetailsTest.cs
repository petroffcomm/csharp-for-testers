using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactDetailsTests : ContactTestBase
    {
        [Test]
        public void ContactDetailsTest()
        {
            CreateContactForTestIfNecessary();

            int recordToCheck = 0;
            ContactData fromForm = app.Contacts.GetContactInformationFormEditForm(recordToCheck);
            string contactFromFormPrepared = Utils.PrepareDetailedViewForContact(fromForm);

            string contactDetails = app.Contacts.GetContactInformationFormDetailedView(recordToCheck);

            /**Console.WriteLine("------");
            Console.WriteLine(contactDetails);
            Console.WriteLine("------");
            Console.WriteLine(contactFromFormPrepared);
            Console.WriteLine("------");**/

            Assert.AreEqual(contactFromFormPrepared, contactDetails);
        }
    }
}
