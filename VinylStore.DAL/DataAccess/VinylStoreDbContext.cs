using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStore.Common.Auth;
using VinylStore.DAL.Entities;

namespace VinylStore.DAL.DataAccess
{
    public class VinylStoreDbContext : IdentityDbContext<ApplicationUser>
    {
        public VinylStoreDbContext(DbContextOptions<VinylStoreDbContext> options)
            : base (options)
        {

        }
        public DbSet<VinylEF> Vinyls { get; set; }

        public DbSet<VinylForSaleEF> Collections { get; set; }

        public DbSet<WantlistEF> Wantlists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
