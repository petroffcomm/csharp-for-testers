﻿using System;
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

        public ContactHelper Edit(int contactIndex, ContactData newContactData)
        {
            appmanager.Naviator.OpenHomePage();
            InitContactModification(contactIndex);
            FillContactForm(newContactData);
            SubmitContactModification();
            ReturnToHomePage();

            return this;
        }

        public ContactHelper Delete(int contactIndex)
        {
            appmanager.Naviator.OpenHomePage();
            SelectContactByIndex(contactIndex);
            RemoveContact();
            appmanager.Naviator.OpenHomePage();

            return this;
        }

        internal List<ContactData> GetContactsList()
        {
            if (contactCache == null)
            {
                appmanager.Naviator.OpenHomePage();
                contactCache = new List<ContactData>();

                ICollection<IWebElement> tableRecords = driver.FindElements(By.Name("entry"));

                foreach (IWebElement contactTableRec in tableRecords)
                {
                    /**IList<IWebElement> contactTableRecCells = contactTableRec.FindElements(By.TagName("td"));

                    ContactData contact = new ContactData()
                    {
                        FirstName = contactTableRecCells[2].Text,
                        LastName = contactTableRecCells[1].Text,
                        Id = contactTableRecCells[0].FindElement(By.TagName("input")).GetAttribute("id")
                    };**/
                    ContactData contact = BuildContactFromContactsTableEntry(contactTableRec);

                    contactCache.Add(contact);
                }
            }

            return new List<ContactData>(contactCache);
        }

        public ContactData GetContactInfoFormTableByIndex(int index)
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

        public ContactData GetContactInformationFormEditForm(int index)
        {
            appmanager.Naviator.OpenHomePage();
            InitContactModification(index);

            ContactData contact = new ContactData();
            contact.Id = driver.FindElement(By.XPath("//form[@action='edit.php']//input[@type='hidden']")).GetAttribute("value");
            contact.FirstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            contact.LastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            contact.PrimaryAddress = driver.FindElement(By.Name("address")).GetAttribute("value");
            contact.HomePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            contact.MobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            contact.WorkPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            contact.Email1 = driver.FindElement(By.Name("email")).GetAttribute("value");
            contact.Email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            contact.Email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return contact;
        }

        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper InitContactModification(int index)
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

        public ContactHelper SelectContactByIndex(int index)
        {
            index += 1;

            driver.FindElement(
                By.XPath("(//table[@id='maintable']//input[@type='checkbox'])[" + index + "]"))
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

            if (contact.BDay != "")
                new SelectElement(driver.FindElement(By.Name("bday"))).SelectByText(contact.BDay);
            new SelectElement(driver.FindElement(By.Name("bmonth"))).SelectByText(contact.BMonth);
            Type(By.Name("byear"), contact.BYear);

            if (contact.ADay != "")
                new SelectElement(driver.FindElement(By.Name("aday"))).SelectByText(contact.ADay);
            new SelectElement(driver.FindElement(By.Name("amonth"))).SelectByText(contact.AMonth);

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
    }
}
