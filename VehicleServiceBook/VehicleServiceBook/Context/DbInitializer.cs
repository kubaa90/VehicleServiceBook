using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleServiceBook.Context
{
    public static class DbInitializer
    {
        public static void Initialize (VehicleServiceContext context, UserManager<IdentityUser> userManager)
        {
            UserInitialize userInitialize = new UserInitialize(userManager);
            userInitialize.InitializeUserAsync("admin", "Admin12345");
        }
    }
}
