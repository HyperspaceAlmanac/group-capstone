using CarRentalService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalService.ViewModels
{
    public class TripViewModel
    {
        public int TripID { get; set; }
        public int VehicleID { get; set; }
        public string TripStatus { get; set; }
        public int Odometer { get; set; }
    }
}
