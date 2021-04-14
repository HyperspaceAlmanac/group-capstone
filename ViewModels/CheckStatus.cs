using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalService.ViewModels
{
    public class CheckStatus
    {
        public int Fuel { get; set; }
        public int Odometer { get; set; }
        public bool Tire { get; set; }
        public bool BodyRepair { get; set; }
        public bool InteriorCleaning { get; set; }
        public bool WindowsRepair { get; set; }
        public bool DashboardLights { get; set; }
        public bool ElectronicsRepair { get; set; }
        public bool Supplies { get; set; }

    }
}
