using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamViewPoC.Data;

namespace TeamViewPoC.Models
{
    public class NoteDataService : INoteDataService
    {
        private readonly ApplicationDbContext _context;
        //
        public NoteDataService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddNoteAsync(Note note)
        {
            await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();
        }
    }
}
