using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamViewPoC.Models;
using TeamViewPoC.Models.ViewModels;
using TeamViewPoC.Services;

namespace TeamViewPoC.Controllers
{
    public class NoteController : Controller
    {
        private readonly INoteDataService _noteDataService;
        private readonly IWorkItemDataService _workItemDataService;
        private readonly IProjectDataService _projectDataService;
        public NoteController(INoteDataService noteDataService, IWorkItemDataService workItemDataService, IProjectDataService projectDataService)
        {
            _noteDataService = noteDataService;
            _workItemDataService = workItemDataService;
            _projectDataService = projectDataService;
        }
        public IActionResult Index()
        {
            return View();
        }

        //POST Add
        [HttpPost]
        public async Task<IActionResult> Add(WorkItemDetailViewModel model, int itemId)
        {
            WorkItem workItem = new WorkItem();
            workItem = await _workItemDataService.GetWorkItemByIdAsync(itemId);

            Note note = new Note
            {
                NoteContent = model.Note.NoteContent,
                CreatedOn = DateTime.Now,
                CreatedBy = Constants.HardCodedSignedInUser,
                WorkItem = workItem
            };

            await _noteDataService.AddNoteAsync(note);
            
            return RedirectToAction("ItemDetail", "Workitem", new { id = itemId });
        }

        //POST ProjectAdd
        [HttpPost]
        public async Task<IActionResult>ProjectNote(ProjectDetailViewModel model, int projectid)
        {
            var project = await _projectDataService.GetProjectById(projectid);

            Note note = new Note
            {
                NoteContent = model.Note.NoteContent,
                CreatedOn = DateTime.Now,
                CreatedBy = Constants.HardCodedSignedInUser,
                Project = project
            };
            await _noteDataService.AddNoteAsync(note);
            return RedirectToAction("Details", "Projects", new { id = project.ProjectId });
        }
    }
}