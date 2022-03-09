using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PuppyStoreFinal.Data;
using PuppyStoreFinal.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace PuppyStoreFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailProvider _emailProvider;

        public HomeController(ApplicationDbContext context, IEmailProvider emailProvider)
        {
            _context = context;
            _emailProvider = emailProvider;
        }

        public async Task<IActionResult> Index()
        {
            List<Event> events = await ApplicationDb.GetEventsAsync(_context);

            return View(events);
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

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(EmailProviderSendgrid e)
        {
            if (ModelState.IsValid)
            {
                await _emailProvider.SendEmailAsync(null, e);
                return RedirectToAction("Index");  
            }

            return View();
        }

    }
}
