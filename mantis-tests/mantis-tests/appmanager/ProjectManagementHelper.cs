using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace MantisTests
{
    public class ProjectManagementHelper : HelperBase
    {
        List<ProjectData> projectsCache = null;

        public ProjectManagementHelper(ApplicationManager appmanager)
            : base(appmanager)
        {
        }

        public List<ProjectData> GetList()
        {
            if (projectsCache == null)
            {
                projectsCache = new List<ProjectData>();

                appmanager.AdminNavigator.GoToProjectsPage();
                // Need to be optimized by using XPath
                ICollection<IWebElement> elements = driver.FindElement(By.CssSelector("div.table-responsive"))
                                                          .FindElement(By.TagName("tbody"))
                                                          .FindElements(By.TagName("tr"));

                foreach (IWebElement element in elements)
                {
                    IList<IWebElement> cells = element.FindElements(By.TagName("td"));


                    ProjectData project = new ProjectData()
                    {
                        Name = cells[0].FindElement(By.TagName("a")).Text,
                        //EditLink = cells[0].FindElement(By.TagName("a")).GetProperty("href"),
                        Status = cells[1].Text,
                        ViewStatus = cells[3].Text,
                        Description = cells[4].Text
                    };

                    projectsCache.Add(project);
                }
            }

            return projectsCache;
        }


        public ProjectManagementHelper AddNewProject(ProjectData project)
        {
            appmanager.AdminNavigator.GoToProjectsPage();
            InitProjectCreation();
            FillProjectForm(project);
            SubmitProjectCreation();
            appmanager.AdminNavigator.GoToProjectsPage();

            return this;
        }


        public void RemoveProject(ProjectData project)
        {
            appmanager.AdminNavigator.GoToProjectsPage();
            InitProjectModification(project);
            InitProjectRemoval();
            SubmitProjectRemoval();
        }


        private void InitProjectCreation()
        {
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }


        private void InitProjectModification(ProjectData project)
        {
            driver.FindElement(By.XPath("//a[text()='" + project.Name + "']")).Click();
        }


        private void InitProjectRemoval()
        {
            driver.FindElement(By.XPath("//input[contains(@value, 'Удалить')]")).Click();
        }


        private void SubmitProjectRemoval()
        {
            // Button has the same path as the first project removal button
            driver.FindElement(By.XPath("//input[contains(@value, 'Удалить')]")).Click();

            projectsCache = null;
        }


        private void FillProjectForm(ProjectData project)
        {
            Type(By.Name("name"), project.Name);
            Type(By.Name("description"), project.Description);
        }


        private void SubmitProjectCreation()
        {
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();

            projectsCache = null;
        }
    }
}