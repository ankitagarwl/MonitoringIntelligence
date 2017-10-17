using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringIntelligence.StateClass
{
    public class Account_Model
    {
        public int count { get; set; }
        public List<Value> value { get; set; }
    }

    public class Properties
    {
    }

    public class Value
    {
        public string accountId { get; set; }
        public string accountUri { get; set; }
        public string accountName { get; set; }
        public Properties properties { get; set; }
    }


}
