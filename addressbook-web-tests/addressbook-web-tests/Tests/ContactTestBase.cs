using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class ContactTestBase : AuthTestBase
    {
        public void CreateContactForTestIfNecessary()
        {
            if (app.Contacts.Count() == 0)
            {
                ContactData contact = new ContactData();
                contact.FirstName = "contact_for_some_action";

                app.Contacts.Create(contact);
            }
        }
    }
}