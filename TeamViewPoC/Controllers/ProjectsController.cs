using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamViewPoC.Data;
using TeamViewPoC.Services;
using TeamViewPoC.Models;
using TeamViewPoC.Models.ViewModels;

namespace TeamViewPoC.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProjectDataService _projectDataService;
        private readonly IProjectNoteDataService _projectNoteDataService;
        private readonly IWorkItemDataService _workItemDataService;

        public ProjectsController(ApplicationDbContext context, IProjectDataService projectDataService, IProjectNoteDataService projectNoteDataService, IWorkItemDataService workItemDataService)
        {
            _context = context;
            _projectDataService = projectDataService;
            _projectNoteDataService = projectNoteDataService;
            _workItemDataService = workItemDataService;

        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            return View(await _context.Projects.ToListAsync());
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var view = new ProjectDetailViewModel();
            if (id == null)
            {
                return NotFound();
            }

            view.Project = await _projectDataService.GetProjectById(id, "projectNotes");
            if (view.Project == null)
            {
                return NotFound();
            }
            view.Project.WorkItems = await _workItemDataService.GetByProjectId(view.Project.ProjectId);
            return View(view);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectId,Title,Description,CreatedOn,DueDate,LastUpdated,Active,Complete,AssignedTo,CreatedBy,Status")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.CreatedBy = Constants.HardCodedSignedInUser;
                project.CreatedOn = DateTime.Now;
                project.LastUpdated = DateTime.Now;
                project.DueDate = project.DueDate.AddHours(17);
                project.Active = true;
                project.Complete = false;
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.SingleOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectId,Title,Description,CreatedOn,DueDate,LastUpdated,Active,Complete,AssignedTo,CreatedBy,Status")] Project project)
        {
            if (id != project.ProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .SingleOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(m => m.ProjectId == id);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.ProjectId == id);
        }
    }
}
