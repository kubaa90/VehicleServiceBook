using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using VehicleServiceBook.Models;
using VehicleServiceBook.ViewModels;

namespace VehicleServiceBook.Context
{
    public class VehicleServiceContext : IdentityDbContext
    {
        //private readonly UserManager<IdentityUser> _userManager;
        public VehicleServiceContext(DbContextOptions<VehicleServiceContext> options/*UserManager<IdentityUser> userManager*/) : base(options)
        {
            //_userManager = userManager;
        }
        public DbSet<VehicleModel> Vehicles { get; set; }

        public DbSet<ProducerModel> Producers { get; set; }
        public DbSet<FaultModel> Faults { get; set; }

        public DbSet<SystemModel> Systems { get; set; }

        public DbSet<InternalServiceModel> InternalServices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            string admin_role_Id = "02174cf0–9412–4cfe-afbf-59f706d72cf6";
            string admin_Id = "341743f0-asd2–42de-afbf-59kmkkmk72cf6";
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(new IdentityUserClaim<string>
            {
                Id = 1,
                UserId = admin_Id,
                ClaimType = "FirstName",
                ClaimValue = "Admin"
            });
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(new IdentityUserClaim<string>
            {
                Id = 2,
                UserId = admin_Id,
                ClaimType = "LastName",
                ClaimValue = "Admin"
            });
            //seed admin role
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
                Id = admin_role_Id,
                ConcurrencyStamp = admin_role_Id,
            });

            //create user
            var applicationAdmin = new IdentityUser()
            {
                Id = admin_Id,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@polgard.com",
                EmailConfirmed = false,
                PhoneNumber = "501398698",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = false,
           };
            //_userManager.AddClaimAsync(applicationAdmin, new Claim("FirstName", "Admin"));
            //_userManager.AddClaimAsync(applicationAdmin, new Claim("LastName", "Admin"));

            //set user password
            PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
            applicationAdmin.PasswordHash = ph.HashPassword(applicationAdmin, "Admin12345");

            //seed user
            modelBuilder.Entity<IdentityUser>().HasData(applicationAdmin);

            //set user role to admin
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = admin_role_Id,
                UserId = admin_Id
            });
        }
        //modelBuilder.Entity<VehicleModel>().ToTable("Vehicle");
        //modelBuilder.Entity<VehicleModel>()
        //    .HasData(
        //        new VehicleModel
        //        {
        //            Id = 1,
        //            Number = "911",
        //            VIN = "12345",
        //            PlateNumber = "WZ1234Z",
        //            ProducerId = 1,
        //            RegistrationDate = DateTime.Now,
        //            RegistrationDateString = "2020.01.22",
        //            HasFault = false,
        //            IsAbleToDrive = true
        //        });
        //modelBuilder.Entity<ProducerModel>().ToTable("Producers");
        //modelBuilder.Entity<ProducerModel>()
        //    .HasData(
        //        new ProducerModel
        //        {
        //            Id = 1,
        //            Name = "Solaris",
        //            Address = "al. Jerozolimskie 123"
        //        });
    }
}
