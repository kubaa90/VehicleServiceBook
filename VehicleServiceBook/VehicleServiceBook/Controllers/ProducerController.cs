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
        public IActionResult Index()
        {
            return View();
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
                    _producerService.Create(producer);
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
            var producerToEdit = _producerService.Get(id);
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
                    var producerEdited = _producerService.Get(viewModel.Id);
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
