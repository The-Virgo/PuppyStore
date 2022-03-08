using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuppyStoreFinal.Data;
using PuppyStoreFinal.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PuppyStoreFinal.Controllers
{
    [Authorize(Roles = "Admin")]

    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webhost;

        public EventController(ApplicationDbContext context, IWebHostEnvironment webhost)
        {
            _context = context;
            _webhost = webhost;
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
            Event e = new Event();
            return PartialView("_EventCreatePartial", e);
        }

        // POST: PuppyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event e, IFormFile eventFile)
        {
            if (ModelState.IsValid)
            {
                await ApplicationDb.AddEventAsync(_context, e);
                await UploadPic(eventFile, "event" + e.EventId.ToString());

                return RedirectToAction("Index");
            }

            return PartialView("_EventCreatePartial", e);
        }

        // GET: PuppyController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Event e = await ApplicationDb.GetEventAsync(_context, id);

            return PartialView("_EventEditPartial", e);
        }

        // POST: PuppyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Event e, IFormFile eventFile)
        {
            string fileName = "event" + e.EventId.ToString() + ".jpg";

            if (ModelState.IsValid)
            {
                _context.Entry(e).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                if(eventFile != null)
                {
                    if (System.IO.File.Exists(Path.Combine(_webhost.WebRootPath, "event-pictures", fileName)))
                    {
                        System.IO.File.Delete(Path.Combine(_webhost.WebRootPath, "event-pictures", fileName));
                    }
                    await UploadPic(eventFile, "event" + e.EventId.ToString());
                }
                

                return RedirectToAction("Index");
            }
            return PartialView("_EventEditPartial", e);
        }

        // GET: PuppyController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Event e = await ApplicationDb.GetEventAsync(_context, id);

            return PartialView("_EventDeletePartial", e);
        }

        // POST: PuppyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id, IFormCollection collection)
        {
            Event e = await ApplicationDb.GetEventAsync(_context, id);

            string fileName = "event" + e.EventId.ToString() + ".jpg";

            _context.Entry(e).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            if (System.IO.File.Exists(Path.Combine(_webhost.WebRootPath, "event-pictures", fileName)))
            {
                System.IO.File.Delete(Path.Combine(_webhost.WebRootPath, "event-pictures", fileName));
            }

            return RedirectToAction("Index");
        }

        public async Task UploadPic(IFormFile file, string newName)
        {
            string extension = Path.GetExtension(file.FileName);
            string newFileName = newName + extension;

            var uploadPath = Path.Combine(_webhost.WebRootPath, "event-pictures", newFileName);

            using (var upload = new FileStream(uploadPath, FileMode.Create))
            {
                await file.CopyToAsync(upload);
            }
        }
    }
}
