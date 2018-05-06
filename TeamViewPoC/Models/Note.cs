using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamViewPoC.Models
{
    public class Note
    {
        public int NoteId { get; set; }
        public string NoteContent { get; set; }
        public DateTime CreatedOn { get; set; }
        public WorkItem WorkItem { get; set; }
        public string CreatedBy { get; set; }


    }
}
