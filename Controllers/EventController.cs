using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuppyStoreFinal.Data;
using PuppyStoreFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PuppyStoreFinal.Controllers
{
    [Authorize(Roles = "Admin")]

    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PuppyController
        public async Task<IActionResult> Index()
        {
            List<Event> events = await ApplicationDb.GetEventsAsync(_context);

            return View(events);
        }
       

        // GET: PuppyController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PuppyController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PuppyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event e)
        {
            if (ModelState.IsValid)
            {
                await ApplicationDb.AddEventAsync(_context, e);
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: PuppyController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Event e = await ApplicationDb.GetEventAsync(_context, id);

            return View(e);
        }

        // POST: PuppyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Event e)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(e).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(e);
        }

        // GET: PuppyController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Event e = await ApplicationDb.GetEventAsync(_context, id);

            return View(e);
        }

        // POST: PuppyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id, IFormCollection collection)
        {
            Event e = await ApplicationDb.GetEventAsync(_context, id);

            _context.Entry(e).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
