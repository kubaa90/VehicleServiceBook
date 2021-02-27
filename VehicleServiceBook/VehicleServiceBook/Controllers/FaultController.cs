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
        private readonly IInternalServiceService _internalServiceService;

        public FaultController(IVehicleService vehicleService, UserManager<IdentityUser> userManager, IFaultService faultService, IInternalServiceService internalServiceService)
        {
            _faultService = faultService;
            _vehicleService = vehicleService;
            _userManager = userManager;
            _internalServiceService = internalServiceService;
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
        public async Task<IActionResult> Create()
        {
            ViewData["VehicleModelID"] = new SelectList(await _vehicleService.GetAllAsync(), "Id", "Number").OrderBy(x => x.Text);
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Kierowca, Admin, Obsługa")]
        public async Task<IActionResult> Create(FaultCreateViewModel viewModel)
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
                    await _faultService.CreateAsync(fault);
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
        public async Task<IActionResult> Details(int id)
        {
            var faultForDetails = await _faultService.GetAsync(id);
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
        public async Task<IActionResult> Confirm(int id)
        {
            var faultConfirm = await _faultService.GetAsync(id);
            FaultConfirmViewModel faultConfirmViewModel = new FaultConfirmViewModel()
            {
                Id = faultConfirm.Id,
                Description = faultConfirm.Description,
                VehicleId = faultConfirm.VehicleId,
                Vehicle = await _vehicleService.GetAsync(faultConfirm.VehicleId)
            };
            return View(faultConfirmViewModel);
        }
        [HttpGet]
        [Authorize(Roles = "Kierowca, Admin, Obsługa")]
        public async Task<IActionResult> Edit(int id)
        {
            var faultToEdit = await _faultService.GetAsync(id);
            ViewData["VehicleModelID"] = new SelectList(await _vehicleService.GetAllAsync(), "Id", "Number").OrderBy(x => x.Value);
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
        public async Task<IActionResult> Edit(FaultEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var faultEdited = await _faultService.GetAsync(viewModel.FaultId);
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
        public async Task<IActionResult> Process(int id)
        {
            var faultToProcess = await _faultService.GetAsync(id);
            var vehicleToChangeStatus = await _vehicleService.GetAsync(faultToProcess.VehicleId);
            ViewData["VehicleModelID"] = new SelectList(await _vehicleService.GetAllAsync(), "Id", "Number").OrderBy(x => x.Value);
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
        public async Task<IActionResult> Process(FaultProcessViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var internalServiceFault = new InternalServiceModel();
                    var faultProcessed = await _faultService.GetAsync(viewModel.FaultId);
                    faultProcessed.Description = viewModel.Description;
                    faultProcessed.VehicleId = viewModel.VehicleId;
                    var vehicleToChangeStatus = await _vehicleService.GetAsync(viewModel.VehicleId);
                    vehicleToChangeStatus.IsAbleToDrive = viewModel.IsVehicleAbleToDrive;
                    faultProcessed.Action = viewModel.Action;
                    faultProcessed.Status = _faultService.ProcessStatus(viewModel.Action);
                    faultProcessed.OperatorRemarks = viewModel.OperatorRemarks;
                    faultProcessed.ProcessedUserName = _userManager.GetUserName(User);
                    faultProcessed.ProcessDateTime = DateTime.Now;
                    if (faultProcessed.Action == "IntService")
                    {
                        internalServiceFault.FaultId = faultProcessed.Id;
                        //internalServiceFault.VehicleNumber = faultProcessed.Vehicle.Number;
                        await _internalServiceService.CreateAsync(internalServiceFault);
                    }
                    _faultService.Update(faultProcessed);
                    _vehicleService.Update(vehicleToChangeStatus);
                    return RedirectToAction("Index", new { id = internalServiceFault.Id });
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
        public async Task<IActionResult> List()
        {
            var allObj = await _faultService.GetAllAsync();
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
