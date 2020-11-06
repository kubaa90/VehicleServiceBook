using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleServiceBook.Models;
using VehicleServiceBook.Services.Interfaces;
using VehicleServiceBook.ViewModels;

namespace VehicleServiceBook.Controllers
{
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
    }
}
