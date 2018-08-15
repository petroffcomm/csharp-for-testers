using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;
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
                    Fax = GetRandomAllowedStringFor(PHONE, 9),
                    SecondaryPhone = GetRandomAllowedStringFor(PHONE, 9),
                    PrimaryAddress = GetRandomAllowedStringFor(GENERAL, 40)
                });
            }

            return contacts;
        }


        public static IEnumerable<ContactData> ContactDataFromXmlFile()
        {
            return (List<ContactData>)
                new XmlSerializer(typeof(List<ContactData>))
                .Deserialize(new StreamReader(@"contacts.xml"));
        }


        public static IEnumerable<ContactData> ContactDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(
                File.ReadAllText(@"contacts.json"));
        }


        [Test, TestCaseSource("ContactDataFromJsonFile")]
        public void ContactCreationTest(ContactData contact)
        {
            List<ContactData> oldContacts = ContactData.GetActiveRecordsFromDB();

            app.Contacts.Create(contact);

            List<ContactData> newContacts = ContactData.GetActiveRecordsFromDB();

            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
        }
        

        [Test]
        public void TestDBConnectivity()
        {
            DateTime start = DateTime.Now;
            List<ContactData> fromUI = app.Contacts.GetContactsList();
            DateTime end = DateTime.Now;
            Console.WriteLine(end.Subtract(start));


            start = DateTime.Now;
            List<ContactData> fromDb = ContactData.GetActiveRecordsFromDB();
            end = DateTime.Now;
            Console.WriteLine(end.Subtract(start));

            fromDb.Sort();
            fromUI.Sort();
            Assert.AreEqual(fromDb, fromUI);
        }
    }
}
