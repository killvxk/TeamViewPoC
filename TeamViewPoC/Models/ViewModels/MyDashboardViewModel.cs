using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamViewPoC.Models.ViewModels
{
    public class MyDashboardViewModel
    {
        public IEnumerable<WorkItem> MyWorkItems { get; set; }
        public IEnumerable<Project> MyProjects { get; set; }


    }
}
