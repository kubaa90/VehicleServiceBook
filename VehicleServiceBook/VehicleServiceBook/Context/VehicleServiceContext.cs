using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VehicleServiceBook.ViewModels;

namespace VehicleServiceBook.Context
{
    public class VehicleServiceContext:IdentityDbContext
    {
        public VehicleServiceContext(DbContextOptions<VehicleServiceContext> options):base(options)
        {
            
        }
        
    }
}
