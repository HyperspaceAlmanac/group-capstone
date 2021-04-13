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

    }