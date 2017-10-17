using System.Web.Http;
using MonitoringIntelligence.StateClass;
using System.Net.Http;
using MonitoringIntelligence.BAL;
using System;
using System.Net;
using System.Collections.Generic;
using System.Web.Http.Cors;

using System.Data;
using System.Configuration;
using System.Net.Mail;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Net.Http.Headers;

namespace MonitoringIntelligence.Controllers
{

    /// <summary>
    /// All APIs 
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
    public class SOAController : ApiController
    {
        #region TESTAPI
        /// <summary>
        /// TestAPI
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public HttpResponseMessage TestAPI()
        {

            HttpResponseMessage response = new HttpResponseMessage();
            response = Request.CreateResponse(HttpStatusCode.OK, "Success");
            return response;
        }
        #endregion
        
        #region All Accounts
        /// <summary>
        /// Get All accounts of member
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> GetAccounts()
        {
            Result objResult = new Result();
            try
            {
                var personalaccesstoken = ConfigurationManager.AppSettings["personalAccessToken"];

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(
                                                                                                string.Format("{0}:{1}", "", personalaccesstoken))));

                    using (HttpResponseMessage response = client.GetAsync("https://app.vssps.visualstudio.com/_apis/Accounts?memberId=b7f6d37d-2a31-471a-92f2-a4483017e8b3&api-version=3.2-preview").Result)
                    {
                        response.EnsureSuccessStatusCode();
                        string resp = await response.Content.ReadAsStringAsync();
                        Account_Model returnedValue = JsonConvert.DeserializeObject<Account_Model>(resp);
                        objResult.Results = returnedValue;
                        objResult.Message = "true";
                        objResult.Status = "true";
                        return Request.CreateResponse(HttpStatusCode.OK, objResult);
                    }
                }

            }
            catch (Exception ex)
            {
                objResult.Results = null;
                objResult.Message = "Something went wrong";
                objResult.Status = "false";
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResult);
            }


        }
        #endregion

        #region All Projects
        /// <summary>
        /// Get All Projects of a account
        /// </summary>
        /// <param name="vsts_account"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> GetProjects([FromBody]vsts_account vsts_account)
        {
            
            Result objResult = new Result();
            try
            {
                var personalaccesstoken = ConfigurationManager.AppSettings["personalAccessToken"];

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(
                                                                                                string.Format("{0}:{1}", "", personalaccesstoken))));

                    using (HttpResponseMessage response = client.GetAsync("https://" + vsts_account.accname.Trim() + ".visualstudio.com/DefaultCollection/_apis/projects?api-version=1.0").Result)
                    {
                        response.EnsureSuccessStatusCode();
                        string resp = await response.Content.ReadAsStringAsync();
                        Projects_model returnedValue = JsonConvert.DeserializeObject<Projects_model>(resp);
                        objResult.Results = returnedValue;
                        objResult.Message = "true";
                        objResult.Status = "true";
                        return Request.CreateResponse(HttpStatusCode.OK, objResult);
                    }
                }

            }
            catch (Exception ex)
            {
                objResult.Results = null;
                objResult.Message = "Something went wrong";
                objResult.Status = "false";
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResult);
            }


        }
        #endregion
        
        #region GetCount
        /// <summary>
        /// Get Count of Bugs and Task
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetCount([FromBody]vsts_project vsts_project)
        {
            SOAmodel soamodel = new SOAmodel();
            HttpResponseMessage response = new HttpResponseMessage(); ;
            Result objResult = new Result();
            SOABAL soabal = new SOABAL();
            HttpClient client = new HttpClient();
            HttpResponseMessage response1 = new HttpResponseMessage();
            string str_guid = Guid.NewGuid().ToString();
            try
            {

                soamodel = soabal.GetCount(vsts_project.vsts_projectname);

                if (soamodel != null)
                {
                    objResult.Results = soamodel;
                    objResult.Message = "true";
                    objResult.Status = "true";
                    response = Request.CreateResponse(HttpStatusCode.OK, objResult);
                }
                else
                {
                    objResult.Results = null;
                    objResult.Message = "false";
                    objResult.Status = "false";
                    response = Request.CreateResponse(HttpStatusCode.NotFound, "Data Empty!");
                }
                return response;
            }
            catch (Exception ex)
            {

                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
        }
        #endregion






    }
}
