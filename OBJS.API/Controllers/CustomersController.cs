using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OBJS.API.Models;
using OBJS.API.Models.Customers;

namespace OBJS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public CustomersController(ApplicationDBContext context)
        {
            _context = context;
        }

        /* Get all customers registered on Database(context is contains datas); Select * from Customers
         */
        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            /*Verilerin gizliliği için, controller'da kullanılan her sınıf için DTO veya
             * O sınıflardaki attribute'ların [JsonIgnore] ile tanımlanması gerekiyor.
            */
            var customers = await _context.Customers.ToListAsync();


            foreach (var customer in customers)
            {
                var customerdetail = await _context.CustomerDetails
                    .Include(d => d.Customer)
                    .FirstOrDefaultAsync(a => a.CustomerId == customer.CustomerId);
            }

            return customers;
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound(id + " numaraya sahip kullanıcı yoktur.");
            }

            return customer;
        }

        /* @param int $id
        * Parametre'den gelen $id ile CustomerDetail'daki $CustomerId(FK) değerini arayıp eşleşen, Customer'ın tüm bilgileri getirir.
        */
        // GET: api/Customer/5/Details
        [HttpGet("{id:int}/details", Name = "GetCustomerDetailbyId")]
        public async Task<ActionResult> GetCustomerDetailbyId(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound("Sistemde " + id + "'ye sahip numaralı kullanıcı yoktur.");
            }

            var customerdetail = await _context.CustomerDetails.AsNoTracking()
                .Include(p => p.Customer)
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if(customerdetail == null)
            {
                return NotFound(id + " numaralı kullanıcının detaylı bilgisi yoktur.");
            }

            customer.CustomerDetails.Add(customerdetail);

            return Ok(customer);
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {

            if (customer == null)
            {
                return BadRequest("Gönderilen içerik boş olamaz");
            }

            if (id != customer.CustomerId)
            {
                return BadRequest("Sistemde bu kullanıcı bulunmamaktadır");
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Content(id + " numaralı kullanıcının bilgileri güncellendi.");
        }

        // PUT: api/Customers/5/details
        //----------

        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            if(customer == null)
            {
                return BadRequest("Gönderilen içerik boş olamaz");
            }
            switch (CustomerValidation(customer))
            {
                case 1:
                    return BadRequest("Bu username sistemde kayıtlıdır");
                case 2:
                    return BadRequest("Bu email sistemde kayıtlıdır");
                default:
                    break;
            }

            _context.Customers.Add(customer);

            //AssingCustomerId(customer);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        // Post: api/Customers/5/Details
        [HttpPost("{id:int}/Details", Name = "PostCustomerDetailbyId")]
        public async Task<ActionResult> PostCustomerDetailbyId(int id, Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("İçerik boş olamaz.");
            }

            var c = await _context.Customers.FindAsync(id);

            if (id != customer.CustomerId)
            {
                return BadRequest("Yetkisiz üye, tekrar giriş yapın.");
            }

            if(c == null)
            {
                return BadRequest("Kullanıcı bulunamadı.");
            }

            //Recieving JSON data, filling customerdetails table.
            var customerDetail = new CustomerDetail();
            customerDetail.CustomerId = customer.CustomerId;
            customerDetail.Address = customer.CustomerDetails.FirstOrDefault().Address;
            customerDetail.City = customer.CustomerDetails.FirstOrDefault().City;
            customerDetail.Phone = customer.CustomerDetails.FirstOrDefault().Phone;

            _context.CustomerDetails.Add(customerDetail);
            
            //Save asyncronized changes on dbcontext
            await _context.SaveChangesAsync();

            return Content("Kullanıcı detayı eklendi!");
        }


        //Send customer to the method, it checks if user is in the system
        private int CustomerValidation(Customer customer)
        {
            if (CustomerUsernameExists(customer))
            {
                return 1;
            }else if(CustomerEmailExists(customer))
            {
                return 2;
            }
            return 0;
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(c => c.CustomerId == id);
        }

        private bool CustomerUsernameExists(Customer customer)
        {
            return _context.Customers.Any(c => c.Username == customer.Username);
        }

        private bool CustomerEmailExists(Customer customer)
        {
            return _context.Customers.Any(c => c.Email == customer.Email);
        }
    }
}
