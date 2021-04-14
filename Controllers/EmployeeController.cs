using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRentalService.Data;
using CarRentalService.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using GoogleMapsApi.Entities.Geocoding.Response;
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi;
using CarRentalService.ViewModels;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace CarRentalService.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var employee = await _context.Employees.Where(c => c.IdentityUserId == userId).SingleOrDefaultAsync();
            if (!employee.CompletedRegistration)
            {
                return RedirectToAction(nameof(Edit));
            }
            else
            {
                var issue = await _context.ServiceReceipts.Where(sr => sr.EmployeeId == employee.Id && sr.EndTime == null).SingleOrDefaultAsync();
                if (issue == null)
                {
                   return RedirectToAction("SelectVehicle");
                }
                else
                {
                    return RedirectToAction(nameof(IssuePage));
                }
            }
            
        }
        public async Task<ActionResult> SelectVehicle()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var employee = _context.Employees.Where(c => c.IdentityUserId == userId).SingleOrDefault();
            var vehicles = _context.Vehicles.Where(v => v.IsAvailable == true).ToList();
            string employeeLocation = employee.CurrentLat.ToString() + ',' + employee.CurrentLong.ToString();

            //Save employee location a GeoCode request.

            for (var i = 0; i < vehicles.Count(); i++)
            {
                var geoCodingEngine = GoogleMaps.Geocode;
                GeocodingRequest geocodeRequest = new GeocodingRequest()
                {
                    Address = $"{vehicles[i].CurrentStreet}, {vehicles[i].CurrentCity}, {vehicles[i].CurrentState} {vehicles[i].CurrentZip}",
                    ApiKey = Secrets.GOOGLE_API_KEY,
                };
                GeocodingResponse geocode = await geoCodingEngine.QueryAsync(geocodeRequest);
                vehicles[i].LastKnownLatitude = geocode.Results.First().Geometry.Location.Latitude;
                vehicles[i].LastKnownLongitude = geocode.Results.First().Geometry.Location.Longitude;
                _context.Update(vehicles[i]);
                vehicles[i].Location = vehicles[i].LastKnownLatitude.ToString() + ',' + vehicles[i].LastKnownLongitude.ToString();

                string url = "https://maps.googleapis.com/maps/api/directions/json?origin=" + employeeLocation + "&destination=" + vehicles[i].Location + "&key=" + Secrets.GOOGLE_API_KEY;
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                string jsonResult = await response.Content.ReadAsStringAsync();
                JObject jobject = JObject.Parse(jsonResult);
                int distanceLength = jobject.SelectToken("routes[0].legs[0].distance.text").ToString().Length;
                double distance = Convert.ToDouble(jobject.SelectToken("routes[0].legs[0].distance.text").ToString().Substring(0, distanceLength - 3));
                vehicles[i].Distance = distance;

                int durationLength = jobject.SelectToken("routes[0].legs[0].duration.text").ToString().Length;
                double duration = Convert.ToDouble(jobject.SelectToken("routes[0].legs[0].duration.text").ToString().Substring(0, durationLength - 5));
                vehicles[i].Duration = duration;
            }
            await _context.SaveChangesAsync();
            List<Vehicle> vehiclesSorted = vehicles.OrderBy(v => v.Distance).ToList();
            return View(vehiclesSorted);
        }

        public async Task<ActionResult> StartService(int vehicleID, double lng, double lat)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var employee = await _context.Employees.Where(c => c.IdentityUserId == userId).SingleOrDefaultAsync();
            var vehicle = await _context.Vehicles.Where(v => v.Id == vehicleID).SingleOrDefaultAsync();

            ServiceReceipt serviceReceipt = PopulateTrip(employee, vehicle);
            
            // Assuming that vehicle always has one trip where it is placed.
            var prevTrip = await _context.Trips.Where(t => t.VehicleId == vehicle.Id).OrderBy(t => t.EndTime).LastOrDefaultAsync();
            if (prevTrip != null)
            {
                GetPreviousImageURLs(newTrip, prevTrip);
            }
            // Estimated cost
            newTrip.Cost = 10.50;
            newTrip.EndLat = lat;
            newTrip.EndLng = lng;
            // User will confirm car is in good condition (guaranteed to be)
            // User gets Twillo code
            return View(newTrip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StartService(ServiceReceipt serviceReceipt)
        {
            serviceReceipt.StartTime = DateTime.Now;
            serviceReceipt. = "DuringTrip";
            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private ServiceReceipt PopulateTrip(Models.Employee employee, Vehicle vehicle)
        {
            ServiceReceipt trip = new ()
            {
                CustomerId = customer.Id,
                Customer = customer,
                VehicleId = vehicle.Id,
                Vehicle = vehicle,
                StartLng = vehicle.LastKnownLongitude.Value,
                StartLat = vehicle.LastKnownLatitude.Value,
                OdometerStart = vehicle.Odometer,
                FuelStart = vehicle.Fuel
            };
            return trip;
        }

        public async Task<IActionResult> IssuePage()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var employee = await _context.Employees.Where(c => c.IdentityUserId == userId).SingleOrDefaultAsync();
            
            // Service Recept filled out. Start, end
            var issue = await _context.ServiceReceipts.Where(sr => sr.EmployeeId == employee.Id && sr.EndTime == null).SingleOrDefaultAsync();

            // Trip start values:
            // IsOperational = False.
            var tripValues = new TripViewModel { TripID = trip.Id, TripStatus = trip.TripStatus };

            return View(tripValues);
        }
        //------------------------------------CRUD Below ------------------------------------------------------------
        public async Task<IActionResult> Edit()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var employee = await _context.Employees.Where(c => c.IdentityUserId == userId).SingleOrDefaultAsync();
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Models.Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    GeocodingRequest geocodeRequest = new GeocodingRequest()
                    {
                        Address = $"{employee.CurrentStreet}, {employee.CurrentCity}, {employee.CurrentState} {employee.CurrentZip}",
                        ApiKey = Secrets.GOOGLE_API_KEY,
                        SigningKey = "Lew Vine"
                    };

                    var geoCodingEngine = GoogleMaps.Geocode;
                    GeocodingResponse geocode = await geoCodingEngine.QueryAsync(geocodeRequest);
                    employee.CurrentLat = geocode.Results.First().Geometry.Location.Latitude;
                    employee.CurrentLong = geocode.Results.First().Geometry.Location.Longitude;
                    employee.CompletedRegistration = true;
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", employee.IdentityUserId);
            return View(employee);
        }

        public async Task<IActionResult> RegisterAccount(Models.Employee employee)
        {
            _context.Add(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", employee.IdentityUserId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(c => c.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}