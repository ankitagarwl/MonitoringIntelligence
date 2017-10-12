using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringIntelligence.StateClass
{
    public class LoginResponse
    {
       
        public string f_name { get; set; }
        public string l_name { get; set; }
        public string token { get; set; }
        public int expirydate { get; set; }
        public string authorization { get; set; }
       
    }
}
