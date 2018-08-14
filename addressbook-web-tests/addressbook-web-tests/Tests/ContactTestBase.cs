﻿using System;
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

        [TearDown]
        public void CompareContactsUI_DB()
        {
            if (PERFORM_LONG_UI_CHECKS)
            {
                List<ContactData> fromUI = app.Contacts.GetContactsList();
                List<ContactData> fromDB = ContactData.GetAllRecordsFromDB();

                fromUI.Sort();
                fromDB.Sort();

                Assert.AreEqual(fromUI, fromDB);
            }
        }
    }
}