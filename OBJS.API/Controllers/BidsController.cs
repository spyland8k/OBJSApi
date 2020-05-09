using System;
using System.Collections.Generic;
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
    [Route("api/[controller]")]
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
        [HttpGet("Advertises/{id:int}/Bids")]
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

     
        /*// PUT: api/Bids/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBid(int id, Bid bid)
        {
            if (id != bid.BidId)
            {
                return BadRequest();
            }

            _context.Entry(bid).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BidExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        */

        // POST: api/Advertise/5/Bids
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("Advertises/{id:int}/Bids", Name = "PostAdvertiseBidbyId")]
        public async Task<ActionResult<Bid>> PostBid(int id, Advertise advertise, Bid bid)
        {
            if(advertise == null || bid == null)
            {
                return BadRequest("İlan veya Teklif içeriği boş olamaz");
            }


            _context.Advertises.FirstOrDefault().Bids.Add(bid);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBid", new { id = bid.BidId }, bid);
        }


        /*// DELETE: api/Bids/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bid>> DeleteBid(int id)
        {
            var bid = await _context.Bids.FindAsync(id);
            if (bid == null)
            {
                return NotFound();
            }

            _context.Bids.Remove(bid);
            await _context.SaveChangesAsync();

            return bid;
        }*/
        private bool BidExists(int id)
        {
            return _context.Bids.Any(e => e.BidId == id);
        }
    }
}
