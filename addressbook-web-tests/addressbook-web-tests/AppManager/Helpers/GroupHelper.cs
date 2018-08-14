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
        private List<GroupData> groupCache = null;

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


        internal GroupHelper ModifyByIndex(int groupIndex, GroupData newGroupData)
        {
            appmanager.Naviator.GoToGroupsPage();
            SelectGroupByIndex(groupIndex);
            InitNewGroupModification();
            FillGroupForm(newGroupData);
            SubmitGroupModification();
            ReturnToGroupsPage();

            return this;
        }


        internal GroupHelper ModifyById(string groupId, GroupData newGroupData)
        {
            appmanager.Naviator.GoToGroupsPage();
            SelectGroupById(groupId);
            InitNewGroupModification();
            FillGroupForm(newGroupData);
            SubmitGroupModification();
            ReturnToGroupsPage();

            return this;
        }


        public GroupHelper DeleteById(string groupId)
        {
            appmanager.Naviator.GoToGroupsPage();
            SelectGroupById(groupId);
            RemoveGroup();
            ReturnToGroupsPage();

            return this;
        }


        public GroupHelper DeleteByIndex(int groupIndex)
        {
            appmanager.Naviator.GoToGroupsPage();
            SelectGroupByIndex(groupIndex);
            RemoveGroup();
            ReturnToGroupsPage();

            return this;
        }

        public List<GroupData> GetGroupList()
        {
            if (groupCache == null)
            {
                groupCache = new List<GroupData>();
                appmanager.Naviator.GoToGroupsPage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group"));

                foreach (IWebElement element in elements)
                {
                    GroupData group = new GroupData(element.Text)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    };

                    groupCache.Add(group);
                }
            }

            return new List<GroupData>(groupCache);
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
            // Reset cache
            groupCache = null;

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
            // Reset cache
            groupCache = null;

            return this;
        }

        public GroupHelper ReturnToGroupsPage()
        {
            driver.FindElement(By.LinkText("group page")).Click();
            return this;
        }

        public GroupHelper SelectGroupByIndex(int index)
        {
            index += 1;
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
            return this;
        }

        public GroupHelper SelectGroupById(string id)
        {
            driver.FindElement(By.XPath("//input[@name='selected[]'][@value='" + id + "']")).Click();
            return this;
        }

        public GroupHelper RemoveGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            // Reset cache
            groupCache = null;

            return this;
        }

        public int Count()
        {
            appmanager.Naviator.GoToGroupsPage();
            return driver.FindElements(By.ClassName("group")).Count;
        }
    }
}
