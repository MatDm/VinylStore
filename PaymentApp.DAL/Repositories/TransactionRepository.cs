using Microsoft.EntityFrameworkCore;
using PaymentApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaymentApp.DAL.Repositories
{
    public class TransactionRepository : IRepository<Transaction>
    {
        private readonly PaymentAppDbContext db;
        public TransactionRepository(PaymentAppDbContext context)
        {
            db = context;
        }

        public void Add(Transaction newObj)
        {
            db.Transactions.Add(newObj);
        }

        public void Commit()
        {
            db.SaveChanges();
        }

        public IEnumerable<Transaction> GetAll()
        {
            return db.Transactions;
        }

        public Transaction GetById(Guid id)
        {
            return db.Transactions.Include(c => c.Client).FirstOrDefault(x => x.Id == id);
        }

        public void Update(Transaction newObj)
        {
            db.Transactions.Update(newObj);
        }
    }
}
