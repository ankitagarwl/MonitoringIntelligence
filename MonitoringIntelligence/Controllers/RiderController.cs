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

        #region All Rider List
        /// <summary>
        /// Get Count of Bugs and Task
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetCount()
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

                soamodel = soabal.GetCount();
            
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
