using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStore.Models;

namespace VinylStore.Concrete
{
    public class VinylStoreDbContext : DbContext
    {
        public VinylStoreDbContext(DbContextOptions<VinylStoreDbContext> options)
            : base (options)
        {

        }
        public DbSet<Vinyl> Vinyls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
