using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleServiceBook.Context
{
    public class UserInitialize
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UserInitialize(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public bool InitializeUserAsync(string userName, string password)
        {
            var user = new IdentityUser { UserName = userName };

            var result = _userManager.CreateAsync(user, password).GetAwaiter().GetResult();

            if (result.Succeeded)
            {
                return true;
                ////var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var code = _userManager.GenerateEmailConfirmationTokenAsync(user).GetAwaiter().GetResult();
                //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                //code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
                ////var result2 = await _userManager.ConfirmEmailAsync(user, code);
                //var result2 = _userManager.ConfirmEmailAsync(user, code).GetAwaiter().GetResult();
            }
            return false;

        }
    }
}
