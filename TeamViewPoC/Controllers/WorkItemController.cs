﻿using System;
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
        public IActionResult Index()
        {
            return View();
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
    }
}