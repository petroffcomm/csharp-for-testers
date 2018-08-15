using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        private List<ContactData> contactCache;

        public ContactHelper(ApplicationManager appmanager)
            : base(appmanager)
        {
        }


        public ContactHelper Create(ContactData contact)
        {
            appmanager.Naviator.OpenHomePage();
            InitContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToHomePage();

            return this;
        }


        public ContactHelper EditByIndex(int contactIndex, ContactData newContactData)
        {
            appmanager.Naviator.OpenHomePage();
            InitContactModificationByIndex(contactIndex);
            FillContactForm(newContactData);
            SubmitContactModification();
            ReturnToHomePage();

            return this;
        }


        public ContactHelper EditById(ContactData newContactData)
        {
            appmanager.Naviator.OpenHomePage();
            // newContactData object is considered to contain
            // Id-value of object to modify
            InitContactModificationById(newContactData.Id);
            FillContactForm(newContactData);
            SubmitContactModification();
            ReturnToHomePage();

            return this;
        }


        public ContactHelper DeleteByIndex(int contactIndex)
        {
            appmanager.Naviator.OpenHomePage();
            SelectContactByIndex(contactIndex);
            RemoveContact();
            appmanager.Naviator.OpenHomePage();

            return this;
        }


        public ContactHelper DeleteById(string contactId)
        {
            appmanager.Naviator.OpenHomePage();
            SelectContactById(contactId);
            RemoveContact();
            appmanager.Naviator.OpenHomePage();

            return this;
        }


        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            appmanager.Naviator.OpenHomePage();
            ClearGroupFilter();
            SelectContactById(contact.Id);
            SelectGroupToAdd(group.Id);
            CommitAddingContactToGroup();

            // wait until operation is completed - just to be sure
            // we've alredy done that before going further
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }


        public void RemoveContactFromGroup(ContactData contact, GroupData group)
        {
            appmanager.Naviator.OpenHomePage();
            SetFilterForGroup(group);
            SelectContactById(contact.Id);
            CommitContactRemovalFromGroup();

            // wait until operation is completed - just to be sure
            // we've alredy done that before going further
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }


        public List<ContactData> GetContactsList()
        {
            if (contactCache == null)
            {
                appmanager.Naviator.OpenHomePage();
                contactCache = new List<ContactData>();

                ICollection<IWebElement> tableRecords = driver.FindElements(By.Name("entry"));

                foreach (IWebElement contactTableRec in tableRecords)
                {
                    ContactData contact = BuildContactFromContactsTableEntry(contactTableRec);

                    contactCache.Add(contact);
                }
            }

            return new List<ContactData>(contactCache);
        }


        public ContactData GetContactInformationFormEditForm(int index)
        {
            appmanager.Naviator.OpenHomePage();
            InitContactModificationByIndex(index);

            ContactData contact = new ContactData();
            contact.Id = driver.FindElement(By.XPath("//form[@action='edit.php']//input[@type='hidden']")).GetAttribute("value");
            contact.FirstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            contact.MiddleName = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            contact.LastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            contact.Nickname = driver.FindElement(By.Name("nickname")).GetAttribute("value");
            contact.Company = driver.FindElement(By.Name("company")).GetAttribute("value");
            contact.Title = driver.FindElement(By.Name("title")).GetAttribute("value");
            contact.PrimaryAddress = driver.FindElement(By.Name("address")).GetAttribute("value");
            contact.SecondaryAddress = driver.FindElement(By.Name("address2")).GetAttribute("value");
            contact.HomePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            contact.MobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            contact.WorkPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            contact.Fax = driver.FindElement(By.Name("fax")).GetAttribute("value");
            contact.SecondaryPhone = driver.FindElement(By.Name("phone2")).GetAttribute("value");
            contact.Email1 = driver.FindElement(By.Name("email")).GetAttribute("value");
            contact.Email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            contact.Email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");
            contact.HomePage = driver.FindElement(By.Name("homepage")).GetAttribute("value")
                                                                      .Replace("http://", "")
                                                                      .Replace("https://", "");
            contact.Notes = driver.FindElement(By.Name("notes")).GetAttribute("value");

            return contact;
        }


        public ContactData GetContactInfoFromTableByIndex(int index)
        {
            appmanager.Naviator.OpenHomePage();
            IWebElement contactsTableRecord = driver.FindElements(By.Name("entry"))[index];

            return BuildContactFromContactsTableEntry(contactsTableRecord);
        }


        private ContactData BuildContactFromContactsTableEntry(IWebElement tableRecord)
        {
            IList<IWebElement> cells = tableRecord.FindElements(By.TagName("td"));

            ContactData contact = new ContactData()
            {
                FirstName = cells[2].Text,
                LastName = cells[1].Text,
                PrimaryAddress = cells[3].Text,
                AllEmails = cells[4].Text,
                AllPhones = cells[5].Text,
                Id = cells[0].FindElement(By.TagName("input")).GetAttribute("id")
            };

            return contact;
        }


        public string GetContactInformationFormDetailedView(int index)
        {
            appmanager.Naviator.OpenHomePage();
            ViewContactDetailsByIndex(index);
            string content = driver.FindElement(By.Id("content")).Text
                /**.Replace("<br>", "")
                .Replace("<b>", "")
                .Replace("</b>", "")
                .Replace("<i>", "")
                .Replace("</i>", "")**/;

            return content;
        }


        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }


        public ContactHelper InitContactModificationByIndex(int index)
        {
            /**index += 1;
            driver.FindElement(
                By.XPath("(//*[@id='maintable']//img[@alt='Edit'])[" + index + "]"))
                .Click();**/

            // Alternative implementation
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();

            return this;
        }


        public ContactHelper InitContactModificationById(string id)
        {
            driver.FindElement(
                // get parent row of the checkbox to access required cells
                By.XPath("//*[@name='entry']//*[@id='" + id + "']/parent::*/parent::*"))
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a"))
                .Click();

            return this;
        }


        public ContactHelper ViewContactDetailsByIndex(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[6]
                .FindElement(By.TagName("a")).Click();
            return this;
        }


        public ContactHelper SelectContactByIndex(int index)
        {
            index += 1;

            driver.FindElement(
                By.XPath("(//table[@id='maintable']//input[@type='checkbox'])[" + index + "]"))
                .Click();

            return this;
        }


        public ContactHelper SelectContactById(string id)
        {
            driver.FindElement(
                By.XPath("//table[@id='maintable']//input[@type='checkbox'][@id=" + id + "]"))
                .Click();

            return this;
        }


        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.FirstName);

            Type(By.Name("middlename"), contact.MiddleName);
            Type(By.Name("lastname"), contact.LastName);
            Type(By.Name("nickname"), contact.Nickname);
            Type(By.Name("title"), contact.Title);
            Type(By.Name("company"), contact.Company);
            Type(By.Name("address"), contact.PrimaryAddress);
            Type(By.Name("home"), contact.HomePhone);
            Type(By.Name("mobile"), contact.MobilePhone);
            Type(By.Name("work"), contact.WorkPhone);
            Type(By.Name("fax"), contact.Fax);
            Type(By.Name("email"), contact.Email1);
            Type(By.Name("email2"), contact.Email2);
            Type(By.Name("email3"), contact.Email3);

            SelectDropdownItemByText(By.Name("bday"), contact.BDay);
            SelectDropdownItemByText(By.Name("bmonth"), contact.BMonth);
            Type(By.Name("byear"), contact.BYear);

            SelectDropdownItemByText(By.Name("aday"), contact.ADay);
            SelectDropdownItemByText(By.Name("amonth"), contact.AMonth);
            Type(By.Name("ayear"), contact.AYear);

            Type(By.Name("address2"), contact.SecondaryAddress);
            Type(By.Name("phone2"), contact.SecondaryPhone);
            Type(By.Name("notes"), contact.Notes);

            return this;
        }


        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("(//input[@name='submit'])[2]")).Click();
            // Reset cache
            contactCache = null;

            return this;
        }


        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.XPath("//input[@name='update']")).Click();
            // Reset cache
            contactCache = null;

            return this;
        }


        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@type='button'][@onclick='DeleteSel()']")).Click();
            driver.SwitchTo().Alert().Accept();
            // Reset cache
            contactCache = null;

            return this;
        }


        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }


        public int Count()
        {
            appmanager.Naviator.OpenHomePage();
            return driver.FindElements(By.XPath("//table[@id='maintable']//input[@name='selected[]']")).Count;
        }


        public int GetNumberOfSearchResults()
        {
            appmanager.Naviator.OpenHomePage();
            return int.Parse(driver.FindElement(By.Id("search_count")).Text);

            // By using Regex
            /** string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value); **/
        }

        public void ClearGroupFilter()
        {
            SelectDropdownItemByText(By.Name("group"), "[all]");
        }


        private void SetFilterForGroup(GroupData group)
        {
            SelectDropdownItemById(By.Name("group"), group.Id);
        }


        public void SelectGroupToAdd(string Id)
        {
            SelectDropdownItemById(By.Name("to_group"), Id);
        }


        public void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }


        public void CommitContactRemovalFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }
    }
}
