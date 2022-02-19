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

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            dynamic mymodel = new ExpandoObject();
            mymodel.Puppies = await ApplicationDb.GetPuppiesAsync(_context);
            mymodel.Events = await ApplicationDb.GetEventsAsync(_context);

            return View(mymodel);
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
