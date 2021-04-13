using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalService.ViewModels
{
    public class DuringTripModel
    {
        public double Lng { get; set; }
        public double Lat { get; set; }
        public string Destination { get; set; }
        public double EstimatedCost { get; set; }
        public int EstimatedTime { get; set; }
    }
}
