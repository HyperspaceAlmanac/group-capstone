using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using CarRentalService.Models;

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
            builder.Entity<Vehicle>()
                .HasData(
                    new Vehicle
                    {
                        Id = 1,
                        Make = "Ford",
                        Model = "F-150",
                        Image = "https://bringatrailer.com/wp-content/uploads/2017/03/58c1cafa21bcb_143322.jpg?fit=940%2C598",
                        Year = 2000,
                        Fuel = 100,
                        Odometer = 1000,
                        CurrentStreet = "555 Claire Avenue",
                        CurrentCity = "Chula Vista",
                        CurrentState = "CA",
                        CurrentZip = "91910",
                        IsAvailable = true,
                        IsOperational = true
                    },
                    new Vehicle
                    {
                        Id = 2,
                        Make = "Oldsmobile",
                        Model = "Cutlass",
                        Image = "https://car-from-uk.com/ebay/carphotos/full/ebay657229.jpg",
                        Year = 1985,
                        Fuel = 100,
                        Odometer = 1000,
                        CurrentStreet = "2519 Calle Gaviota",
                        CurrentCity = "San Diego",
                        CurrentState = "CA",
                        CurrentZip = "92139",
                        IsAvailable = true,
                        IsOperational = true
                    },
                    new Vehicle
                    {
                        Id = 3,
                        Make = "Toyota",
                        Model = "Tacoma",
                        Image = "https://hips.hearstapps.com/hmg-prod/amv-prod-cad-assets/images/08q1/267367/2008-toyota-tacoma-photo-193561-s-original.jpg?fill=2:1&resize=1200:*",
                        Year = 2008,
                        Fuel = 100,
                        Odometer = 101011,
                        CurrentStreet = "1120 Cuyamaca Avenue",
                        CurrentCity = "Chula Vista",
                        CurrentState = "CA",
                        CurrentZip = "91911",
                        IsAvailable = true,
                        IsOperational = true
                    },
                        new Vehicle
                        {
                            Id = 4,
                            Make = "Chevrolet",
                            Model = "Bronco",
                            Image = "https://cdn1.mecum.com/auctions/pa0715/pa0715-216861/images/pa0715-216861_2@2x.jpg?1436997552000",
                            Year = 1979,
                            Fuel = 100,
                            Odometer = 90011,
                            CurrentStreet = "9449 Friars Road",
                            CurrentCity = "San Diego",
                            CurrentState = "CA",
                            CurrentZip = "92108",
                            IsAvailable = true,
                            IsOperational = true
                        },
                        new Vehicle
                        {
                            Id = 5,
                            Make = "Nissan",
                            Model = "Altima",
                            Image = "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/2019-nissan-altima-102-1538074559.jpg?crop=0.822xw:1.00xh;0.138xw,0&resize=640:*",
                            Year = 2021,
                            Fuel = 100,
                            Odometer = 90011,
                            CurrentStreet = "740 Hilltop Drive",
                            CurrentCity = "Chula Vista",
                            CurrentState = "CA",
                            CurrentZip = "91910",
                            IsAvailable = true,
                            IsOperational = true
                        }
                 );
        }

               

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<ServiceReceipt> ServiceReceipt { get; set; }
        public DbSet<Trip> Trips { get; set; }

    }
}
