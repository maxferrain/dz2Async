using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace dz2Async
{
    public class Context : DbContext
    {
        public DbSet<Content> Contents { get; set; }
        public DbSet<Actors> Actors { get; set; }
        public DbSet<Country> Countries { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite($"Data Source='/Users/maximilienk/MEPHI/filmsdz2.db'");
        }
    }
}
