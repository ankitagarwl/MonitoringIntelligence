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

namespace MonitoringIntelligence.BAL
{
    public class SOABAL
    {
        #region Count
        /// <summary>
        /// Gives total number of bugs and task
        /// </summary>
        /// <returns>int</returns>
        public SOAmodel GetCount()
        {
            SOAmodel soamodel = new SOAmodel();
            Uri uri = new Uri(ConfigurationManager.AppSettings["uri"]);
            string personalAccessToken = ConfigurationManager.AppSettings["personalAccessToken"];
            string project = ConfigurationManager.AppSettings["project"];

            VssBasicCredential credentials = new VssBasicCredential("", ConfigurationManager.AppSettings["personalAccessToken"]);

            #region Bug_count
            //create a wiql object and build our query
            Wiql wiql = new Wiql()
            {

                Query = "Select [State], [Title] " +
                        "From WorkItems " +
                        " Where [Work Item Type] = 'Bug' " +
                        " And [System.TeamProject] = '" + project + "' " +
                        // " And [System.State] <> 'Closed' And [System.State] <> 'Done' " +
                        " Order By [State] Asc, [Changed Date] Desc"
            };

            //create instance of work item tracking http client
            using (WorkItemTrackingHttpClient workItemTrackingHttpClient = new WorkItemTrackingHttpClient(uri, credentials))
            {
                //execute the query to get the list of work items in the results
                WorkItemQueryResult workItemQueryResult = workItemTrackingHttpClient.QueryByWiqlAsync(wiql).Result;
                soamodel.Bugs =  workItemQueryResult.WorkItems.Count();
            }
            #endregion

            #region Task_count
            //create a wiql object and build our query
            Wiql wiql_task = new Wiql()
            {

                Query = "Select [State], [Title] " +
                        "From WorkItems " +
                        " Where [Work Item Type] = 'Task' " +
                        " And [System.TeamProject] = '" + project + "' " +
                        // " And [System.State] <> 'Closed' And [System.State] <> 'Done' " +
                        " Order By [State] Asc, [Changed Date] Desc"
            };

            //create instance of work item tracking http client
            using (WorkItemTrackingHttpClient workItemTrackingHttpClient = new WorkItemTrackingHttpClient(uri, credentials))
            {
                //execute the query to get the list of work items in the results
                WorkItemQueryResult workItemQueryResult = workItemTrackingHttpClient.QueryByWiqlAsync(wiql_task).Result;
                soamodel.Tasks = workItemQueryResult.WorkItems.Count();
            }
            #endregion
            return soamodel;
        }
        #endregion


    }
}
