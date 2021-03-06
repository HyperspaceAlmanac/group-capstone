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
using GoogleMapsApi;
using GoogleMapsApi.Entities.Geocoding.Response;
using GoogleMapsApi.Entities.Geocoding.Request;
using Stripe;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using CarRentalService.ViewModels;

namespace CarRentalService.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly string _userImagesDirectory = "wwwroot\\Images";
        private List<string> _photoTypes;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
            System.IO.Directory.CreateDirectory(_userImagesDirectory);
            System.IO.Directory.CreateDirectory(_userImagesDirectory + "\\Before");
            System.IO.Directory.CreateDirectory(_userImagesDirectory + "\\After");
            _photoTypes = new List<string>() { "front", "back", "left", "right", "interiorFront", "interiorBack" };
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = await _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefaultAsync();
            if (!customer.CompletedRegistration)
            {
                return RedirectToAction(nameof(Edit));
            }
            else
            {
                var trip = await _context.Trips.Where(trip => trip.CustomerId == customer.Id && trip.EndTime == null).SingleOrDefaultAsync();
                if (trip == null)
                {
                    //return RedirectToAction("SelectVehicle");
                    return RedirectToAction("EstimatedDestination");
                }
                else
                {
                    return RedirectToAction(nameof(TripPage));
                }
            }
        }

        //GET
        public async Task<ActionResult> EstimatedDestination()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = await _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefaultAsync();
            _context.SaveChanges();
            return View(customer);
        }

        //POST
        [HttpPost]
        public async Task<ActionResult> EstimatedDestination(Models.Customer customer)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var customerInDB = await _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefaultAsync();
            customerInDB.DestinationCity = customer.DestinationCity;
            customerInDB.DestinationStreet = customer.DestinationStreet;
            customerInDB.DestinationState = customer.DestinationState;
            customerInDB.DestinationZip = customer.DestinationZip;
            _context.SaveChanges();
            return RedirectToAction("SelectVehicle");
        }

        public async Task<ActionResult> SelectVehicle(string sortOrder, string searchString)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = await _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefaultAsync();

            var availableVehicles = await _context.Vehicles.Where(v => v.IsAvailable == true && v.IsOperational).ToListAsync();
            List<Vehicle> vehicles = new List<Vehicle>();
            Issue issue;
            foreach (Vehicle v in availableVehicles)
            {

                issue = await _context.Issues.Where(i => i.VehicleId == v.Id && i.Resolved == false).FirstOrDefaultAsync();
                if (issue == null)
                {
                    vehicles.Add(v);
                }
                else
                {
                    v.IsOperational = false;
                    _context.Update(v);
                }
            }
            string custLocation = customer.CurrentLat.ToString() + ',' + customer.CurrentLong.ToString();

            //Save customer location a GeoCode request. 

            for (var i = 0; i < vehicles.Count; i++)
            {
                var geoCodingEngine = GoogleMaps.Geocode;
                GeocodingRequest geocodeRequest = new ()
                {
                    Address = $"{vehicles[i].CurrentStreet}, {vehicles[i].CurrentCity}, {vehicles[i].CurrentState} {vehicles[i].CurrentZip}",
                    ApiKey = Secrets.GOOGLE_API_KEY,
                };
                GeocodingResponse geocode = await geoCodingEngine.QueryAsync(geocodeRequest);
                vehicles[i].LastKnownLatitude = geocode.Results.First().Geometry.Location.Latitude;
                vehicles[i].LastKnownLongitude = geocode.Results.First().Geometry.Location.Longitude;
                _context.Update(vehicles[i]);
                vehicles[i].Location = vehicles[i].LastKnownLatitude.ToString() + ',' + vehicles[i].LastKnownLongitude.ToString();

                string url = "https://maps.googleapis.com/maps/api/directions/json?origin=" + custLocation + "&destination=" + vehicles[i].Location + "&key=" + Secrets.GOOGLE_API_KEY;
                HttpClient client = new();
                HttpResponseMessage response = await client.GetAsync(url);
                string jsonResult = await response.Content.ReadAsStringAsync();
                JObject jobject = JObject.Parse(jsonResult);
                int distanceLength = jobject.SelectToken("routes[0].legs[0].distance.text").ToString().Length;
                double distance = Convert.ToDouble(jobject.SelectToken("routes[0].legs[0].distance.text").ToString().Substring(0, distanceLength - 3));
                vehicles[i].Distance = distance;
            }

            await _context.SaveChangesAsync();

            if (!String.IsNullOrEmpty(searchString)){
                string sSU = searchString.ToUpper();
                vehicles = await _context.Vehicles.Where(v => 
                                                v.Make.ToUpper().Contains(sSU) || 
                                                v.Model.ToUpper().Contains(sSU) ||
                                                v.Year.ToString().Contains(sSU)
                                            ).ToListAsync();
            }
            else
            {
                switch (sortOrder)
                {
                    case "make_descending":
                        vehicles = await _context.Vehicles.Where(v => v.IsAvailable && v.IsOperational).OrderByDescending(v => v.Make).ToListAsync();
                        ViewBag.MakeSortParam = "make_descending";
                        break;
                    case "make_ascending":
                        vehicles = await _context.Vehicles.Where(v => v.IsAvailable && v.IsOperational).OrderBy(v => v.Make).ToListAsync();
                        ViewBag.MakeSortParam = "make_ascending";
                        break;
                    case "model_descending":
                        vehicles = await _context.Vehicles.Where(v => v.IsAvailable && v.IsOperational).OrderByDescending(v => v.Model).ToListAsync();
                        ViewBag.ModelSortParam = "model_descending";
                        break;
                    case "model_ascending":
                        vehicles = await _context.Vehicles.Where(v => v.IsAvailable && v.IsOperational).OrderBy(v => v.Model).ToListAsync();
                        ViewBag.ModelSortParam = "model_ascending";
                        break;
                    case "year_descending":
                        vehicles = await _context.Vehicles.Where(v => v.IsAvailable && v.IsOperational).OrderByDescending(v => v.Year).ToListAsync();
                        ViewBag.YearSortParam = "year_descending";
                        break;
                    case "year_ascending":
                        vehicles = await _context.Vehicles.Where(v => v.IsAvailable && v.IsOperational).OrderBy(v => v.Year).ToListAsync();
                        ViewBag.YearSortParam = "year_ascending";
                        break;
                    case "distance_descending":
                        vehicles = await _context.Vehicles.Where(v => v.IsAvailable && v.IsOperational).OrderByDescending(v => v.Distance).ToListAsync();
                        ViewBag.DistanceSortParam = "distance_descending";
                        break;
                    case "distance_ascending":
                        vehicles = await _context.Vehicles.Where(v => v.IsAvailable && v.IsOperational).OrderBy(v => v.Distance).ToListAsync();
                        ViewBag.DistanceSortParam = "distance_ascending";
                        break;
                    default:
                        vehicles = await _context.Vehicles.Where(v => v.IsAvailable && v.IsOperational).OrderByDescending(v => v.Distance).ToListAsync();
                        break;
                }
            }
            //List<Vehicle> vehiclesSorted = vehicles.OrderBy(v => v.Distance).ToList();
            return View(vehicles);
        }
        // GET: Vehicles/Details/5
        public async Task<ActionResult> VehicleDetails(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = await _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefaultAsync();
            var vehicle = await _context.Vehicles.Where(v => v.Id == id).SingleOrDefaultAsync();

            var geoCodingEngine = GoogleMaps.Geocode;
            GeocodingRequest geocodeRequest = new ()
            {
                Address = $"{customer.DestinationStreet}, {customer.DestinationCity}, {customer.DestinationState} {customer.DestinationZip}",
                ApiKey = Secrets.GOOGLE_API_KEY,
            };
            GeocodingResponse geocode = await geoCodingEngine.QueryAsync(geocodeRequest);
            double lat = geocode.Results.First().Geometry.Location.Latitude;
            double lng = geocode.Results.First().Geometry.Location.Longitude;
            var destinationLocation = lat.ToString() + ',' + lng.ToString();


            string url = "https://maps.googleapis.com/maps/api/directions/json?origin=" + vehicle.Location + "&destination=" + destinationLocation + "&key=" + Secrets.GOOGLE_API_KEY;
            HttpClient client = new ();
            HttpResponseMessage response = await client.GetAsync(url);
            string jsonResult = await response.Content.ReadAsStringAsync();
            JObject jobject = JObject.Parse(jsonResult);
            int distanceLength = jobject.SelectToken("routes[0].legs[0].distance.text").ToString().Length;
            double distance = Convert.ToDouble(jobject.SelectToken("routes[0].legs[0].distance.text").ToString().Substring(0, distanceLength - 3));

            @ViewBag.Distance = distance.ToString() + "miles";
            @ViewBag.MapUrl = $"https://www.google.com/maps/embed/v1/directions?key=" + Secrets.GOOGLE_API_KEY + "&origin=" + vehicle.Location + "&destination=" + destinationLocation;
            @ViewBag.Duration = jobject.SelectToken("routes[0].legs[0].duration.text").ToString();
            var cost = 49.99 + (distance - 100) * .25;
            @ViewBag.Cost = Math.Round(cost, 2);
            ViewBag.Lat = lat;
            ViewBag.Lng = lng;

            return View(vehicle);

        }

        //POST
        [HttpPost]
        public async Task<ActionResult> VehicleDetails(Vehicle vehicle)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = await _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefaultAsync();
            return View(vehicle);
        }

        public async Task<ActionResult> CreateTrip(int vehicleID, double lng, double lat, double cost)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = await _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefaultAsync();
            var vehicle = await _context.Vehicles.Where(v => v.Id == vehicleID).SingleOrDefaultAsync();

            Trip newTrip = PopulateTrip(customer, vehicle);
            // Assuming that vehicle always has one trip where it is placed.

            GetPreviousImageURLs(newTrip, vehicle.Id);

            // Estimated cost
            newTrip.Cost = cost;
            newTrip.EndLat = lat;
            newTrip.EndLng = lng;
            // User will confirm car is in good condition (guaranteed to be)
            // User gets Twillo code
            return View(newTrip);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTrip(Trip trip)
        {
            trip.StartTime = DateTime.Now;
            trip.TripStatus = "DuringTrip";

            var vehicle = await _context.Vehicles.Where(v => v.Id == trip.VehicleId).SingleOrDefaultAsync();
            vehicle.IsAvailable = false;
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();

            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private Trip PopulateTrip(Models.Customer customer, Vehicle vehicle)
        {
            Trip trip = new() {CustomerId = customer.Id, Customer = customer, VehicleId = vehicle.Id, Vehicle = vehicle, StartLng = vehicle.LastKnownLongitude.Value,
                StartLat = vehicle.LastKnownLatitude.Value, OdometerStart = vehicle.Odometer, FuelStart = vehicle.Fuel};
            return trip;
        }

        private void GetPreviousImageURLs(Trip current, int id)
        {
            if (System.IO.File.Exists($"{_userImagesDirectory}\\Before\\front.{id}.png"))
            {
                current.BeforeTripFrontImage = "front." + id + ".png";
            } else {
                current.BeforeTripFrontImage = "FrontDefault.png";
            }
            if (System.IO.File.Exists($"{_userImagesDirectory}\\Before\\back.{id}.png"))
            {
                current.BeforeTripBackImage = "back." + id + ".png";
            }
            else
            {
                current.BeforeTripBackImage = "BackDefault.png";
            }
            if (System.IO.File.Exists($"{_userImagesDirectory}\\Before\\left.{id}.png"))
            {
                current.BeforeTripLeftImage = "left." + id + ".png";
            }
            else
            {
                current.BeforeTripLeftImage = "LeftDefault.png";
            }
            if (System.IO.File.Exists($"{_userImagesDirectory}\\Before\\right.{id}.png"))
            {
                current.BeforeTripRightImage = "right." + id + ".png";
            }
            else
            {
                current.BeforeTripRightImage = "RightDefault.png";
            }
            if (System.IO.File.Exists($"{_userImagesDirectory}\\Before\\interiorFront.{id}.png"))
            {
                current.BeforeTripInteriorFront = "interiorFront." + id + ".png";
            }
            else
            {
                current.BeforeTripInteriorFront = "InteriorFrontDefault.png";
            }
            if (System.IO.File.Exists($"{_userImagesDirectory}\\Before\\interiorBack.{id}.png"))
            {
                current.BeforeTripInteriorBack = "interiorBack." + id + ".png";
            }
            else
            {
                current.BeforeTripInteriorBack = "InteriorBackDefault.png";
            }
            
            current.AfterTripFrontImage = "";
            current.AfterTripBackImage = "";
            current.AfterTripLeftImage = "";
            current.AfterTripRightImage = "";
            current.AfterTripInteriorFront = "";
            current.AfterTripInteriorBack = "";
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> TripPage()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = await _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefaultAsync();
            // Trip filled out. Start, end
            var trip = await _context.Trips.Where(trip => trip.CustomerId == customer.Id && trip.EndTime == null).SingleOrDefaultAsync();

            // Trip start values:
            // IsOperational = False.
            var tripValues = new TripViewModel {TripID = trip.Id, VehicleID = trip.VehicleId, TripStatus = trip.TripStatus, Odometer = trip.OdometerStart};

            return View(tripValues);
            
        }



        // GET: Customers/Details/5
        public async Task<IActionResult> Details()
        {
            var userId =  this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = await _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefaultAsync();
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = await _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefaultAsync();
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Models.Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    GeocodingRequest geocodeRequest = new ()
                    {
                        Address = $"{customer.CurrentStreet}, {customer.CurrentCity}, {customer.CurrentState} {customer.CurrentZip}",
                        ApiKey = Secrets.GOOGLE_API_KEY,
                        SigningKey = "Lew Vine"
                    };

                    var geoCodingEngine = GoogleMaps.Geocode;
                    GeocodingResponse geocode = await geoCodingEngine.QueryAsync(geocodeRequest);
                    customer.CurrentLat = geocode.Results.First().Geometry.Location.Latitude;
                    customer.CurrentLong = geocode.Results.First().Geometry.Location.Longitude;
                    customer.CompletedRegistration = true;
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", customer.IdentityUserId);
            return View(customer);
        }

        public async Task<IActionResult> RegisterAccount(Models.Customer customer)
        {
            _context.Add(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", customer.IdentityUserId);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }

        public async Task<IActionResult> TotalBill()
        {
            ViewBag.StripePublishKey = Secrets.STRIPES_PUBLIC_KEY;
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefault();

            List<Trip> customerTrips = await _context.Trips.Where(t => t.CustomerId.Equals(customer.Id)).ToListAsync();
            return View(customerTrips);
        }

        [HttpPost]
        public async Task<IActionResult> Charge(string stripeToken, string stripeEmail, int amount, string description, int tripId)
        {
            StripeConfiguration.ApiKey = Secrets.STRIPES_API_KEY;
            var myCharge = new ChargeCreateOptions
            {
                Amount = amount,
                Currency = "USD",
                ReceiptEmail = stripeEmail,
                Description = description,
                Source = stripeToken,
                Capture = true
            };
            var chargeService = new ChargeService();
            try
            {
                Charge stripeCharge = chargeService.Create(myCharge);
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var customer = await _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefaultAsync();

                if (tripId.Equals(-1))
                {
                    List<Trip> allTrips = await _context.Trips.Where(t => t.CustomerId.Equals(customer.Id)).ToListAsync();
                    foreach (var trip in allTrips)
                    {
                        trip.IsPaid = true;
                        _context.Update(trip);
                    }
                }
                else
                {
                    Trip tripPaid = await _context.Trips.Where(t => t.Id.Equals(tripId)).FirstOrDefaultAsync();
                    tripPaid.IsPaid = true;
                    _context.Update(tripPaid);
                }
                await _context.SaveChangesAsync();
                double amountPaid = Convert.ToDouble(myCharge.Amount);
                ViewBag.PaymentInfo = myCharge;
                ViewBag.PaymentTotal = Math.Round(amountPaid/100, 2);
                return View(true);
            }
            catch (Exception exceptionThrown)
            {
                ViewBag.Exception = exceptionThrown;
                return View(false);
            }
        }
    }
}
