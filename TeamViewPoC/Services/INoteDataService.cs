using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamViewPoC.Data;

namespace TeamViewPoC.Models
{
    public interface INoteDataService
    {
        /// <summary>
        /// Adds the note asynchronous.
        /// </summary>
        /// <param name="note">The note object</param>
        /// <returns></returns>
        Task AddNoteAsync(Note note);
    }
}
