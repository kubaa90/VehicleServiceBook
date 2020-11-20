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
    public class SystemController : Controller
    {
        private readonly ISystemService _systemService;
        private readonly UserManager<IdentityUser> _userManager;

        public SystemController(UserManager<IdentityUser> userManager, ISystemService systemService)
        {
            _systemService = systemService;
            _userManager = userManager;
        }
        [Authorize(Roles = "Admin, Obsługa")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin, Obsługa")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SystemCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    SystemModel system = new SystemModel
                    {
                        Name = viewModel.Name,
                        Description = viewModel.Description
                    };
                    _systemService.Create(system);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(viewModel);
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Obsługa")]
        public IActionResult Details(int id)
        {
            var systemDetails = _systemService.Get(id);
            SystemDetailsViewModel systemDetailsViewModel = new SystemDetailsViewModel()
            {
                Id = systemDetails.Id,
                Name = systemDetails.Name,
                Description = systemDetails.Description,
            };
            return View(systemDetailsViewModel);
        }
        [HttpGet]
        [Authorize(Roles = "Admin, Obsługa")]
        public IActionResult Edit(int id)
        {
            var systemToEdit = _systemService.Get(id);
            SystemEditViewModel systemEditViewModel = new SystemEditViewModel()
            {
                Id = systemToEdit.Id,
                Name = systemToEdit.Name,
                Description = systemToEdit.Description,
            };
            return View(systemEditViewModel);
        }
        [HttpPost]
        [Authorize(Roles = "Admin, Obsługa")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SystemEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var systemEdited = _systemService.Get(viewModel.Id);
                    systemEdited.Name = viewModel.Name;
                    systemEdited.Description = viewModel.Description;
                    _systemService.Update(systemEdited);
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
        [Authorize(Roles = "Admin, Obsługa")]
        public IActionResult List()
        {
            var allObj = _systemService.GetAll();
            return Json(new { data = allObj });
        }
        [HttpDelete]
        [Authorize(Roles = "Admin, Obsługa")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _systemService.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Błąd przy usuwaniu" });
            }
            _systemService.Delete(id);
            return Json(new { success = true, message = "Usunięto" });
        }
        #endregion
    }
}