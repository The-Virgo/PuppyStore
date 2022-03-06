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


    public class PuppyController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webhost;
       

        public PuppyController(ApplicationDbContext context, IWebHostEnvironment webhost)
        {
            _context = context;
            _webhost = webhost;
        }

        // GET: PuppyController
        public async Task<IActionResult> Index()
        {
            List<Puppy> puppies = await ApplicationDb.GetPuppiesAsync(_context);

            return View(puppies);
        }

        // GET: PuppyController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Puppy p = await ApplicationDb.GetPuppyAsync(_context, id);
            return View(p);
        }

        // GET: PuppyController/Create
        public ActionResult Create()
        {
            Puppy p = new Puppy();
            return PartialView("_PuppyCreatePartial", p);
        }

        // POST: PuppyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Puppy p, IFormCollection collection, IFormFile puppyFile)
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
                await UploadPic(puppyFile, p.PuppyId.ToString());

                return RedirectToAction("Index");
            }

            return PartialView("_PuppyCreatePartial", p);
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
        public async Task<IActionResult> Edit(Puppy p, IFormCollection collection, IFormFile puppyFile)
        {
            string fileName = p.PuppyId.ToString() + ".jpg";

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

                if (puppyFile != null)
                {
                    if (System.IO.File.Exists(Path.Combine(_webhost.WebRootPath, "puppy-pictures", fileName)))
                    {
                        System.IO.File.Delete(Path.Combine(_webhost.WebRootPath, "puppy-pictures", fileName));
                    }
                    await UploadPic(puppyFile, p.PuppyId.ToString());
                }

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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Puppy p = await ApplicationDb.GetPuppyAsync(_context, id);

            string fileName = p.PuppyId.ToString() + ".jpg";

            _context.Entry(p).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            if (System.IO.File.Exists(Path.Combine(_webhost.WebRootPath, "puppy-pictures", fileName)))
            {
                System.IO.File.Delete(Path.Combine(_webhost.WebRootPath, "puppy-pictures", fileName));
            }

            return RedirectToAction("Index");
        }

        public async Task UploadPic(IFormFile file, string newName)
        {
            string extension = Path.GetExtension(file.FileName);
            string newFileName = newName + extension;

            var uploadPath = Path.Combine(_webhost.WebRootPath, "puppy-pictures", newFileName);

            using (var upload = new FileStream(uploadPath, FileMode.Create))
            {
                await file.CopyToAsync(upload);
            }
        }

    }
}
