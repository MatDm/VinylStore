using PaymentApp.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApp.Models
{
    public class PaymentInfo
    {
        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Total")]
        public double Total { get; set; }

        [Required]
        public Guid TransactionId { get; set; }

        [Required]
        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }

        [Required]
        [Display(Name = "Street")]
        public string Street { get; set; }

        [Required]
        [Display(Name = "Street number")]
        public string StreetNumber { get; set; }

        [Required]
        [Display(Name = "Cardholder name")]
        public string CardholderName { get; set; }

        [Required]
        [Display(Name = "Card type")]
        public PaymentOption CartType { get; set; }

        [Required]
        [Display(Name = "Card number")]
        public string CardNumber { get; set; }

        [Required]
        [Display(Name = "Mois")]
        public int MonthExpDate { get; set; }

        [Required]
        [Display(Name = "Année")]
        public int YearExpDate { get; set; }

        [Required]
        [Display(Name = "Secret code")]
        public string SecretNumber { get; set; }
    }
}
