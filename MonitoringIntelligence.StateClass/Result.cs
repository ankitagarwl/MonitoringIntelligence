using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringIntelligence.StateClass
{
   public class Result
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public dynamic Results { get; set; }
    }
    public class Result_new
    {
        public dynamic Results { get; set; }
    }


    public class Result_location
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string address { get; set; }
        public string City { get; set; }
        public string state { get; set; }
    }



}
