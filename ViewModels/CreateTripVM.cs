using CarRentalService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalService.ViewModels
{
    public class CreateTripVM
    {
        public Customer Customer { get; set; }
        public Vehicle Vehicle { get; set; }
        public double[] EndCoordinates { get; set; }
        public string[] ImageURLs { get; set; }
    }
}
