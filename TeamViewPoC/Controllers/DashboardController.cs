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
    public class DashboardController: Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProjectDataService _projectDataService;
        private readonly IProjectNoteDataService _projectNoteDataService;
        private readonly IWorkItemDataService _workItemDataService;

        public DashboardController(ApplicationDbContext context, IProjectDataService projectDataService, IProjectNoteDataService projectNoteDataService, IWorkItemDataService workItemDataService)
        {
            _context = context;
            _projectDataService = projectDataService;
            _projectNoteDataService = projectNoteDataService;
            _workItemDataService = workItemDataService;

        }

        // GET: Projects
        public async Task<IActionResult> MyDashboard()
        {

            var data = new MyDashboardViewModel();
            data.MyWorkItems = await _workItemDataService.GetActiveWorkItemsAsync();
            data.MyProjects = await _projectDataService.GetMyOpenProjects();
            return View(data);
        }
    }
}
