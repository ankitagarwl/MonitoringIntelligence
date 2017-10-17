using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringIntelligence.StateClass
{
    public class Projects_model
    {
        public int count { get; set; }
        public List<AllProjects> value { get; set; }
    }
    public class AllProjects
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string state { get; set; }
        public int revision { get; set; }
        public string visibility { get; set; }
    }





    }
