using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamViewPoC.Models
{
    public class ProjectNote
    {
        public int ProjectNoteId { get; set; }
        public string NoteContent { get; set; }
        public DateTime CreatedOn { get; set; }
        public Project Project { get; set; }
        public string CreatedBy { get; set; }
    }
}
