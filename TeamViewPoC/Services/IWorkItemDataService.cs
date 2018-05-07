using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamViewPoC.Models;

namespace TeamViewPoC.Services
{
    public interface IWorkItemDataService
    {
        Task AddWorkItemAsync(WorkItem item);
        Task<WorkItem> GetWorkItemByIdAsync(int id);
        void UpdateWorkItemAsync(WorkItem item);
        Task<IEnumerable<WorkItem>> GetActiveWorkItemsAsync();
        Task<IEnumerable<WorkItem>> GetMyWorkItemsAsync(string sort);
        Task MarkComplete(int id);
    }
}
