using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OBJS.API.Models;
using OBJS.API.Models.Advertises;
using OBJS.API.Models.Customers;
using OBJS.API.Models.Transactions;

namespace OBJS.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class BidsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public BidsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Advertises/5/Bids
        [HttpGet("Advertises/{id:int}/Bids", Name = "GetAdvertiseBidsbyId")]
        public async Task<ActionResult<IEnumerable<Bid>>> GetAdvertiseBidsbyId(int id)
        {
            var advertisebids = await _context.Bids
                .Where(a => a.AdvertiseId == id).ToListAsync();

            if(advertisebids == null)
            {
                return NotFound("İlan'a ait teklif bulunmuyor");
            }

            return advertisebids;
        }


        //@Param= id: advertiseId, bid: bid details for advertise
        // POST: api/Advertises/5/Bids
        [HttpPost("Advertises/{id:int}/Bids", Name = "PostAdvertiseBidbyId")]
        public async Task<ActionResult<Bid>> PostAdvertiseBidbyId(int id, Bid bid)
        {
            if (bid == null)
            {
                return BadRequest("Gönderilen içerik boş olamaz");
            }

            var advertise = await _context.Advertises.FindAsync(id);

            if (advertise == null)
            {
                return BadRequest("İlan bulunamadı");
            }

            if (advertise.IsActive == false)
            {
                return Ok("Bu ilan aktif olmadığı için teklif veremezsiniz");
            }

            if (advertise.CustomerId == bid.CustomerId)
            {
                return BadRequest("Kendi oluşturduğunuz ilana teklif veremezsiniz");
            }

            /*var customerbids = await _context.Bids
               .Where(a => a.AdvertiseId == id)
               .Where(b => b.Price == bid.Price).ToListAsync();*/

            var advertiseBid = new Bid();
            advertiseBid.AdvertiseId = advertise.AdvertiseId;
            advertiseBid.Price = bid.Price;
            advertiseBid.Duration = bid.Duration;
            advertiseBid.CustomerId = bid.CustomerId;

            _context.Bids.Add(advertiseBid);

            await _context.SaveChangesAsync();

            return advertiseBid;
        }
    }
}
