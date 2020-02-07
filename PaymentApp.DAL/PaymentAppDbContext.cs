using Microsoft.EntityFrameworkCore;
using PaymentApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentApp.DAL
{
    public class PaymentAppDbContext : DbContext
    {
        public PaymentAppDbContext(DbContextOptions<PaymentAppDbContext> options) : base(options)
        {

        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}
