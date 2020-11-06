using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleServiceBook.Services.Interfaces;
using VehicleServiceBook.ViewModels;

namespace VehicleServiceBook.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly UserManager<IdentityUser> _userManager;

        public VehicleController(IVehicleService vehicleService, UserManager<IdentityUser> userManager)
        {
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
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(VehicleCreateViewModel viewModel)
        {
            CategoryList = CatList.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }),
        }
    }
}