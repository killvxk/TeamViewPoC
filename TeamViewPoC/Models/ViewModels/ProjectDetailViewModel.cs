using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamViewPoC.Models.ViewModels
{
    public class ProjectDetailViewModel
    {
        public Project Project { get; set; }
        public Note Note { get; set; }
        public WorkItem WorkItem { get; set; }
        public ProjectNote ProjectNote { get; set; }

        public IEnumerable<ProjectNote> ProjectNotes { get; set; }

    }
}
