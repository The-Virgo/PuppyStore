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


    public class PuppyController : Controller
    {

        private readonly ApplicationDbContext _context;
       

        public PuppyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PuppyController
        public async Task<IActionResult> Index()
        {
            List<Puppy> puppies = await ApplicationDb.GetPuppiesAsync(_context);

            return View(puppies);
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
        public async Task<IActionResult> Create(Puppy p, IFormCollection collection)
        {
            ViewData["Sex"] = collection["Sex"];
            if (ViewData["Sex"].ToString() == "false")
            {
                p.Sex = false;
            }
            else
            {
                p.Sex = true;
            }
            if (ModelState.IsValid)
            {
                await ApplicationDb.AddPuppyAsync(_context, p);
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: PuppyController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Puppy p = await ApplicationDb.GetPuppyAsync(_context, id);

            return View(p);
        }

        // POST: PuppyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Puppy p, IFormCollection collection)
        {
            ViewData["Sex"] = collection["Sex"];
            if (ViewData["Sex"].ToString() == "false")
            {
                p.Sex = false;
            }
            else
            {
                p.Sex = true;
            }
            if (ModelState.IsValid)
            {
                _context.Entry(p).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(p);
        }

        // GET: PuppyController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Puppy p = await ApplicationDb.GetPuppyAsync(_context, id);

            return View(p);
        }

        // POST: PuppyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id, IFormCollection collection)
        {
            Puppy p = await ApplicationDb.GetPuppyAsync(_context, id);

            _context.Entry(p).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
