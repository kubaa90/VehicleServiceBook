using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleServiceBook.Models;
using VehicleServiceBook.ViewModels;

namespace VehicleServiceBook.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AccountController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;

        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var isUserExist = await _userManager.FindByNameAsync(viewModel.UserName);
                if (!await _roleManager.RoleExistsAsync(viewModel.Role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(viewModel.Role));
                }
                if (isUserExist == null)
                {
                    var user = new IdentityUser()
                    {
                        UserName = viewModel.UserName,
                        Email = viewModel.Email,
                        PhoneNumber = viewModel.PhoneNumber
                    };

                    // Saves the role in the underlying AspNetRoles table
                    /*IdentityResult identityResult = await _roleManager.CreateAsync(identityRole);
                    if (identityResult.Succeeded)
                    {
                        
                    }*/
                    var applicationUserResult = await _userManager.CreateAsync(user, viewModel.Password);
                    if (applicationUserResult.Succeeded)
                    {
                        //Add First and Last Name
                        await _userManager.AddClaimAsync(user, new Claim("FirstName", viewModel.Name));
                        await _userManager.AddClaimAsync(user, new Claim("LastName", viewModel.Surname));
                        var userFromDb = await _userManager.FindByNameAsync(user.UserName);
                        await _userManager.AddToRoleAsync(userFromDb, viewModel.Role);
                        return RedirectToAction("index", "Account");
                    }

                    foreach (var error in applicationUserResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Użytkownik o tym emailu już jest zarejestrowany!");
                    return View(viewModel);
                }

                //raz trzeba stworzyc role - potem moze w panelu administratora
                //-----------------------------
                /*if (_roleManager.Roles.Any(x => x.Name == "Admin")) //User
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }
                else
                {
                    var identityRole = new IdentityRole("Admin"); //User
                    var createResult = await _roleManager.CreateAsync(identityRole);
                    if (createResult.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "Admin"); //User
                    }
                    else
                    {
                        ModelState.AddModelError("", "Cant Add Uprawnienia");
                    }

                    var identityRoleBlocked = new IdentityRole("Blocked");
                    var createResultBlocked = await _roleManager.CreateAsync(identityRoleBlocked);
                    if (!createResultBlocked.Succeeded)
                    {
                        ModelState.AddModelError("", "Cant Add Uprawnienia Blocked");
                    }
                }
            }*/
            }

            return View(viewModel);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var login = await _signInManager.PasswordSignInAsync
                    (model.UserName, model.Password, true, false);
                if (login.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Nie można się zalogować!");
                    return View(model);
                }
            }
            return View(model);
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> List()
        {
            var usersIdentity = _userManager.Users.ToList();
            List<ApplicationUserViewModel> users = new List<ApplicationUserViewModel>();
            foreach (var item in usersIdentity)
            {
                var claims = await _userManager.GetClaimsAsync(item);
                var nameClaim = claims.Where(x => x.Type == "FirstName").FirstOrDefault();
                var surnameClaim = claims.Where(x => x.Type == "LastName").FirstOrDefault();

                var roles = await _userManager.GetRolesAsync(item);

                ApplicationUserViewModel userVM = new ApplicationUserViewModel()
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    Name = nameClaim.Value,
                    Surname = surnameClaim.Value,
                    Role = roles.FirstOrDefault(),
                    Email = item.Email,
                    PhoneNumber = item.PhoneNumber
                };
                users.Add(userVM);
            }
            return Json(new { data = users });
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return Content("Error");
            }
            var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var model = new EditUserViewModel()
            {
                ID = user.Id,
                UserName = user.UserName,
                Name = claims.Where(x => x.Type == "FirstName").FirstOrDefault().Value,
                Surname = claims.Where(x => x.Type == "LastName").FirstOrDefault().Value,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = roles.FirstOrDefault()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(viewModel.ID);
                user.UserName = user.UserName;
                user.NormalizedUserName = viewModel.UserName.ToUpper();
                user.Email = viewModel.Email;
                user.NormalizedEmail = viewModel.Email.ToUpper();
                user.PhoneNumber = viewModel.PhoneNumber;
                //Change first and last name
                var claims = await _userManager.GetClaimsAsync(user);
                var nameClaim = claims.Where(x => x.Type == "FirstName").FirstOrDefault();
                var surnameClaim = claims.Where(x => x.Type == "LastName").FirstOrDefault();
                await _userManager.ReplaceClaimAsync(user, nameClaim, new Claim("FirstName", viewModel.Name));
                await _userManager.ReplaceClaimAsync(user, surnameClaim, new Claim("LastName", viewModel.Surname));
                //-------
                //Change role
                var roles = await _userManager.GetRolesAsync(user);
                var oldRole = roles.FirstOrDefault();
                if (roles != null)
                {
                    var rola = await _userManager.RemoveFromRoleAsync(user, oldRole);
                    var nowaRola =await _userManager.AddToRoleAsync(user, viewModel.Role);
                    
                }
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Content("Updated");
                }
                /*var isSignIn = await _signInManager.CanSignInAsync(user);
                if (isSignIn)
                {
                    await _signInManager.RefreshSignInAsync(user);
                    return RedirectToAction("Index", "Dashboard");
                }*/

            }

            else
            {
                ModelState.AddModelError("", "Nie można edytować!");
            }

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return Json(new { success = false, message = "Nie można usunąć użytkownika" });
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return Json(new { success = true, message = "Usunięto użytkownika" });
                }
                else
                {
                    return Json(new { success = false, message = "Nie udało się usunąć użytkownika" });
                }
            }
        }
        public async Task<IActionResult> BlockUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            var oldRole = roles.FirstOrDefault();
            await _userManager.AddClaimAsync(user, new Claim("OldRole", oldRole));
            await _userManager.RemoveFromRoleAsync(user, oldRole);
            if (!await _roleManager.RoleExistsAsync("Zablokowany"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Zablokowany"));
            }
            var result = await _userManager.AddToRoleAsync(user, "Zablokowany");
            if (result.Succeeded)
            {
                return Json(new { success = true, message = "Zablokowano użytkownika" });
            }
            else
            {
                return Json(new { success = false, message = "Nie udało się zablokować użytkownika" });
            }
        }
        public async Task<IActionResult> UnBlockUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var claims = await _userManager.GetClaimsAsync(user);
            Claim oldRoleClaim;
            string oldRoleClaimName;
            try
            {
                oldRoleClaim = claims.Where(x => x.Type == "OldRole").FirstOrDefault();
                oldRoleClaimName = oldRoleClaim.Value;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { success = false, message = "Nie udało się odblokować użytkownika" });
            }
            await _userManager.RemoveClaimAsync(user, oldRoleClaim);
            await _userManager.RemoveFromRoleAsync(user, "Zablokowany");
            if (!await _roleManager.RoleExistsAsync(oldRoleClaimName))
            {
                await _roleManager.CreateAsync(new IdentityRole(oldRoleClaimName));
            }
            var result = await _userManager.AddToRoleAsync(user, oldRoleClaimName);
            if (result.Succeeded)
            {
                return Json(new { success = true, message = "Odblokowano użytkownika" });
            }
            else
            {
                return Json(new { success = false, message = "Nie udało się odblokować użytkownika" });
            }
        }
    }
}

