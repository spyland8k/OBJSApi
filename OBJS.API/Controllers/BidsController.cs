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

        //returns advertises 5 of had all bids
        // GET: api/Advertises/5/Bids
        [HttpGet("Advertises/{id:int}/Bids", Name = "GetBidtoAdvertisebyId")]
        public async Task<ActionResult<IEnumerable<Bid>>> GetBids(int id)
        {
            var bids = await _context.Bids.ToListAsync();
            List<Bid> advertisebids = new List<Bid>();

            foreach (var bid in bids)
            {
                if(bid.AdvertiseId == id)
                {
                    advertisebids.Add(bid);
                }
            }

            return advertisebids;
        }


        //@Param= id: advertiseId, bid: bid details for advertise
        // POST: api/Advertise/5/Bids

        [HttpPost("Advertises/{id:int}/Bids", Name = "PostBidtoAdvertisebyId")]
        public async Task<ActionResult<Bid>> PostBid(int id, Bid bid)
        {
            if (bid == null)
            {
                return BadRequest("İçerik boş olamaz.");
            }

            var advertise = await _context.Advertises.FindAsync(id);

            if (id != bid.AdvertiseId)
            {
                return BadRequest("Yetkisiz ilan teklifi");
            }

            if (advertise == null)
            {
                return BadRequest("İlan bulunamadı.");
            }

            //Recieving JSON data, filling customerdetails table.
            var advertiseBid = new Bid();
            advertiseBid.AdvertiseId = advertise.AdvertiseId;
            advertiseBid.Price = bid.Price;
            advertiseBid.Duration = bid.Duration;
            advertiseBid.CustomerId = bid.CustomerId;

            _context.Bids.Add(advertiseBid);

            //Save asyncronized changes on dbcontext
            await _context.SaveChangesAsync();

            return advertiseBid;
        }
    }
}
