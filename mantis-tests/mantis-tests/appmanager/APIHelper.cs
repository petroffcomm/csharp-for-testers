using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantisTests
{
    public class APIHelper : HelperBase
    {
        Mantis.MantisConnectPortTypeClient SOAPClient;

        public APIHelper(ApplicationManager appmanager) : base(appmanager)
        {
            SOAPClient = new Mantis.MantisConnectPortTypeClient();
        }


        public void CreateNewIssue(AccountData account, ProjectData projectData, Mantis.IssueData issue)
        {
            
            issue.project = new Mantis.ObjectRef();
            issue.project.id = projectData.Id;

            SOAPClient.mc_issue_add(account.Username, account.Password, issue);
        }


        public List<ProjectData> GetProjectList()
        {
            List<ProjectData> projects = new List<ProjectData>();
            
            Mantis.ProjectData[] mc_projects = SOAPClient.mc_projects_get_user_accessible(appmanager.Auth.CurrAccount.Username,
                                                                                          appmanager.Auth.CurrAccount.Password);

            foreach (Mantis.ProjectData mc_project in mc_projects)
            {
                ProjectData project = new ProjectData()
                {
                    Name = mc_project.name,
                    Status = mc_project.status.name,
                    ViewStatus = mc_project.view_state.name,
                    Description = mc_project.description
                };

                projects.Add(project);
            }

            return projects;
        }
    }
}
