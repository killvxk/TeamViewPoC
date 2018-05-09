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

        public async Task<IEnumerable<WorkItem>> GetMyRecentlyCompletedAsync()
        {
            return await _context.WorkItems.Where(x => x.Complete == true && x.LastUpdated.AddDays(30) >= DateTime.Now).ToArrayAsync();
        }

        public async Task<IEnumerable<WorkItem>> GetMyWorkItemsAsync(string sort)
        {
            if (sort == "due")
            {
                return await _context.WorkItems.Where(x => x.AssignedTo == Constants.HardCodedSignedInUser && x.Complete == false).OrderBy(x => x.DueDate).Include(x => x.Notes).ToArrayAsync();
            }

            return await _context.WorkItems.Where(x => x.AssignedTo == Constants.HardCodedSignedInUser && x.Complete == false).Include(x => x.Notes).ToArrayAsync();
        }

        public async Task<WorkItem> GetWorkItemByIdAsync(int id)
        {
            return await _context.WorkItems.Include(x => x.Notes).FirstOrDefaultAsync(x => x.WorkItemId == id);
        }

        public async Task MarkComplete(int id)
        {
            var update =  await _context.WorkItems.FirstOrDefaultAsync(x => x.WorkItemId == id);
            update.Complete = true;
            update.LastUpdated = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async void UpdateWorkItemAsync(WorkItem item)
        {
            _context.WorkItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(WorkItem item)
        {
            _context.WorkItems.Update(item);
            item.LastUpdated = DateTime.Now;

            await _context.SaveChangesAsync();
        }
    }
}
