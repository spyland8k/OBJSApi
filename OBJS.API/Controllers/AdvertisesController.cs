using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OBJS.API.Models;
using OBJS.API.Models.Advertises;

namespace OBJS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public AdvertisesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Advertises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Advertise>>> GetAdvertises()
        {
            var advertises = await _context.Advertises.ToListAsync();

            foreach (var advertise in advertises)
            {
                var advertisedetail = await _context.AdvertiseDetails
                    .Include(d => d.Advertise)
                    .FirstOrDefaultAsync(a => a.AdvertiseId == advertise.AdvertiseId);
            }

            return advertises;
        }

        // GET: api/Advertises/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Advertise>> GetAdvertise(int id)
        {
            var advertise = await _context.Advertises.FindAsync(id);

            if (advertise == null)
            {
                return NotFound();
            }

            return advertise;
        }

        // GET: api/Advertises/Categories/5
        /* İlanları CategoryId'lere göre filtrele 
         */
        [HttpGet("Categories/{id:int}")]
        public async Task<ActionResult<IEnumerable<Advertise>>> GetAdvertiseByCategoryId(int id)
        {
            var advertises = await _context.Advertises.ToListAsync();
            List<Advertise> categoryadvertises = new List<Advertise>();

            foreach (var advertise in advertises)
            {
                if(advertise.CategoryId == id)
                {
                    categoryadvertises.Add(advertise);
                }
            }

            return categoryadvertises;
        }

        // PUT: api/Advertises/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdvertise(int id, Advertise advertise)
        {
            if (id != advertise.AdvertiseId)
            {
                return BadRequest();
            }

            _context.Entry(advertise).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdvertiseExists(id))
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

        // POST: api/Advertises
        // Requested Json has a include advertisedetails
        [HttpPost]
        public async Task<ActionResult<Advertise>> PostAdvertise(Advertise advertise)
        {
            _context.Advertises.Add(advertise);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdvertise", new { id = advertise.AdvertiseId }, advertise);
        }
    }
}
