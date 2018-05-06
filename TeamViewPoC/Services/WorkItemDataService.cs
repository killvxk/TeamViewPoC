using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamViewPoC.Data;
using TeamViewPoC.Models;

namespace TeamViewPoC.Services
{
    public class WorkItemDataService : IWorkItemDataService
    {
        private readonly ApplicationDbContext _context;

        //injecting the db into the constructor and setting it to the private field
        public WorkItemDataService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task AddWorkItemAsync(WorkItem item)
        {
            await _context.WorkItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WorkItem>> GetActiveWorkItemsAsync()
        {
            return await _context.WorkItems.Where(x=>x.Complete == false).ToArrayAsync();
        }

        public async Task<IEnumerable<WorkItem>> GetMyWorkItemsAsync(string sort)
        {
            if (sort == "due")
            {
                return await _context.WorkItems.Where(x => x.AssignedTo == Constants.HardCodedSignedInUser).OrderBy(x => x.DueDate).ToArrayAsync();
            }

            return await _context.WorkItems.Where(x => x.AssignedTo == Constants.HardCodedSignedInUser).ToArrayAsync();
        }

        public async Task<WorkItem> GetWorkItemByIdAsync(int id)
        {
            return await _context.WorkItems.FindAsync(id);
        }

        public async void UpdateWorkItemAsync(WorkItem item)
        {
            _context.WorkItems.Update(item);
            await _context.SaveChangesAsync();
        }

    }
}
