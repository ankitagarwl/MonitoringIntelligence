using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringIntelligence.StateClass
{
    public class SOAmodel
    {
        public int approved_bugs { get; set; }
        public int committed_bugs { get; set; }
        public int done_bugs { get; set; }
        public int new_bugs { get; set; }

        public int done_tasks { get; set; }
        public int in_progress_tasks { get; set; }
        public int removed_tasks { get; set; }
        public int to_do_tasks { get; set; }
        
    }
}
