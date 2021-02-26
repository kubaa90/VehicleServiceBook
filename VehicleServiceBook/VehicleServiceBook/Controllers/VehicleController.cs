using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleServiceBook.Models;
using VehicleServiceBook.Services.Interfaces;
using VehicleServiceBook.ViewModels;

namespace VehicleServiceBook.Controllers
{
    [Authorize(Roles = "Admin, Obsługa")]
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IProducerService _producerService;
        private readonly UserManager<IdentityUser> _userManager;

        public VehicleController(IVehicleService vehicleService, UserManager<IdentityUser> userManager, IProducerService producerService)
        {
            _vehicleService = vehicleService;
            _userManager = userManager;
            _producerService = producerService;
        }
        [HttpGet]
        [Authorize(Roles = "Admin, Obsługa")]
        public async Task<IActionResult> Index()
        {
            var vehicles = await _vehicleService.GetAllAsync();
            VehicleIndexViewModel vehicleIndexViewModel = new VehicleIndexViewModel()
            {
                Vehicles = vehicles
            };
            return View("Index", vehicleIndexViewModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["ProducerModelID"] = new SelectList(_producerService.GetAll(), "Id", "Name").OrderBy(x => x.Text);
            return View("NewCreate");
        }
        [HttpPost]
        public async Task<ActionResult> Create(VehicleCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    VehicleModel vehicle = new VehicleModel()
                    {
                        Number = viewModel.Vehicle.Number,
                        VIN = viewModel.Vehicle.VIN,
                        PlateNumber = viewModel.Vehicle.PlateNumber,
                        ProducerId = viewModel.Vehicle.ProducerId,
                        RegistrationDate = viewModel.Vehicle.RegistrationDate,
                        IsOnWarranty = viewModel.Vehicle.IsOnWarranty,
                        WarrantyTerms= viewModel.Vehicle.IsOnWarranty ? viewModel.Vehicle.WarrantyTerms : null
                    };
                    await _vehicleService.CreateAsync(vehicle);
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var vehicleToEdit = await _vehicleService.GetAsync(id);
            ViewData["ProducerModelID"] = new SelectList(_producerService.GetAll(), "Id", "Name").OrderBy(x => x.Text);
            VehicleEditViewModel vehicleEditViewModel = new VehicleEditViewModel
            {
                Id = vehicleToEdit.Id,
                Vehicle = vehicleToEdit
            };
            return View(vehicleEditViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VehicleEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var vehicleEdited = await _vehicleService.GetAsync(viewModel.Id);
                    vehicleEdited.Number = viewModel.Vehicle.Number;
                    vehicleEdited.VIN = viewModel.Vehicle.VIN;
                    vehicleEdited.PlateNumber = viewModel.Vehicle.PlateNumber;
                    vehicleEdited.ProducerId = viewModel.Vehicle.ProducerId;
                    vehicleEdited.RegistrationDate = viewModel.Vehicle.RegistrationDate;
                    _vehicleService.Update(vehicleEdited);
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
        [HttpGet]
        [Authorize(Roles = "Admin, Obsługa")]
        public async Task<IActionResult> Details(int id)
        {
            var vehicleForDetails = await _vehicleService.GetAsync(id);
            VehicleDetailsViewModel vehicleDetailsViewModel = new VehicleDetailsViewModel()
            {
                Vehicle = vehicleForDetails,
            };
            return View("Details", vehicleDetailsViewModel);
        }
        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var allObj = await _vehicleService.GetAllAsync();
            return Json(new { data = allObj });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var objFromDb = await _vehicleService.GetAsync(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Błąd przy usuwaniu" });
            }
            await _vehicleService.DeleteAsync(id);
            return Json(new { success = true, message = "Usunięto" });
        }
        #endregion
    }
}