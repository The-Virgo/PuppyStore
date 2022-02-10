using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuppyStoreFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PuppyStoreFinal.Data
{
    public class ApplicationDb
    {
        public static async Task<List<Puppy>> GetPuppiesAsync(ApplicationDbContext _context)
        {
            return await (from p in _context.Puppies
                          orderby p.PuppyId ascending
                          select p).ToListAsync();
        }

        public static async Task<Puppy> AddPuppyAsync(ApplicationDbContext _context, Puppy p)
        {
            _context.Puppies.Add(p);
            await _context.SaveChangesAsync();
            return p;
        }

        public static async Task<Puppy> GetPuppyAsync(ApplicationDbContext _context, int puppyId)
        {
            Puppy p = await (from puppies in _context.Puppies
                            where puppies.PuppyId == puppyId
                            select puppies).SingleAsync();

            return p;
        }
    }
}
