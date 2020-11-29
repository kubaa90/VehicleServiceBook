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
        [Authorize(Roles = "Admin, Obsługa")]
        public async Task<IActionResult> Index()
        {
            var faults = await _faultService.GetAllAsync();
            FaultIndexViewModel faultIndexViewModel = new FaultIndexViewModel()
            {
                Faults = faults
            };
            return View("NewIndex", faultIndexViewModel);
        }
        [HttpGet]
        [Authorize(Roles = "Kierowca, Admin, Obsługa")]
        public IActionResult Create()
        {
            ViewData["VehicleModelID"] = new SelectList(_vehicleService.GetAll(), "Id", "Number").OrderBy(x => x.Text);
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Kierowca, Admin, Obsługa")]
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
                        //AddDateTimeString = DateTime.Now.ToString("g"),
                        Status = "Nowa",
                        Action= "-",
                        ProcessedUserName = "-",
                        OperatorRemarks = "-",
                        CreateUserName = _userManager.GetUserName(User),
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
        [Authorize(Roles = "Admin, Obsługa")]
        public IActionResult Details(int id)
        {
            var faultForDetails = _faultService.Get(id);
            FaultDetailsViewModel faultDetailsViewModel = new FaultDetailsViewModel()
            {
                Fault = faultForDetails,
                AddDateTimeString = _faultService.ConvertAddDateTimeToString(faultForDetails.AddDateTime),
                ProcessDateTimeString = _faultService.ConvertProcessTimeToString(faultForDetails.ProcessDateTime),
                OperatorRemarks = _faultService.ProcessRemarks(faultForDetails.OperatorRemarks)
            };
            return View("NewDetails",faultDetailsViewModel);
        }
        [HttpGet]
        [Authorize(Roles = "Kierowca, Admin, Obsługa")]
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
        [Authorize(Roles = "Kierowca, Admin, Obsługa")]
        public IActionResult Edit(int id)
        {
            var faultToEdit = _faultService.Get(id);
            ViewData["VehicleModelID"] = new SelectList(_vehicleService.GetAll(), "Id", "Number").OrderBy(x => x.Value);
            FaultEditViewModel faultEditViewModel = new FaultEditViewModel()
            {
                FaultId = faultToEdit.Id,
                Description = faultToEdit.Description,
                VehicleId = faultToEdit.VehicleId,
            };
            return View(faultEditViewModel);
        }
        [HttpPost]
        [Authorize(Roles = "Kierowca, Admin, Obsługa")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FaultEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var faultEdited = _faultService.Get(viewModel.FaultId);
                    faultEdited.Id = viewModel.Id;
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
        [HttpGet]
        [Authorize(Roles = "Admin, Obsługa")]
        public IActionResult Process(int id)
        {
            var faultToProcess = _faultService.Get(id);
            var vehicleToChangeStatus = _vehicleService.Get(faultToProcess.VehicleId);
            ViewData["VehicleModelID"] = new SelectList(_vehicleService.GetAll(), "Id", "Number").OrderBy(x => x.Value);
            FaultProcessViewModel faultProcessViewModel = new FaultProcessViewModel()
            {
                FaultId = faultToProcess.Id,
                Description = faultToProcess.Description,
                VehicleId = faultToProcess.VehicleId,
                IsVehicleAbleToDrive = vehicleToChangeStatus.IsAbleToDrive ?? true, 
                CreateUserName = faultToProcess.CreateUserName,
                OperatorRemarks = _faultService.ReverseProcessRemarks(faultToProcess.OperatorRemarks),
                Action = faultToProcess.Action ?? string.Empty,
                Status = faultToProcess.Status ?? "Nowe"
            };
            return View("NewProcess",faultProcessViewModel);
        }
        [HttpPost]
        [Authorize(Roles = "Admin, Obsługa")]
        [ValidateAntiForgeryToken]
        public IActionResult Process(FaultProcessViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var faultProcessed = _faultService.Get(viewModel.FaultId);
                    faultProcessed.Description = viewModel.Description;
                    faultProcessed.VehicleId = viewModel.VehicleId;
                    var vehicleToChangeStatus = _vehicleService.Get(viewModel.VehicleId);
                    vehicleToChangeStatus.IsAbleToDrive = viewModel.IsVehicleAbleToDrive;
                    faultProcessed.Action = viewModel.Action;
                    faultProcessed.Status = _faultService.ProcessStatus(viewModel.Action);
                    faultProcessed.OperatorRemarks = viewModel.OperatorRemarks;
                    faultProcessed.ProcessedUserName = _userManager.GetUserName(User);
                    faultProcessed.ProcessDateTime = DateTime.Now;
                    _faultService.Update(faultProcessed);
                    _vehicleService.Update(vehicleToChangeStatus);
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
            var allObj = _faultService.GetAll();
            return Json(new { data = allObj });
        }
        [HttpDelete]
        [Authorize(Roles = "Admin, Obsługa")]
        public async Task<IActionResult> Delete(int id)
        {
            var objFromDb = await _faultService.GetAsync(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Błąd przy usuwaniu" });
            }
            await _faultService.DeleteAsync(id);
            RedirectToAction("Index");
            return Json(new { success = true, message = "Usunięto" });
        }
        #endregion
    }
}
