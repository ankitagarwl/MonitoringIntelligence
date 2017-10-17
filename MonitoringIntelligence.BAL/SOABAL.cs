using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonitoringIntelligence.StateClass;
using MonitoringIntelligence.DAL;
using System.Net.Http;
using System.Data;
using System.Configuration;
using SupportBeacon.DAL;
using System.Net.Mail;
using System.IO;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using System.Net.Http.Headers;

namespace MonitoringIntelligence.BAL
{
    public class SOABAL
    {
        // Bug -  Approved,Committed,Done,New   // task - Done, In Progress,Removed,To Do
        #region Count
        /// <summary>
        /// Gives total number of bugs and task
        /// </summary>
        /// <returns>int</returns>
        public SOAmodel GetCount(string projectname)
        {
            SOAmodel soamodel = new SOAmodel();
            Uri uri = new Uri(ConfigurationManager.AppSettings["uri"]);
            string personalAccessToken = ConfigurationManager.AppSettings["personalAccessToken"];
           // string project = ConfigurationManager.AppSettings["project"];

            VssBasicCredential credentials = new VssBasicCredential("", ConfigurationManager.AppSettings["personalAccessToken"]);
            // Bug -  Approved,Committed,Done,New   // task - Done, In Progress,Removed,To Do
            #region Bugs
            #region Approved_Bug_count
            //create a wiql object and build our query
            Wiql wiql = new Wiql()
            {

                Query = "Select [State], [Title] " +
                        "From WorkItems " +
                        " Where [Work Item Type] = 'Bug' " +
                        " And [System.TeamProject] = '" + projectname + "' " +
                        " And [System.State] = 'Approved'  " +
                        " Order By [State] Asc, [Changed Date] Desc"
            };

            //create instance of work item tracking http client
            using (WorkItemTrackingHttpClient workItemTrackingHttpClient = new WorkItemTrackingHttpClient(uri, credentials))
            {
                //execute the query to get the list of work items in the results
                WorkItemQueryResult workItemQueryResult = workItemTrackingHttpClient.QueryByWiqlAsync(wiql).Result;
                soamodel.approved_bugs =  workItemQueryResult.WorkItems.Count();
            }
            #endregion

            #region Committed_Bug_count
            //create a wiql object and build our query
            Wiql wiql_commited = new Wiql()
            {

                Query = "Select [State], [Title] " +
                        "From WorkItems " +
                        " Where [Work Item Type] = 'Bug' " +
                        " And [System.TeamProject] = '" + projectname + "' " +
                        " And [System.State] = 'Committed'  " +
                        " Order By [State] Asc, [Changed Date] Desc"
            };

            //create instance of work item tracking http client
            using (WorkItemTrackingHttpClient workItemTrackingHttpClient = new WorkItemTrackingHttpClient(uri, credentials))
            {
                //execute the query to get the list of work items in the results
                WorkItemQueryResult workItemQueryResult = workItemTrackingHttpClient.QueryByWiqlAsync(wiql_commited).Result;
                soamodel.committed_bugs = workItemQueryResult.WorkItems.Count();
            }
            #endregion

            #region Done_Bug_count
            //create a wiql object and build our query
            Wiql wiql_done = new Wiql()
            {

                Query = "Select [State], [Title] " +
                        "From WorkItems " +
                        " Where [Work Item Type] = 'Bug' " +
                        " And [System.TeamProject] = '" + projectname + "' " +
                        " And [System.State] = 'Done'  " +
                        " Order By [State] Asc, [Changed Date] Desc"
            };

            //create instance of work item tracking http client
            using (WorkItemTrackingHttpClient workItemTrackingHttpClient = new WorkItemTrackingHttpClient(uri, credentials))
            {
                //execute the query to get the list of work items in the results
                WorkItemQueryResult workItemQueryResult = workItemTrackingHttpClient.QueryByWiqlAsync(wiql_done).Result;
                soamodel.done_bugs = workItemQueryResult.WorkItems.Count();
            }
            #endregion

            #region Approved_Bug_count
            //create a wiql object and build our query
            Wiql wiql_new = new Wiql()
            {

                Query = "Select [State], [Title] " +
                        "From WorkItems " +
                        " Where [Work Item Type] = 'Bug' " +
                        " And [System.TeamProject] = '" + projectname + "' " +
                        " And [System.State] = 'New'  " +
                        " Order By [State] Asc, [Changed Date] Desc"
            };

            //create instance of work item tracking http client
            using (WorkItemTrackingHttpClient workItemTrackingHttpClient = new WorkItemTrackingHttpClient(uri, credentials))
            {
                //execute the query to get the list of work items in the results
                WorkItemQueryResult workItemQueryResult = workItemTrackingHttpClient.QueryByWiqlAsync(wiql_new).Result;
                soamodel.new_bugs = workItemQueryResult.WorkItems.Count();
            }
            #endregion

            #endregion

            #region Task

                #region Done_Task_count
                //create a wiql object and build our query
                Wiql wiql_task_done = new Wiql()
                {

                    Query = "Select [State], [Title] " +
                            "From WorkItems " +
                            " Where [Work Item Type] = 'Task' " +
                            " And [System.TeamProject] = '" + projectname + "' " +
                            " And [System.State] = 'Done'" +
                            " Order By [State] Asc, [Changed Date] Desc"
                };

                //create instance of work item tracking http client
                using (WorkItemTrackingHttpClient workItemTrackingHttpClient = new WorkItemTrackingHttpClient(uri, credentials))
                {
                    //execute the query to get the list of work items in the results
                    WorkItemQueryResult workItemQueryResult = workItemTrackingHttpClient.QueryByWiqlAsync(wiql_task_done).Result;
                    soamodel.done_tasks = workItemQueryResult.WorkItems.Count();
                }
            #endregion

                #region In_Progress_Task_count
            //create a wiql object and build our query
            Wiql wiql_task_inprogress = new Wiql()
            {

                Query = "Select [State], [Title] " +
                        "From WorkItems " +
                        " Where [Work Item Type] = 'Task' " +
                        " And [System.TeamProject] = '" + projectname + "' " +
                        " And [System.State] = 'In Progress'" +
                        " Order By [State] Asc, [Changed Date] Desc"
            };

            //create instance of work item tracking http client
            using (WorkItemTrackingHttpClient workItemTrackingHttpClient = new WorkItemTrackingHttpClient(uri, credentials))
            {
                //execute the query to get the list of work items in the results
                WorkItemQueryResult workItemQueryResult = workItemTrackingHttpClient.QueryByWiqlAsync(wiql_task_inprogress).Result;
                soamodel.in_progress_tasks = workItemQueryResult.WorkItems.Count();
            }
            #endregion

                #region Removed_Task_count
            //create a wiql object and build our query
            Wiql wiql_task_removed = new Wiql()
            {

                Query = "Select [State], [Title] " +
                        "From WorkItems " +
                        " Where [Work Item Type] = 'Task' " +
                        " And [System.TeamProject] = '" + projectname + "' " +
                        " And [System.State] = 'Removed'" +
                        " Order By [State] Asc, [Changed Date] Desc"
            };

            //create instance of work item tracking http client
            using (WorkItemTrackingHttpClient workItemTrackingHttpClient = new WorkItemTrackingHttpClient(uri, credentials))
            {
                //execute the query to get the list of work items in the results
                WorkItemQueryResult workItemQueryResult = workItemTrackingHttpClient.QueryByWiqlAsync(wiql_task_removed).Result;
                soamodel.removed_tasks = workItemQueryResult.WorkItems.Count();
            }
            #endregion

                #region To_Do_Task_count
                //create a wiql object and build our query
                Wiql wiql_task_ToDo = new Wiql()
                {

                    Query = "Select [State], [Title] " +
                            "From WorkItems " +
                            " Where [Work Item Type] = 'Task' " +
                            " And [System.TeamProject] = '" + projectname + "' " +
                            " And [System.State] = 'To Do'" +
                            " Order By [State] Asc, [Changed Date] Desc"
                };

                //create instance of work item tracking http client
                using (WorkItemTrackingHttpClient workItemTrackingHttpClient = new WorkItemTrackingHttpClient(uri, credentials))
                {
                    //execute the query to get the list of work items in the results
                    WorkItemQueryResult workItemQueryResult = workItemTrackingHttpClient.QueryByWiqlAsync(wiql_task_ToDo).Result;
                    soamodel.to_do_tasks = workItemQueryResult.WorkItems.Count();
                }
                #endregion

            #endregion
            return soamodel;
        }
        #endregion

       

    }
}
