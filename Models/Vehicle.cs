using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalService.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }

        public int Year { get; set; }

        public string Image { get; set; }
        public int Fuel { get; set; }
        public int Odometer { get; set; }
        public double? LastKnownLatitude { get; set; }
        public double? LastKnownLongitude { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsOperational { get; set; }
        public string CurrentStreet { get; set; }
        public string CurrentCity { get; set; }
        public string CurrentState { get; set; }
        public string CurrentZip { get; set; }
        public string? Location { get; set; }
        public double? Distance { get; set; }
        public double? Duration { get; set; }

    }
}
