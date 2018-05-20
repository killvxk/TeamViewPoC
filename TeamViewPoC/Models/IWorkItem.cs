using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamViewPoC.Models
{
    public interface IWorkItem
    {
         
         string Title { get; set; }
         string Description { get; set; }
         DateTime CreatedOn { get; set; }
         DateTime DueDate { get; set; }
         DateTime LastUpdated { get; set; }
         bool Active { get; set; }
         bool Complete { get; set; }
         string AssignedTo { get; set; }
         string CreatedBy { get; set; }
         string Status { get; set; }
         

    }
}
