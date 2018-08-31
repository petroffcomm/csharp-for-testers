using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace MantisTests
{
    public class ProjectManagementTests : AuthTestBase
    {
        [Test]
        public void CreateProject()
        {
            string postfix = GetRandomAllowedStringFor(GENERAL, 10);
            ProjectData project = new ProjectData("projectName_" + postfix, "project description " + postfix);

            List<ProjectData> oldProjects = app.API.GetProjectList();

            app.ProjectsAdministration.AddNewProject(project);

            List<ProjectData> newProjects = app.API.GetProjectList();

            oldProjects.Add(project);
            oldProjects.Sort();
            newProjects.Sort();

            Assert.AreEqual(oldProjects, newProjects);
        }


        [Test]
        public void DeleteProject()
        {
            CreateContactForTestIfNecessary();

            List<ProjectData> oldProjects = app.API.GetProjectList();
            ProjectData project = oldProjects[0];

            app.ProjectsAdministration.RemoveProject(project);

            List<ProjectData> newProjects = app.API.GetProjectList();

            oldProjects.Remove(project);
            oldProjects.Sort();
            newProjects.Sort();

            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}
