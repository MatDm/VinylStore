using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaymentApp.Models;
using PaymentApp.DAL;
using System.Text.RegularExpressions;
using PaymentApp.DAL.Repositories;
using PaymentApp.DAL.Entities;

namespace PaymentApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly PaymentAppDbContext context;
        public HomeController(PaymentAppDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index(string transactionId)
        {
            ViewBag.Id = transactionId;
            return View();
        }

        public IActionResult PaymentForm(string transactionId)
        {
            transactionId = Regex.Replace(transactionId, "\\\"", "");
            TransactionRepository transactRepo = new TransactionRepository(context);
            Transaction transaction = transactRepo.GetById(Guid.Parse(transactionId));
            ViewBag.Id = transaction.Id.ToString();
            ViewBag.Total = transaction.Total;
            return View();
        }

        [HttpPost]
        public IActionResult PaymentForm(PaymentInfo paymentInfo)
        {
            TransactionRepository transactRepo = new TransactionRepository(context);
            Transaction transaction = transactRepo.GetById(paymentInfo.TransactionId);
            string callbackUrl = transaction.Client.CallbackUrl;
            ViewBag.CallbackUrl = string.Concat(callbackUrl, paymentInfo.TransactionId);
            bool IsAccepted = CheckBancontact();
            if (IsAccepted)
            {
                if(transaction != null)
                {
                    transaction.Country = paymentInfo.Country;
                    transaction.Name = paymentInfo.CardholderName;
                    transaction.PostalCode = paymentInfo.PostalCode;
                    transaction.Street = paymentInfo.Street;
                    transaction.StreetNumber = paymentInfo.StreetNumber;
                    transaction.PaymentDate = DateTime.Now;
                    transaction.IsPayed = true;
                }
                transactRepo.Update(transaction);
                transactRepo.Commit();
                return View("~/Views/Shared/Success.cshtml");
            }
            else
                return View("~/Views/Shared/Error.cshtml");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private bool CheckBancontact()
        {
            Random rnd = new Random();
            int result = rnd.Next(1, 101);
            if (result > 31)
            {
                return true;
            }
            return false;
        }
    }
}
