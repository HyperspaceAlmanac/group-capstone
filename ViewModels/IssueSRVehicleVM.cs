using CarRentalService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalService.ViewModels
{
    public class IssueSRVehicleVM
    {
        public Issue Issue { get; set; }
        public Employee Employee { get; set; }
        public Vehicle Vehicle { get; set; }
        public ServiceReceipt ServiceReceipt { get; set; }
        
    }
}
