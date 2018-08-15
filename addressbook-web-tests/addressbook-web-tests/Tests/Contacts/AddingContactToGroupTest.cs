using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AddingContactToGroupTest : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
        {
            // Preparation section
            CreateGroupForTestIfNecessary();
            CreateContactForTestIfNecessary();


            GroupData group = GroupData.GetAllRecordsFromDB()[0];
            List<ContactData> oldContactsList = group.GetContacts();
            ContactData contact;
            if (oldContactsList.Count() == 0)
            {
                // if group doesn't contain any contact then just take first existing
                // to add to (nothing to exclude)
                contact = ContactData.GetActiveRecordsFromDB().First();
            }
            else
            {
                if (ContactData.GetActiveRecordsFromDB().Except(oldContactsList).Count() == 0)
                    CreateContactForTestIfNecessary(true);

                contact = ContactData.GetActiveRecordsFromDB().Except(oldContactsList).First();
            }
            

            // Test section
            app.Contacts.AddContactToGroup(contact, group);

            List<ContactData> newContactsList = group.GetContacts();


            oldContactsList.Add(contact);
            oldContactsList.Sort();
            newContactsList.Sort();
            Assert.AreEqual(oldContactsList, newContactsList);

        }
    }
}
