using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamViewPoC.Models.ViewModels
{
    public class TodayViewModel
    {
        public IEnumerable<WorkItem> WorkItemsSoon{ get; set; }
        public IEnumerable<WorkItem> WorkItemsToday { get; set; }


    }
}
