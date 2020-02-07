using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentApp.DAL.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string CallbackUrl { get; set; }

        //Relationship
        public ICollection<Transaction> Transactions { get; set; }
    }
}
