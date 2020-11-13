﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleServiceBook.Models;
using VehicleServiceBook.Services.Interfaces;
using VehicleServiceBook.ViewModels;

namespace VehicleServiceBook.Controllers
{
    public class FaultController:Controller
    {
        private readonly IFaultService _faultService;
        private readonly IVehicleService _vehicleService;
        private readonly UserManager<IdentityUser> _userManager;

        public FaultController(IVehicleService vehicleService, UserManager<IdentityUser> userManager, IFaultService faultService)
        {
            _faultService = faultService;
            _vehicleService = vehicleService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["VehicleModelID"] = new SelectList(_vehicleService.GetAll(), "Id", "Number").OrderBy(x => x.Text);
            return View();
        }
        [HttpPost]
        public IActionResult Create(FaultCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    FaultModel fault = new FaultModel()
                    {
                        Description = viewModel.Description,
                        VehicleId = viewModel.VehicleId,
                        AddDateTime = DateTime.Now,
                        UserId = _userManager.GetUserId(User),
                        AddDateTimeString = DateTime.Now.ToString("g")
                    };
                    _faultService.Create(fault);
                    return RedirectToAction("Confirm", new { id = fault.Id });
                }
                catch
                {
                    return View(viewModel);
                }
            }
            else
            {
                return View(viewModel);
            }
        }

        [HttpGet] 
        public IActionResult Details(int id)
        {
            var faultDetails = _faultService.Get(id);
            FaultDetailsViewModel faultDetailsViewModel = new FaultDetailsViewModel()
            {
                Id = faultDetails.Id,
                Description = faultDetails.Description,
                VehicleId = faultDetails.VehicleId,
                Vehicle = _vehicleService.Get(faultDetails.VehicleId)
            };
            return View(faultDetailsViewModel);
        }
        [HttpGet]
        public IActionResult Confirm(int id)
        {
            var faultConfirm = _faultService.Get(id);
            FaultConfirmViewModel faultConfirmViewModel = new FaultConfirmViewModel()
            {
                Id = faultConfirm.Id,
                Description = faultConfirm.Description,
                VehicleId = faultConfirm.VehicleId,
                Vehicle = _vehicleService.Get(faultConfirm.VehicleId)
            };
            return View(faultConfirmViewModel);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var faultToEdit = _faultService.Get(id);
            ViewData["VehicleModelID"] = new SelectList(_vehicleService.GetAll(), "Id", "Number").OrderBy(x => x.Value);
            FaultEditViewModel faultEditViewModel = new FaultEditViewModel()
            {
                Id = faultToEdit.Id,
                Description = faultToEdit.Description,
                VehicleId = faultToEdit.VehicleId,
            };
            return View(faultEditViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FaultEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var faultEdited = _faultService.Get(viewModel.Id);
                    faultEdited.Description = viewModel.Description;
                    faultEdited.VehicleId = viewModel.VehicleId;
                    _faultService.Update(faultEdited);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(viewModel);
                }
            }
            else
            {
                return View(viewModel);
            }
        }
        #region API CALLS
        [HttpGet]
        public IActionResult List()
        {
            var allObj = _faultService.GetAll();
            return Json(new { data = allObj });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _faultService.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Błąd przy usuwaniu" });
            }
            _faultService.Delete(id);
            return Json(new { success = true, message = "Usunięto" });
        }
        #endregion
    }
}