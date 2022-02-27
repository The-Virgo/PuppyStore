using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogBreedApi;

namespace PuppyStoreFinal.Controllers
{
    public class BreedController : Controller
    {

        private readonly IConfiguration _config;

        public BreedController(IConfiguration config)
        {
            _config = config;
        }
        public async Task<IActionResult> Index()
        {
            List<Root> breeds = await DogBreed.GetBreedsAsync(_config.GetSection("x-api-key").Value);
            return View(breeds);
        }

        public async Task<ActionResult> Details(string id)
        {
            List<Root> breeds = await DogBreed.GetBreedsAsync(_config.GetSection("x-api-key").Value);
            ViewData["BreedId"] = id;
            return View(breeds);
        }

    }
}
