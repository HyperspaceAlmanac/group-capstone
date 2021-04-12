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
using CarRentalService.ViewModels;

namespace CarRentalService.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
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
                    return RedirectToAction(nameof(SelectVehicle));
                }
                else
                {
                    return RedirectToAction(nameof(TripPage));
                }
            }
        }
        public async Task<ActionResult> SelectVehicle()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = await _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefaultAsync();
            // Display map,list of vehicles.
            // Select a vehicle
            // Select a End location to create a trip
            List<Vehicle> vehicles = _context.Vehicles.ToList();
            return View(vehicles);
        }

        public async Task<ActionResult> CreateTrip(int vehicleID, double lng, double lat)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = await _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefaultAsync();
            var vehicle = await _context.Vehicles.Where(v => v.Id == vehicleID).SingleOrDefaultAsync();

            Trip newTrip = PopulateTrip(customer, vehicle);
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
        public async Task<IActionResult> CreateTrip(Trip trip)
        {
            trip.StartTime = DateTime.Now;
            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private Trip PopulateTrip(Customer customer, Vehicle vehicle)
        {
            Trip trip = new Trip {CustomerId = customer.Id, Customer = customer, VehicleId = vehicle.Id, Vehicle = vehicle, StartLng = vehicle.LastKnownLongitude.Value,
                StartLat = vehicle.LastKnownLatitude.Value, OdometerStart = vehicle.Odometer, FuelStart = vehicle.Fuel};
            return trip;
        }

        private void GetPreviousImageURLs(Trip current, Trip prev)
        {
            current.BeforeTripFrontImage = prev.AfterTripFrontImage;
            current.BeforeTripBackImage = prev.AfterTripBackImage;
            current.BeforeTripLeftImage = prev.AfterTripLeftImage;
            current.BeforeTripRightImage = prev.AfterTripRightImage;
            current.BeforeTripInteriorFront = prev.AfterTripInteriorFront;
            current.BeforeTripInteriorBack = prev.AfterTripInteriorBack;
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
            var vehicle = await _context.Vehicles.Where(v => v.Id == trip.VehicleId).SingleOrDefaultAsync();

            // Trip start values:
            // IsOperational = False. 

            return View();
            
        }

        public bool PhotosUploaded(Vehicle vehicle)
        {
            return false;
        }

        // GET


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
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Address,PhoneNumber,DriverLicenseNumber,CompletedRegistration,TotalBalance,IdentityUserId")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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

        public async Task<IActionResult> RegisterAccount(Customer customer)
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
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Address,PhoneNumber,DriverLicenseNumber,CompletedRegistration,TotalBalance,IdentityUserId")] Customer customer)
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
    }
}
