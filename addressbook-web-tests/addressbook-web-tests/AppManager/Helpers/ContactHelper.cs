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

        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            driver.FindElement(
                By.XPath("(//*[@id='maintable']//img[@alt='Edit'])[" + index + "]"))
                .Click();
            return this;
        }

        public ContactHelper SelectContactByIndex(int index)
        {
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
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.XPath("//input[@name='update']")).Click();
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@type='button'][@onclick='DeleteSel()']")).Click();
            driver.SwitchTo().Alert().Accept();
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
