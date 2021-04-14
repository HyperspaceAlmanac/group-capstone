using CarRentalService.Data;
using CarRentalService.Models;
using CarRentalService.TwilioSend;
using CarRentalService.ViewModels;
using Microsoft.AspNetCore.Http;
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
        private readonly string _userImagesDirectory = "wwwroot\\Images";
        private List<string> _photoTypes;
        public TripController(ApplicationDbContext context)
        {
            _context = context;
            System.IO.Directory.CreateDirectory(_userImagesDirectory);
            System.IO.Directory.CreateDirectory(_userImagesDirectory + "\\Before");
            System.IO.Directory.CreateDirectory(_userImagesDirectory + "\\After");
            _photoTypes = new List<string>() { "front", "back", "left", "right", "interiorFront", "interiorBack" };
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

        private double[] GetGeoCode(ConfirmLocation location)
        {
            // hard code for now, API calls will eventually be service
            return new double[] { 34.0430058, -118.2673597 };
        }
        private ConfirmLocation ConvertGeoCode(double lng, double lat)
        {
            return new ConfirmLocation { Street = "1234 Street", Lat = lat, Lng = lng, City = "San Diego", State = "CA", Zipcode = 12345 };
        }

        private void FillPhotos(Trip trip, TakePhotos photos)
        {
            photos.BeforeTripFrontImage = trip.BeforeTripFrontImage ?? "";
            photos.BeforeTripBackImage = trip.BeforeTripBackImage ?? "";
            photos.BeforeTripLeftImage = trip.BeforeTripLeftImage ?? "";
            photos.BeforeTripRightImage = trip.BeforeTripRightImage ?? "";
            photos.BeforeTripInteriorBack = trip.BeforeTripInteriorBack ?? "";
            photos.BeforeTripInteriorFront = trip.BeforeTripInteriorFront ?? "";

            photos.Done = trip.AfterTripFrontImage == "" || trip.AfterTripBackImage == "" || trip.AfterTripLeftImage == ""
                || trip.AfterTripRightImage == "" || trip.AfterTripInteriorFront == "" || trip.AfterTripInteriorBack == "";
            photos.AfterTripFrontImage = trip.AfterTripFrontImage ?? "";
            photos.AfterTripBackImage = trip.AfterTripBackImage ?? "";
            photos.AfterTripLeftImage = trip.AfterTripLeftImage ?? "";
            photos.AfterTripRightImage = trip.AfterTripRightImage ?? "";
            photos.AfterTripInteriorFront = trip.AfterTripInteriorFront ?? "";
            photos.AfterTripInteriorBack = trip.AfterTripInteriorBack ?? "";
        }

        [HttpGet("Twilio/{id}")]
        public async Task<IActionResult> SendTwilioCode(int id)
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
                    var vehicle = await _context.Vehicles.Where(v => v.Id == trip.VehicleId).SingleOrDefaultAsync();
                    TwilioText.SendTextToDriver(Secrets.MY_PHONE_NUMBER, vehicle.DoorKey);
                    return Ok();
                }
            }
            catch
            {
                return StatusCode(500);
            }
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
                    if (trip.TripStatus == "DuringTrip")
                    {
                        trip.TripStatus = "ConfirmLocation";
                        _context.Update(trip);
                        await _context.SaveChangesAsync();
                    }
                    ConfirmLocation location = ConvertGeoCode(trip.EndLng, trip.EndLat);
                    return Ok(location);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpPut("ConfirmLocation/{id}")]
        public async Task<IActionResult> ConfirmLocation(int id, [FromBody] ConfirmLocation location)
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
                    double[] endDestination = GetGeoCode(location);
                    trip.EndLat = endDestination[0];
                    trip.EndLng = endDestination[1];
                    trip.TripStatus = "CheckStatus";
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
        private async Task<bool> ReportIssues(CheckStatus status, int vehicleId)
        {
            DateTime currentTime = DateTime.Now;
            bool update = false;
            Issue issue;
            // Create issues to add into issues table
            if (status.Tire)
            {
                issue = new Issue() { ServiceNeeded = "Tire", VehicleId = vehicleId, TimeReported = currentTime};
                await _context.AddAsync(issue);
                update = true;
            }
            if (status.BodyRepair)
            {
                issue = new Issue() { ServiceNeeded = "BodyRepair", VehicleId = vehicleId, TimeReported = currentTime };
                await _context.AddAsync(issue);
                update = true;
            }
            if (status.InteriorCleaning)
            {
                issue = new Issue() { ServiceNeeded = "InteriorCleaning", VehicleId = vehicleId, TimeReported = currentTime };
                await _context.AddAsync(issue);
                update = true;
            }
            if (status.WindowsRepair)
            {
                issue = new Issue() { ServiceNeeded = "Windows", VehicleId = vehicleId, TimeReported = currentTime };
                await _context.AddAsync(issue);
                update = true;
            }
            if (status.DashboardLights)
            {
                issue = new Issue() { ServiceNeeded = "DashboardLights", VehicleId = vehicleId, TimeReported = currentTime };
                await _context.AddAsync(issue);
                update = true;
            }
            if (status.ElectronicsRepair)
            {
                issue = new Issue() { ServiceNeeded = "Electronics", VehicleId = vehicleId, TimeReported = currentTime };
                await _context.AddAsync(issue);
                update = true;
            }
            if (status.Supplies)
            {
                issue = new Issue() { ServiceNeeded = "Supplies", VehicleId = vehicleId, TimeReported = currentTime };
                await _context.AddAsync(issue);
                update = true;
            }
            if (update)
            {
                await _context.SaveChangesAsync();
            }
            return update;
        }

        [HttpPut("CheckStatus/{id}")]
        public async Task<IActionResult> SetStatus(int id, [FromBody] CheckStatus status)
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
                    trip.FuelEnd = status.Fuel;
                    trip.OdometerEnd = status.Odometer;
                    await ReportIssues(status, trip.VehicleId);
                    _context.Update(trip);
                    trip.TripStatus = "TakePhotos";
                    await _context.SaveChangesAsync();
                    return Ok();
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpGet("TakePhotos/{id}")]
        public async Task<IActionResult> TakePhotos(int id)
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
                    TakePhotos photos = new TakePhotos();
                    FillPhotos(trip, photos);
                    return Ok(photos);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpPut("TakePhotos/{imageType}/{id}")]
        public async Task<IActionResult> TakePhotos(string imageType, int id, [FromForm] IFormFile file)
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
                    var fileName = $"{imageType}.{trip.VehicleId}.png";
                    await SaveImage(trip, fileName, file, imageType);


                    _context.SaveChanges();
                    return StatusCode(200);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        private async Task<bool> SaveImage(Trip trip, string fileName, IFormFile file, string imgType) {
            using (var stream = System.IO.File.Create(_userImagesDirectory + "\\after\\" + fileName))
            {
                await file.CopyToAsync(stream);
                stream.Close();
            }
            switch (imgType)
            {
                case "front":
                    trip.AfterTripFrontImage = fileName;
                    break;
                case "back":
                    trip.AfterTripBackImage = fileName;
                    break;
                case "left":
                    trip.AfterTripLeftImage = fileName;
                    break;
                case "right":
                    trip.AfterTripRightImage = fileName;
                    break;
                case "interiorFront":
                    trip.AfterTripInteriorFront = fileName;
                    break;
                case "interiorBack":
                    trip.AfterTripInteriorBack = fileName;
                    break;
                default:
                    break;
            }
            return true;
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
                    trip.TripStatus = "Completed";
                    trip.EndTime = DateTime.Now;
                    ShiftPhotos(trip);
                    _context.Update(trip);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
            }
            catch (System.IO.IOException)
            {
                return StatusCode(500);
            }
        }
        private void ShiftPhotos(Trip trip)
        {
            trip.AfterTripFrontImage = "";
            trip.AfterTripBackImage = "";
            trip.AfterTripLeftImage = "";
            trip.AfterTripRightImage = "";
            trip.AfterTripInteriorFront = "";
            trip.AfterTripInteriorBack = "";
            string source;
            string destination;
            int id = trip.VehicleId;
            foreach (string s in _photoTypes)
            {
                source = _userImagesDirectory + $"\\After\\{s}.{id}.png";
                destination = _userImagesDirectory + $"\\Before\\{s}.{id}.png";
                System.IO.File.Delete(destination);
                System.IO.File.Move(source, destination);
            }
        }
    }
}
