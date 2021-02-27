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
    public class InternalServiceController:Controller
    {
        private readonly IInternalServiceService _internalServiceService;
        private readonly UserManager<IdentityUser> _userManager;

        public InternalServiceController(UserManager<IdentityUser> userManager, IInternalServiceService internalService)
        {
            _userManager = userManager;
            _internalServiceService = internalService;
        }
        [HttpGet]
        [Authorize(Roles = "Admin, Obsługa, Serwis")]
        public async Task<IActionResult> Index()
        {
            var internalServices = await _internalServiceService.GetAllAsync();
            InternalServiceIndexViewModel internalServiceIndexViewModel = new InternalServiceIndexViewModel()
            {
                InternalServices = internalServices,
            };
            return View(internalServiceIndexViewModel);
        }
    }
}
