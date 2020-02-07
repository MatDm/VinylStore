using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VinylStore.Controllers
{
    public class SalesController : Controller
    {

        // GET: Sales
        public ActionResult Index()
        {
            return View();
        }

        // GET: Sales/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sales/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Sales/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Sales/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Authorize]
        public IActionResult PaymentReturn(Guid transactionId)
        {
            try
            {
                string trId = HttpContext.Session.GetString("_TransactionId");
                string vinylId = HttpContext.Session.GetString("_VinylId");
                string buyerId = HttpContext.Session.GetString("_BuyerId");
                trId = Regex.Replace(trId, "\\\"", "");
                bool result = IsTransactionPayed(new Guid(trId));

                if (result)
                {
                    //que se passe-t-il lorqu'un vinyle à été acheté? => écrire le code
                    ViewBag.Success = true;
                }
                else
                    ViewBag.Success = false;
                return View();
            }
            catch (Exception ex)
            {
                //log error
                return View("~/Views/Shared/Error.cshtml", ex.Message);
            }
        }

        private bool IsTransactionPayed(Guid TransactionId)
        {
            try
            {
                string urlEnd = "?transactionId=" + TransactionId;
                string url = "https://localhost:44386/api/Transaction/GetInfo/" + urlEnd;
                bool isPayed = false;

                using (var client = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                    {
                        var response = client.SendAsync(request);
                        Task<string> task = Task.Run<string>(async () => await response.Result.Content.ReadAsStringAsync());
                        string rep = task.Result;
                        isPayed = bool.Parse(rep);
                    }
                }
                return isPayed;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}