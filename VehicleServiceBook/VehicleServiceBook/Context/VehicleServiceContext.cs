using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VehicleServiceBook.Models;
using VehicleServiceBook.ViewModels;

namespace VehicleServiceBook.Context
{
    public class VehicleServiceContext:IdentityDbContext
    {
        public VehicleServiceContext(DbContextOptions<VehicleServiceContext> options):base(options)
        {
            
        }
        public DbSet<VehicleModel> Vehicles { get; set; }

        public DbSet<ProducerModel> Producers { get; set; }
        public DbSet<FaultModel> Faults { get; set; }

        public DbSet<SystemModel> Systems { get; set; }
    }
}
