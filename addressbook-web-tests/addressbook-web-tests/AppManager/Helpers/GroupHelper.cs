using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace WebAddressbookTests
{
    public class GroupHelper : HelperBase
    {

        public GroupHelper(ApplicationManager appmanager)
            : base(appmanager)
        {
        }

        public GroupHelper Create(GroupData group)
        {
            appmanager.Naviator.GoToGroupsPage();
            InitNewGroupCreation();
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupsPage();

            return this;
        }

        internal GroupHelper Modify(int groupIndex, GroupData newGroupData)
        {
            appmanager.Naviator.GoToGroupsPage();
            SelectGroup(groupIndex);
            InitNewGroupModification();
            FillGroupForm(newGroupData);
            SubmitGroupModification();
            ReturnToGroupsPage();

            return this;
        }

        public GroupHelper Delete(int groupIndex)
        {
            appmanager.Naviator.GoToGroupsPage();
            SelectGroup(groupIndex);
            RemoveGroup();
            ReturnToGroupsPage();

            return this;
        }

        public List<GroupData> GetGroupList()
        {
            List<GroupData> groups = new List<GroupData>();
            appmanager.Naviator.GoToGroupsPage();
            ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group"));

            foreach (IWebElement element in elements)
            {
                groups.Add(new GroupData(element.Text));
            }

            return groups;
        }

        public GroupHelper InitNewGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
            return this;
        }

        public GroupHelper FillGroupForm(GroupData group)
        {
            Type(By.Name("group_name"), group.Name);
            Type(By.Name("group_header"), group.Header);
            Type(By.Name("group_footer"), group.Footer);
            return this;
        }

        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }

        public GroupHelper InitNewGroupModification()
        {
            driver.FindElement(By.Name("edit")).Click();
            return this;
        }

        public GroupHelper SubmitGroupModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public GroupHelper ReturnToGroupsPage()
        {
            driver.FindElement(By.LinkText("group page")).Click();
            return this;
        }

        public GroupHelper SelectGroup(int index)
        {
            index += 1;
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
            return this;
        }

        public GroupHelper RemoveGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            return this;
        }

        public int Count()
        {
            appmanager.Naviator.GoToGroupsPage();
            return driver.FindElements(By.ClassName("group")).Count;
        }
    }
}
