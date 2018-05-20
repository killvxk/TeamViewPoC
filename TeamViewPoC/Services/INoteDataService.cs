using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamViewPoC.Data;

namespace TeamViewPoC.Models
{
    public interface INoteDataService
    {
        Task AddNoteAsync(Note note);
    }
}
