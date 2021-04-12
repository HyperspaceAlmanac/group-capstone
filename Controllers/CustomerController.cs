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
using System.Configuration;
using Stripe;

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
            var customer = _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefault();
            if (!customer.CompletedRegistration)
            {
                return RedirectToAction(nameof(Edit));
            }
            else
            {
                var trip = await _context.Trips.Where(trip => trip.CustomerId == customer.Id && trip.EndTime == null).SingleOrDefaultAsync();
                if (trip == null)
                {
                    RedirectToAction(nameof(SelectVehicle));
                }
                else
                {
                    RedirectToAction(nameof(TripPage));
                }
            }
            var applicationDbContext = _context.Customers.Include(c => c.IdentityUser);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<ActionResult> SelectVehicle()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = await _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefaultAsync();
            return View();
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> TripPage()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = await _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefaultAsync();

            return View();
            
        }

        // GET


        // GET: Customers/Details/5
        public async Task<IActionResult> Details()
        {
            var userId =  this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = await _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefaultAsync();
            return View(customer);
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
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Address,PhoneNumber,DriverLicenseNumber,CompletedRegistration,TotalBalance,IdentityUserId")] Models.Customer customer)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Address,PhoneNumber,DriverLicenseNumber,CompletedRegistration,TotalBalance,IdentityUserId")] Models.Customer customer)
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

        public async Task<IActionResult> RegisterAccount(Models.Customer customer)
        {
            _context.Add(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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

        public ActionResult TotalBill()
        {
            ViewBag.StripePublishKey = Secrets.STRIPES_PUBLIC_KEY;
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefault();
            List<Trip> customerTrips = _context.Trips.Where(t => t.CustomerId.Equals(customer.Id)).ToList();
            return View(customerTrips);
        }

        [HttpPost]
        public ActionResult Charge(string stripeToken, string stripeEmail, int amount, string description, int tripId)
        {
            StripeConfiguration.ApiKey = Secrets.STRIPES_API_KEY;
            var myCharge = new ChargeCreateOptions();
            myCharge.Amount = amount;
            myCharge.Currency = "USD";
            myCharge.ReceiptEmail = stripeEmail;
            myCharge.Description = description;
            myCharge.Source = stripeToken;
            myCharge.Capture = true;
            var chargeService = new ChargeService();
            Charge stripeCharge = chargeService.Create(myCharge);
            
            Trip tripPaid = _context.Trips.Where(t => t.Id.Equals(tripId)).FirstOrDefault();
            tripPaid.IsPaid = true;
            _context.Update(tripPaid);
            _context.SaveChanges();

            return View(stripeCharge);
        }
    }
}
