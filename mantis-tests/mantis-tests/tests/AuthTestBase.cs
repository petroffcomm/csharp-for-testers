using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;


namespace MantisTests
{
    public class AuthTestBase : TestBase
    {
        public void CreateContactForTestIfNecessary()
        {
            if (app.ProjectsAdministration.GetList().Count() == 0)
            {
                ProjectData project = new ProjectData();
                project.Name = "testProjectName";
                project.Description = "test description";

                app.ProjectsAdministration.AddNewProject(project);
            }
        }


        [SetUp]
        public void DoLogin()
        {
            // This part of code was moved here from TestBase inheritants because
            // all our tests require these actions at the same beginning.
            app.Auth.Login(new AccountData("administrator", "root"));
        }
    }
}
