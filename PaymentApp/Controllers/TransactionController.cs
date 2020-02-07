using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentApp.DAL;
using PaymentApp.DAL.Entities;
using PaymentApp.DAL.Repositories;

namespace PaymentApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly PaymentAppDbContext _context;
        public TransactionController(PaymentAppDbContext context)
        {
            _context = context;
        }

        // GET: api/Transaction
        [HttpGet("{id}", Name = "GetInfo")]
        public IActionResult GetInfo(Guid transactionId)
        {
            TransactionRepository transactRepo = new TransactionRepository(_context);
            if (transactRepo.GetById(transactionId).IsPayed)
                return Ok("true");
            return Ok("false");
        }

        // GET: api/Transaction/5
        //[HttpGet("{id}", Name = "Get")]
        [HttpGet]
        [ProducesResponseType(typeof(Guid), 200)]
        public IActionResult Initialize(string clientToken, double total)
        {
            //This method initialize the transaction and return its id

            ClientRepository clientRepo = new ClientRepository(_context);
            if (!clientRepo.IsRealClient(clientToken) || total == 0)
                return BadRequest();
            TransactionRepository transactRepo = new TransactionRepository(_context);
            Guid guid = new Guid();
            Transaction transaction = new Transaction
            {
                Id = guid,
                CreationDate = DateTime.Now,
                IsPayed = false,
                Total = total,
                Client = clientRepo.GetByToken(clientToken)
            };
            transactRepo.Add(transaction);
            transactRepo.Commit();
            return Ok();
        }

        // POST: api/Transaction
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Transaction/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
