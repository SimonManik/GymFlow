using GymFlow.Data;
using GymFlow.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;

namespace GymFlow.Controllers
{
    public class HomeController : Controller
    {
        // Database context variable
        private readonly ApplicationDbContext _context;

        // Constructor - injecting the database context
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // LINQ queries to calculate statistics
            ViewBag.TotalMembers = _context.Members.Count();
            ViewBag.ActiveMembers = _context.Members.Count(m => m.MembershipExpiry >= DateTime.Now);
            ViewBag.ExpiredMembers = _context.Members.Count(m => m.MembershipExpiry < DateTime.Now);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}