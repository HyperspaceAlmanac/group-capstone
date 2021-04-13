using CarRentalService.Data;
using CarRentalService.Models;
using CarRentalService.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarRentalService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private ApplicationDbContext _context;
        public TripController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<TripController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _context.Trips.FindAsync();
            return NotFound();
        }

        // GET api/<TripController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return NotFound();
        }

        // POST api/<TripController>
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return NotFound();
        }

        // PUT api/<TripController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return NotFound();
        }

        [HttpGet("DuringTrip/{id}")]
        public async Task<IActionResult> GetDuringTrip(int id)
        {
            try
            {
                var trip = await _context.Trips.Where(t => t.Id == id).SingleOrDefaultAsync();
                if (trip == null)
                {
                    return NotFound();
                }
                else
                {
                    DuringTripModel model = new DuringTripModel {Destination = "Street Address", Lat = trip.EndLat, Lng = trip.EndLng,
                        EstimatedCost = trip.Cost, EstimatedTime = 30};
                    return Ok(model);
                }
            } catch {
                return StatusCode(500);
            }
        }

        [HttpGet("ConfirmLocation/{id}")]
        public async Task<IActionResult> ConfirmLocation(int id)
        {
            try
            {
                var trip = await _context.Trips.Where(t => t.Id == id).SingleOrDefaultAsync();
                if (trip == null)
                {
                    return NotFound();
                }
                else
                {
                    trip.EndTime = DateTime.Now;
                    _context.Update(trip);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpPut("ConfirmLocation/{id}")]
        public async Task<IActionResult> ConfirmLocation(int id, [FromForm] string value)
        {
            return NotFound();
        }
        [HttpGet("GetStatus/{id}")]
        public async Task<IActionResult> GetStatus(int id)
        {
            return NotFound();
        }
        [HttpPut("SetStatus/{id}")]
        public async Task<IActionResult> SetStatus(int id, [FromForm] string value)
        {
            return NotFound();
        }
        [HttpGet("TakePictures/{id}")]
        public async Task<IActionResult> TakePictures(int id)
        {
            return NotFound();
        }
        [HttpPut("TakePictures/{id}")]
        public async Task<IActionResult> TakePictures(int id, [FromForm] string value)
        {
            return NotFound();
        }
        // PUT api/UpdateTrip/<TripController>/5
        [HttpGet("CompleteTrip/{id}")]
        public async Task<IActionResult> CompleteTrip(int id)
        {
            try
            {
                var trip = await _context.Trips.Where(t => t.Id == id).SingleOrDefaultAsync();
                if (trip == null)
                {
                    return NotFound();
                }
                else
                {
                    trip.EndTime = DateTime.Now;
                    _context.Update(trip);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }


    }
}
