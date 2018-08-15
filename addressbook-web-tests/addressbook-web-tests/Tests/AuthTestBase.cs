using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AuthTestBase : TestBase
    {
        // Was moved here from ContactTestBase to give any derived test the ability 
        // to create contacts
        // 'forceCreation'-parameter is used when we need to create object in any case
        public void CreateContactForTestIfNecessary(bool forceCreation=false)
        {
            if (app.Contacts.Count() == 0 || forceCreation)
            {
                ContactData contact = new ContactData();
                contact.FirstName = "contact_for_some_action";
                contact.LastName = "test lastname";

                app.Contacts.Create(contact);
            }
        }


        // Was moved here from GroupTestBase to give any derived test the ability
        // to create groupd.
        // 'forceCreation'-parameter is used when we need to create object in any case
        public void CreateGroupForTestIfNecessary(bool forceCreation = false)
        {
            if (app.Groups.Count() == 0 || forceCreation)
            {
                GroupData group = new GroupData("group_for_some_action");
                app.Groups.Create(group);
            }
        }


        [SetUp]
        public void DoLogin()
        {
            // This part of code was moved here from TestBase inheritants because
            // all our tests require these actions at the same beginning.
            app.Auth.Login(new AccountData("admin", "secret"));
        }
    }
}
