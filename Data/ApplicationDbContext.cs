using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRentalService.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole
                    {
                        Id = "0f3f8561-ba6a-4530-b5e4-9e66425089de",
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                        ConcurrencyStamp = "a0b6873b-bcfd-4880-8987-3a403620f5f2"
                    },
                    new IdentityRole
                    {
                        Id = "ff2a00f0-9394-4ebf-9546-135cd2ab6500",
                        Name = "Customer",
                        NormalizedName = "CUSTOMER",
                        ConcurrencyStamp = "5d137252-b432-4cba-a5ec-6b1150addd1a"

                    },
                    new IdentityRole
                    {
                        Id = "135fc814-e4c8-4423-b037-6b28a1eb96d0",
                        Name = "Employee",
                        NormalizedName = "EMPLOYEE",
                        ConcurrencyStamp = "3a9c7601-e621-457d-8f50-7572109c09a7"
                    }
                );
        }
    }
}
