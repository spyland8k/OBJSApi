using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OBJS.API.Models;
using OBJS.API.Models.Customers;
using OBJS.API.Models.Transactions;

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

        //Get all customers registered on Database(context is contains datas); Select * from Customers
        // GET: api/Customers
        // GET: api/Customers?username={string}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers([FromQuery] string? username)
        {
            List<Customer> customers;

            if(username == null)
            {
                customers = await _context.Customers
                    .Where(k => k.IsActive == true).ToListAsync();
            }
            else
            {
                customers = await _context.Customers
                    .Where(k => k.Username == username && k.IsActive == true).ToListAsync();
            }

            await _context.Customers
                    .Include(k => k.CustomerDetails)
                    .Include(k => k.Advertises)
                    .Include(k => k.Bids)
                    .Include(k => k.FeedbackFrom)
                    .Include(k => k.FeedbackTo).ToListAsync();

            if (customers == null)
            {
                return NotFound("Username: " + username + "Sistemde kayıtlı değildir");
            }

            return customers;
        }

        // GET: api/Customers/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound(id + " numaraya sahip kullanıcı yoktur.");
            }

            if(customer.IsActive == false)
            {
                return Ok(id + "li kullanıcıya erişilemez");
            }

            await _context.Customers
                .Include(k => k.CustomerDetails)
                .Include(k => k.Advertises)
                .Include(k => k.Bids)
                .Include(k => k.FeedbackFrom)
                .Include(k => k.FeedbackTo).ToListAsync();

            return customer;
        }

        /* @param int $id
        * Parametre'den gelen $id ile CustomerDetail'daki $CustomerId(FK) değerini arayıp eşleşen, Customer'ın tüm bilgileri getirir.
        */
        // GET: api/Customer/5/Details
        [HttpGet("{id:int}/Details", Name = "GetCustomerDetailbyId")]
        public async Task<ActionResult<Customer>> GetCustomerDetailbyId(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound("Sistemde " + id + "'ye sahip numaralı kullanıcı yoktur.");
            }

            if (customer.IsActive == false)
            {
                return Ok(id + "li kullanıcıya erişilemez");
            }

            var customerdetail = await _context.CustomerDetails
                .Where(a => a.CustomerId == id).ToListAsync();

            if (customerdetail == null)
            {
                return NotFound(id + " numaralı kullanıcının detaylı bilgisi yoktur.");
            }

            return customer;
        }

        // GET: api/Customer/5/Bids
        [HttpGet("{id:int}/Bids", Name = "GetCustomerBidsById")]
        public async Task<ActionResult> GetCustomerBidsbyId(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound("Sistemde " + id + "'ye sahip numaralı kullanıcı yoktur.");
            }

            if (customer.IsActive == false)
            {
                return Ok(id + "li kullanıcıya erişilemez");
            }

            await _context.Bids
                .Where(a => a.CustomerId == id).ToListAsync();

            return Ok(customer);
        }

        // GET: api/Customer/5/Feedbacks
        [HttpGet("{id:int}/Feedbacks", Name = "GetCustomerFeedbackbyId")]
        public async Task<ActionResult<Customer>> GetCustomerFeedbackbyId(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound("Sistemde " + id + "'ye sahip numaralı kullanıcı yoktur.");
            }

            if (customer.IsActive == false)
            {
                return Ok(id + "li kullanıcıya erişilemez");
            }

            var customerFromFeedbacks = await _context.Feedbacks
                .Where(a => a.OwnerID == id).ToListAsync();

            var customerToFeedbacks = await _context.Feedbacks
                .Where(a => a.BidderID == id).ToListAsync();

            if(customerFromFeedbacks == null && customerToFeedbacks == null)
            {
                return NotFound(id + " kullanıcının geri bildirimi/yapılmış yorumu yoktur");
            }

            return customer;
        }

        //change customer information and details
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

        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            if (customer == null)
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

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        // Post: api/Customers/5/Details
        [HttpPost("{id:int}/Details", Name = "PostCustomerDetailbyId")]
        public async Task<ActionResult> PostCustomerDetailbyId(int id, Customer customer)
        {
            if(customer == null)
            {
                return BadRequest("Gönderilen İstek içeriği boş olamaz");
            }

            var c = await _context.Customers.FindAsync(id);

            if (c == null)
            {
                return BadRequest("Kullanıcı bulunamadı.");
            }

            if (c.IsActive == false)
            {
                return BadRequest("Kullanıcı yasaklanmış");
            }

            var customerDetail = new CustomerDetail();
            customerDetail.CustomerId = customer.CustomerId;
            customerDetail.Address = customer.CustomerDetails.FirstOrDefault().Address;
            customerDetail.City = customer.CustomerDetails.FirstOrDefault().City;
            customerDetail.Phone = customer.CustomerDetails.FirstOrDefault().Phone;

            _context.CustomerDetails.Add(customerDetail);

            //Save asyncronized changes on dbcontext
            await _context.SaveChangesAsync();

            return Ok("Kullanıcı oluşturuldu / güncellendi");
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
