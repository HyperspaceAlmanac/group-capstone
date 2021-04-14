using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalService.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CurrentStreet { get; set; }
        public string CurrentCity { get; set; }
        public string CurrentState { get; set; }
        public string CurrentZip { get; set; }
        public double CurrentLat { get; set; }
        public double CurrentLong { get; set; }
        public string PhoneNumber { get; set; }
        public string DriverLicenseNumber { get; set; }
        public int TotalBalance { get; set; }
        public bool CompletedRegistration { get; set; }
        public string? DestinationStreet { get; set; }
        public string? DestinationCity { get; set; }
        public string? DestinationState { get; set; }
        public string? DestinationZip { get; set; }

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
