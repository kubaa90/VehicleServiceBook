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
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["ProducerModelID"] = new SelectList(_producerService.GetAll(), "Id", "Name").OrderBy(x => x.Text);
            return View();
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
                        Number = viewModel.Number,
                        VIN = viewModel.VIN,
                        PlateNumber = viewModel.PlateNumber,
                        ProducerId = viewModel.ProducerId,
                        RegistrationDate = viewModel.RegistrationDate,
                        RegistrationDateString = viewModel.RegistrationDate.ToString("d")
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
                Number = vehicleToEdit.Number,
                VIN = vehicleToEdit.VIN,
                PlateNumber = vehicleToEdit.PlateNumber,
                ProducerId = vehicleToEdit.ProducerId,
                RegistrationDate = vehicleToEdit.RegistrationDate,
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
                    vehicleEdited.Number = viewModel.Number;
                    vehicleEdited.VIN = viewModel.VIN;
                    vehicleEdited.PlateNumber = viewModel.PlateNumber;
                    vehicleEdited.ProducerId = viewModel.ProducerId;
                    vehicleEdited.RegistrationDate = viewModel.RegistrationDate;
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