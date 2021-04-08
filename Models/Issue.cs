using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalService.Models
{
    public class Issue
    {
        [Key]
        public int Id { get; set; }
        public string ServiceNeeded { get; set; }
        public DateTime TimeReported { get; set; }
        public bool Resolved { get; set; }
        [ForeignKey(nameof(Vehicle))]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

    }
}
