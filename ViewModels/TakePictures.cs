using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalService.ViewModels
{
    public class TakePictures
    {
        public string BeforeTripFrontImage { get; set; }
        public string BeforeTripBackImage { get; set; }
        public string BeforeTripLeftImage { get; set; }
        public string BeforeTripRightImage { get; set; }
        public string BeforeTripInteriorFront { get; set; }
        public string BeforeTripInteriorBack { get; set; }

        public string AfterTripFrontImage { get; set; }
        public string AfterTripBackImage { get; set; }
        public string AfterTripLeftImage { get; set; }
        public string AfterTripRightImage { get; set; }
        public string AfterTripInteriorFront { get; set; }
        public string AfterTripInteriorBack { get; set; }
    }
}
