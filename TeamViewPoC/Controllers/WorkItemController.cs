using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamViewPoC.Models;
using TeamViewPoC.Services;
using TeamViewPoC.Models.ViewModels;

namespace TeamViewPoC.Controllers
{
    public class WorkItemController : Controller
    {
        private readonly IWorkItemDataService _workItemDataService;
        
        //DI the workitem data service
        public WorkItemController(IWorkItemDataService workItemDataService)
        {
            _workItemDataService = workItemDataService;
        }
        //GET Index
        //This loads the my work items as a summary
        public async Task<IActionResult> Index()
        {
            var data = await _workItemDataService.GetMyWorkItemsAsync("due");
            return View(data);
        }

        //GET Create
        public IActionResult Create()
        {
            return View();
        }

        //GET List
        public async Task<IActionResult> List()
        {
            var data = await _workItemDataService.GetActiveWorkItemsAsync();
            return View(data);
        }

        //GET ItemDetail
        public async Task<IActionResult> ItemDetail(int id)
        {
            var model = new WorkItemDetailViewModel
            {
                WorkItem = await _workItemDataService.GetWorkItemByIdAsync(id)

            };
            return View(model);
        }

        //GET MarkComplete
        public async Task<IActionResult> MarkComplete(int id)
        {
            await _workItemDataService.MarkComplete(id);
            return RedirectToAction("Index");
        }
        
        //GET RecentlyCompleted
        public async Task<IActionResult> RecentlyCompleted()
        {
            var data = await _workItemDataService.GetMyRecentlyCompletedAsync();
            return View(data);
        }

        //GET Edit Workitem
        public async Task<IActionResult>Edit(int id)
        {
            var data = await _workItemDataService.GetWorkItemByIdAsync(id);
            return View(data);
        }
        //POST Create
        [HttpPost]
        public async Task<IActionResult> Create(WorkItem model)
        {
            model.CreatedOn = DateTime.Now;
            model.LastUpdated = DateTime.Now;
            //make the due time equal to CoB on the date (1700 hrs)
            model.DueDate = model.DueDate.AddHours(17);
            model.Complete = false;
            model.Active = true;
            model.CreatedBy = Constants.HardCodedSignedInUser;
            model.Status = Constants.OnTrack;
            await _workItemDataService.AddWorkItemAsync(model);
            ModelState.Clear();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult>Edit(WorkItem model)
        {
            await _workItemDataService.Update(model);
            return RedirectToAction("Index");
        }

    }
}