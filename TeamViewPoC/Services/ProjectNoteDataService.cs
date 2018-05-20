using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamViewPoC.Data;
using TeamViewPoC.Models;


namespace TeamViewPoC.Services
{
    public class ProjectNoteDataService : IProjectNoteDataService
    {
        private readonly ApplicationDbContext _context;

        public ProjectNoteDataService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddProjectNoteAsync(ProjectNote note)
        {
            await _context.ProjectNotes.AddAsync(note);
            await _context.SaveChangesAsync();
        }
    }
}
