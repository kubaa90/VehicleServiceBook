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
        public async Task<IActionResult> Create(VehicleCreateViewModel viewModel)
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
                    _vehicleService.Create(vehicle);
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
        public IActionResult Edit(int id)
        {
            var vehicleToEdit = _vehicleService.Get(id);
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
                    var vehicleEdited = _vehicleService.Get(viewModel.Id);
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
        public IActionResult List()
        {
            var allObj = _vehicleService.GetAll();
            return Json(new { data = allObj });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _vehicleService.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Błąd przy usuwaniu" });
            }
            _vehicleService.Delete(id);
            return Json(new { success = true, message = "Usunięto" });
        }
        #endregion
    }
}