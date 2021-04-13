using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalService.Models
{
    public class Trip
    {
        [Key]
        public int Id { get; set; }
        public double StartLng { get; set; }
        public double StartLat { get; set; }
        public double EndLng { get; set; }
        public double EndLat { get; set; }
        public int OdometerStart { get; set; }
        public int OdometerEnd { get; set; }
        public int FuelStart { get; set; }
        public int FuelEnd { get; set; }
        public double Cost { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string TripStatus { get; set; }
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

        [ForeignKey(nameof(Vehicle))]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
