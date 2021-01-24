using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleServiceBook.Models;
using VehicleServiceBook.Services.Interfaces;
using VehicleServiceBook.ViewModels;

namespace VehicleServiceBook.Controllers
{
    [Authorize(Roles = "Admin, Obsługa")]
    public class ProducerController:Controller
    {
        private readonly IProducerService _producerService;
        private readonly UserManager<IdentityUser> _userManager;

        public ProducerController(IProducerService producerService, UserManager<IdentityUser> userManager)
        {
            _producerService = producerService;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var producers = await _producerService.GetAllAsync();
            ProducerIndexViewModel producerIndexViewModel = new ProducerIndexViewModel()
            {
                Producers = producers
            };
            return View("Index", producerIndexViewModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProducerCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ProducerModel producer = new ProducerModel
                    {
                        Name = viewModel.Name,
                        Address = viewModel.Address
                    };
                    await _producerService.CreateAsync(producer);
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
            var producerToEdit = await _producerService.GetAsync(id);
            ProducerEditViewModel producerEditViewModel = new ProducerEditViewModel()
            {
                Id = producerToEdit.Id,
                Name = producerToEdit.Name,
                Address = producerToEdit.Address,
            };
            return View(producerEditViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProducerEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var producerEdited = await _producerService.GetAsync(viewModel.Id);
                    producerEdited.Name = viewModel.Name;
                    producerEdited.Address = viewModel.Address;
                    _producerService.Update(producerEdited);
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
            var producerForDetails = await _producerService.GetAsync(id);
            ProducerDetailsViewModel producerDetailsViewModel = new ProducerDetailsViewModel()
            {
                Producer = producerForDetails,
            };
            return View("Details", producerDetailsViewModel);
        }
        #region API CALLS
        [HttpGet]
        public IActionResult List()
        {
            var allObj = _producerService.GetAll();
            return Json(new { data = allObj });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _producerService.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Błąd przy usuwaniu" });
            }
            _producerService.Delete(id);
            return Json(new { success = true, message = "Usunięto" });
        }
        #endregion
    }
}
