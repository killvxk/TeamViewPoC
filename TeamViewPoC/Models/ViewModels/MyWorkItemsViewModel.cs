using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamViewPoC.Models.ViewModels
{
    public class MyWorkItemsViewModel
    {
        public IEnumerable<WorkItem> WorkItems { get; set; }
        public WorkItem WorkItem { get; set; }

    }
}
