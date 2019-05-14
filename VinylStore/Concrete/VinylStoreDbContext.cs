using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStore.Auth;
using VinylStore.Models;

namespace VinylStore.Abstract
{
    public class VinylStoreDbContext : IdentityDbContext<ApplicationUser>
    {
        public VinylStoreDbContext(DbContextOptions<VinylStoreDbContext> options)
            : base (options)
        {

        }
        public DbSet<Vinyl> Vinyls { get; set; }

        public DbSet<VinylForSale> Collections { get; set; }

        public DbSet<Wantlist> Wantlists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
