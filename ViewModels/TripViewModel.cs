using CarRentalService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalService.ViewModels
{
    public class TripViewModel
    {
        public Customer Customer { get; set; }
        public Trip Trip { get; set; }
        public bool TripStarted { get; set; }
    }
}
