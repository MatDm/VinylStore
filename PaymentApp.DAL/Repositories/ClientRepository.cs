using PaymentApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaymentApp.DAL.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        private readonly PaymentAppDbContext db;
        public ClientRepository(PaymentAppDbContext context)
        {
            this.db = context;
        }

        public void Add(Client obj)
        {
            throw new NotImplementedException();
        }

        public Client GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Client> GetAll()
        {
            return db.Clients;
        }

        public Client GetByToken(string clientToken)
        {
            return db.Clients.Where(c => string.Equals(c.Token, clientToken)).FirstOrDefault();
        } 

        public bool IsRealClient(string clientToken)
        {
            try
            {
                db.Clients.Where(c => string.Equals(c.Token, clientToken)).FirstOrDefault();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public void Commit()
        {
            db.SaveChanges();
        }
    }
}
