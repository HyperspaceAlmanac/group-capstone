using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalService.ViewModels
{
    public class ConfirmLocation
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zipcode { get; set; }
    }
}
