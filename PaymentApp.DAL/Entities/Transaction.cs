using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaymentApp.DAL.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public double Total { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool IsPayed { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string Name { get; set; }

        //Relationship
        [Required]
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
