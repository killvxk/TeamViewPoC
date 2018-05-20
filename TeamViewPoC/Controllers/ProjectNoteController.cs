using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamViewPoC.Services;
using TeamViewPoC.Models;


namespace TeamViewPoC.Controllers
{
    public class ProjectNoteController : Controller
    {
        private readonly IProjectNoteDataService _projectNoteDataService;
        private readonly IProjectDataService _projectDataService;

        public ProjectNoteController(IProjectDataService projectDataService, IProjectNoteDataService projectNoteDataService)
        {
            _projectDataService = projectDataService;
            _projectNoteDataService = projectNoteDataService;

        }


        // POST: ProjectNote/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProjectNote note, int projectid)
        {
            ProjectNote projectnote = new ProjectNote
            {
                NoteContent = note.NoteContent, 
                CreatedOn = DateTime.Now,
                CreatedBy = Constants.HardCodedSignedInUser,
                Project = await _projectDataService.GetProjectById(projectid)
            };
            

                await _projectNoteDataService.AddProjectNoteAsync(projectnote);
                return RedirectToAction("Details", "Projects", new { id = projectid});

        }

        // POST: ProjectNote/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return View();
            }
            catch
            {
                return View();
            }
        }

        // POST: ProjectNote/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}