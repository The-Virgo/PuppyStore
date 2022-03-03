using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PuppyStoreFinal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuppyStoreFinal.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options){}

        public DbSet<Puppy> Puppies { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<PuppyStoreFinal.Models.EmailProviderSendgrid> EmailProviderSendgrid { get; set; }
    }
}
