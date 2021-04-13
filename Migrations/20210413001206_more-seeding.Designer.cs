﻿// <auto-generated />
using System;
using CarRentalService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarRentalService.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210413001206_more-seeding")]
    partial class moreseeding
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CarRentalService.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CompletedRegistration")
                        .HasColumnType("bit");

                    b.Property<string>("CurrentCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("CurrentLat")
                        .HasColumnType("float");

                    b.Property<double>("CurrentLong")
                        .HasColumnType("float");

                    b.Property<string>("CurrentState")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CurrentStreet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CurrentZip")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DriverLicenseNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdentityUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalBalance")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdentityUserId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("CarRentalService.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("CompletedRegistration")
                        .HasColumnType("bit");

                    b.Property<string>("IdentityUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Zipcode")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdentityUserId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("CarRentalService.Models.Issue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Resolved")
                        .HasColumnType("bit");

                    b.Property<string>("ServiceNeeded")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeReported")
                        .HasColumnType("datetime2");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("Issues");
                });

            modelBuilder.Entity("CarRentalService.Models.ServiceReceipt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("VehicleId");

                    b.ToTable("ServiceReceipt");
                });

            modelBuilder.Entity("CarRentalService.Models.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AfterTripBackImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AfterTripFrontImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AfterTripInteriorBack")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AfterTripInteriorFront")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AfterTripLeftImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AfterTripRightImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BeforeTripBackImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BeforeTripFrontImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BeforeTripInteriorBack")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BeforeTripInteriorFront")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BeforeTripLeftImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BeforeTripRightImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Cost")
                        .HasColumnType("float");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<double>("EndLat")
                        .HasColumnType("float");

                    b.Property<double>("EndLng")
                        .HasColumnType("float");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("FuelEnd")
                        .HasColumnType("int");

                    b.Property<int>("FuelStart")
                        .HasColumnType("int");

                    b.Property<int>("OdometerEnd")
                        .HasColumnType("int");

                    b.Property<int>("OdometerStart")
                        .HasColumnType("int");

                    b.Property<double>("StartLat")
                        .HasColumnType("float");

                    b.Property<double>("StartLng")
                        .HasColumnType("float");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("CarRentalService.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CurrentCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CurrentState")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CurrentStreet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CurrentZip")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Distance")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Duration")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Fuel")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOperational")
                        .HasColumnType("bit");

                    b.Property<double?>("LastKnownLatitude")
                        .HasColumnType("float");

                    b.Property<double?>("LastKnownLongitude")
                        .HasColumnType("float");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Make")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Odometer")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Vehicles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CurrentCity = "Chula Vista",
                            CurrentState = "CA",
                            CurrentStreet = "555 Claire Avenue",
                            CurrentZip = "91910",
                            Fuel = 100,
                            Image = "https://dealeraccelerate-all.s3.amazonaws.com/adrenalin/images/3/5/6/356/87792652e1c0_hd_2000-ford-f-150-harley-davidson-limited-edition.jpg",
                            IsAvailable = true,
                            IsOperational = true,
                            Make = "Ford",
                            Model = "F-150",
                            Odometer = 1000,
                            Year = 2000
                        },
                        new
                        {
                            Id = 2,
                            CurrentCity = "San Diego",
                            CurrentState = "CA",
                            CurrentStreet = "2519 Calle Gaviota",
                            CurrentZip = "92139",
                            Fuel = 100,
                            Image = "http://car-from-uk.com/ebay/carphotos/full/ebay657234.jpg",
                            IsAvailable = true,
                            IsOperational = true,
                            Make = "Oldsmobile",
                            Model = "Cutlass",
                            Odometer = 1000,
                            Year = 1985
                        },
                        new
                        {
                            Id = 3,
                            CurrentCity = "Chula Vista",
                            CurrentState = "CA",
                            CurrentStreet = "1120 Cuyamaca Avenue",
                            CurrentZip = "91911",
                            Fuel = 100,
                            Image = "https://www.google.com/imgres?imgurl=https%3A%2F%2Fsmartcdn.prod.postmedia.digital%2Fdriving%2Fimages%3Furl%3Dhttp%3A%2F%2Fsmartcdn.prod.postmedia.digital%2Fdriving%2Fwp-content%2Fuploads%2F2013%2F08%2F85428491.jpg%26w%3D580%26h%3D370&imgrefurl=https%3A%2F%2Fdriving.ca%2Ftoyota%2Ftacoma%2Freviews%2Froad-test%2Froad-test-2008-toyota-tacoma-2&tbnid=wEkshGvejDsKKM&vet=12ahUKEwj-ytXB9vnvAhXBDlMKHYImAzgQMygBegUIARDlAQ..i&docid=ZDXPJovseuFbHM&w=580&h=370&q=2008%20toyota%20tacoma&ved=2ahUKEwj-ytXB9vnvAhXBDlMKHYImAzgQMygBegUIARDlAQ",
                            IsAvailable = true,
                            IsOperational = true,
                            Make = "Toyota",
                            Model = "Tacoma",
                            Odometer = 101011,
                            Year = 2008
                        },
                        new
                        {
                            Id = 4,
                            CurrentCity = "San Diego",
                            CurrentState = "CA",
                            CurrentStreet = "9449 Friars Road",
                            CurrentZip = "92108",
                            Fuel = 100,
                            Image = "https://www.google.com/url?sa=i&url=http%3A%2F%2Fdavidsclassiccars.com%2Fchevrolet%2F24214-1979-chevrolet-blazer-k5-4x4-lifted-rebuilt-350-v8-auto-custom-deluxe-nt-bronco.html&psig=AOvVaw3kFOxYaZnHGAr3Tld8YuN6&ust=1618358915386000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCLjNxff2-e8CFQAAAAAdAAAAABAD",
                            IsAvailable = true,
                            IsOperational = true,
                            Make = "Chevrolet",
                            Model = "Bronco",
                            Odometer = 90011,
                            Year = 1979
                        },
                        new
                        {
                            Id = 5,
                            CurrentCity = "Chula Vista",
                            CurrentState = "CA",
                            CurrentStreet = "740 Hilltop Drive",
                            CurrentZip = "91910",
                            Fuel = 100,
                            Image = "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/2019-nissan-altima-102-1538074559.jpg?crop=0.822xw:1.00xh;0.138xw,0&resize=640:*",
                            IsAvailable = true,
                            IsOperational = true,
                            Make = "Nissan",
                            Model = "Altima",
                            Odometer = 90011,
                            Year = 2021
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "0f3f8561-ba6a-4530-b5e4-9e66425089de",
                            ConcurrencyStamp = "a0b6873b-bcfd-4880-8987-3a403620f5f2",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "ff2a00f0-9394-4ebf-9546-135cd2ab6500",
                            ConcurrencyStamp = "5d137252-b432-4cba-a5ec-6b1150addd1a",
                            Name = "Customer",
                            NormalizedName = "CUSTOMER"
                        },
                        new
                        {
                            Id = "135fc814-e4c8-4423-b037-6b28a1eb96d0",
                            ConcurrencyStamp = "3a9c7601-e621-457d-8f50-7572109c09a7",
                            Name = "Employee",
                            NormalizedName = "EMPLOYEE"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CarRentalService.Models.Customer", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "IdentityUser")
                        .WithMany()
                        .HasForeignKey("IdentityUserId");

                    b.Navigation("IdentityUser");
                });

            modelBuilder.Entity("CarRentalService.Models.Employee", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "IdentityUser")
                        .WithMany()
                        .HasForeignKey("IdentityUserId");

                    b.Navigation("IdentityUser");
                });

            modelBuilder.Entity("CarRentalService.Models.Issue", b =>
                {
                    b.HasOne("CarRentalService.Models.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("CarRentalService.Models.ServiceReceipt", b =>
                {
                    b.HasOne("CarRentalService.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarRentalService.Models.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("CarRentalService.Models.Trip", b =>
                {
                    b.HasOne("CarRentalService.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarRentalService.Models.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
