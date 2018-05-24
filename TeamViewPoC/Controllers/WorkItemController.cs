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
        private readonly IProjectDataService _projectDataService;
        private readonly IProjectNoteDataService _projectNoteDataService;
        
        //DI the workitem data service
        public WorkItemController(IWorkItemDataService workItemDataService, IProjectDataService projectDataService, IProjectNoteDataService projectNoteDataService)
        {
            _workItemDataService = workItemDataService;
            _projectDataService = projectDataService;
            _projectNoteDataService = projectNoteDataService;
        }
        
        //GET Index
        //This loads the my work items as a summary
        public async Task<IActionResult> Index()
        {
            var data = new MyWorkItemsViewModel
                {
                WorkItems = await _workItemDataService.GetMyWorkItemsAsync("due"),
                WorkItem = new WorkItem()
                };
            

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
        
        //GET Today
        public async Task<IActionResult> Today()
        {
            var data = new TodayViewModel
            {
                WorkItemsSoon = await _workItemDataService.GetMyDueSoon(2),
                WorkItemsToday = await _workItemDataService.GetMyDueToday()
            };
            return View(data);
        }

        //GET search results
        public async Task<IActionResult> SearchResult(string searchstring)
        {
            var data = await _workItemDataService.SearchMyWorkItems(searchstring);
            ViewBag.SearchTerm = searchstring;
            return View(data);
        }
        
        //POST Create
        [HttpPost]
        public async Task<IActionResult> Create(WorkItem model, int projectid)
        {
            
            model.Project = await _projectDataService.GetProjectById(projectid);
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
        public async Task<IActionResult> CreateFromVM(MyWorkItemsViewModel model)
        {
            model.WorkItem.CreatedOn = DateTime.Now;
            model.WorkItem.LastUpdated = DateTime.Now;
            //make the due time equal to CoB on the date (1700 hrs)
            model.WorkItem.DueDate = model.WorkItem.DueDate.AddHours(17);
            model.WorkItem.Complete = false;
            model.WorkItem.Active = true;
            model.WorkItem.CreatedBy = Constants.HardCodedSignedInUser;
            model.WorkItem.Status = Constants.OnTrack;
            await _workItemDataService.AddWorkItemAsync(model.WorkItem);
            ModelState.Clear();

            return RedirectToAction("Index");
        }

        //POST Edit
        [HttpPost]
        public async Task<IActionResult>Edit(WorkItem model)
        {
            await _workItemDataService.Update(model);
            return RedirectToAction("Index");
        }

        //Transform
        [HttpGet]
        public async Task<IActionResult>Transform(int id)
        {
            WorkItem item = await _workItemDataService.GetWorkItemByIdAsync(id);

            Project project = new Project
            {
                Title = item.Title,
                AssignedTo = item.AssignedTo,
                CreatedBy = item.CreatedBy,
                CreatedOn = item.CreatedOn,
                Description = item.Description,
                DueDate = item.DueDate,
                LastUpdated = item.LastUpdated,
                Active = true

            };
            //add the project
            await _projectDataService.AddProject(project);

            //move notes to the new project
            foreach (var note in item.Notes)
            {
                ProjectNote projectNote = new ProjectNote
                {
                    CreatedBy = note.CreatedBy,
                    CreatedOn = note.CreatedOn,
                    NoteContent = note.NoteContent,
                    Project = project
                };
                //add the project note
                await _projectNoteDataService.AddProjectNoteAsync(projectNote);

            }

            //deactivate the old work item
            await _workItemDataService.SetInactive(item);
            //go to the project detail page
            return RedirectToAction("Details", "Projects", new { id = project.ProjectId });
        }
    }
}