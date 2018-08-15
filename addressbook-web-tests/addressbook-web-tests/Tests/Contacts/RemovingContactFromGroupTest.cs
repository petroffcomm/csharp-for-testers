using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class RemovingContactFromGroupTest : AuthTestBase
    {
        [Test]
        public void TestRemovingContactFromGroup()
        {
            // Preparation section
            GroupData group = GroupData.GetNonEmptyGroup();

            if (group == null)
            {
                // We can't say for sure if any of these objects' instance exists,
                // so we need to check this out and to create if necessary
                CreateGroupForTestIfNecessary();
                CreateContactForTestIfNecessary();

                group = GroupData.GetAllRecordsFromDB()[0];
                ContactData contactCreated = ContactData.GetActiveRecordsFromDB()[0];
                app.Contacts.AddContactToGroup(contactCreated, group);
            }


            // Test section
            List<ContactData> oldContactsList = group.GetContacts();
            ContactData contact = oldContactsList[0];

            app.Contacts.RemoveContactFromGroup(contact, group);

            List<ContactData> newContactsList = group.GetContacts();


            oldContactsList.Remove(contact);
            oldContactsList.Sort();
            newContactsList.Sort();
            Assert.AreEqual(oldContactsList, newContactsList);
        }
    }
}
