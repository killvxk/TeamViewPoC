using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamViewPoC.Data;
using TeamViewPoC.Models;

namespace TeamViewPoC.Services
{
    public class ProjectDataService : IProjectDataService
    {
        private readonly ApplicationDbContext _context;
        public ProjectDataService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddProject(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();   
        }

        public async Task<Project> GetProjectById(int? id)
        {
            return await _context.Projects.Include(x=>x.WorkItems).FirstOrDefaultAsync(x => x.ProjectId == id);
        }

        public async Task<Project> GetProjectById(int? id, string option)
        {
            return await _context.Projects.Include(x => x.WorkItems).Include(x=>x.ProjectNotes).FirstOrDefaultAsync(x => x.ProjectId == id);
        }
    }
}
