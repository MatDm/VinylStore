using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStore.Entities;

namespace VinylStore.Abstract
{
    public class VinylStoreDbContext : DbContext
    {
        public VinylStoreDbContext(DbContextOptions<VinylStoreDbContext> options)
            : base (options)
        {

        }
        public DbSet<Vinyl> Vinyls { get; set; }
    }
}
