using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // ASP.NET Core will automatically bind form values, route values and query strings by name
        // GET: api/Advertises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Advertise>>> GetAdvertises([FromQuery] DateTime? startdate, [FromQuery] DateTime? enddate)
        {
            //return Ok(startdate.ToShortDateString() + " " + enddate.ToShortDateString() + " " + test);
            var advertises = await _context.Advertises
                .Where(a => a.Startdate >= startdate && a.Startdate <= enddate).ToListAsync();

            if (advertises == null)
            {
                return NotFound("Sistemde kayıtlı ilan bulunmuyor");
            }

            await _context.Advertises
                .Include(k => k.AdvertiseDetails).ToListAsync();

            return advertises;
        }

        // GET: api/Advertises/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Advertise>> GetAdvertise(int id)
        {
            var advertise = await _context.Advertises.FindAsync(id);

            if (advertise == null)
            {
                return NotFound("Sistemde " + id + "'ye sahip ilan bulunmuyor");
            }

            await _context.AdvertiseDetails
                .Where(a => a.AdvertiseId == advertise.AdvertiseId).ToListAsync();

            return advertise;
        }

        // GET: api/Advertises/Categories/5
        /* Filter advertises category by categoryId 
         */
        [HttpGet("Categories/{id:int}")]
        public async Task<ActionResult<IEnumerable<Advertise>>> GetAdvertiseByCategoryId(int id)
        {
            var advertises = await _context.Advertises
                .Where(a => a.CategoryId == id).ToListAsync();

            return advertises;
        }

        // PUT: api/Advertises/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdvertise(int id, Advertise advertise)
        {
            if (advertise == null)
            {
                return BadRequest("İstek içeriği boş olamaz");
            }

            if (id != advertise.AdvertiseId)
            {
                return NotFound("Sistemde bu ilan bulunmamaktadır");
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
            if (advertise == null)
            {
                return BadRequest("İstek içeriği boş olamaz");
            }

            _context.Advertises.Add(advertise);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdvertise", new { id = advertise.AdvertiseId }, advertise);
        }

        // inserting to the db all of coming data from client about advertise details.
        // POST: api/Advertises/5/Details
        [HttpPost("{id:int}/Details", Name = "PostAdvertiseDetailbyId")]
        public async Task<ActionResult<Advertise>> PostAdvertiseDetailbyId(int id, Advertise advertise)
        {
            if (advertise == null)
            {
                return BadRequest("İstek içeriği boş olamaz");
            }

            var c = await _context.Advertises.FindAsync(id);

            if (c == null)
            {
                return NotFound("İlan bulunmamaktadır");
            }

            var advertiseDetail = new AdvertiseDetail();
            advertiseDetail.AdvertiseId = advertise.AdvertiseId;

            advertiseDetail.Title = advertise.AdvertiseDetails
                .Where(t => t.AdvertiseId == id).FirstOrDefault().Title;

            advertiseDetail.Description = advertise.AdvertiseDetails
                .Where(t => t.AdvertiseId == id).FirstOrDefault().Description;

            advertiseDetail.ImagePath = advertise.AdvertiseDetails
                .Where(t => t.AdvertiseId == id).FirstOrDefault().ImagePath;


            _context.AdvertiseDetails.Add(advertiseDetail);

            await _context.SaveChangesAsync();

            return Ok("İlan detayı eklendi!");
        }

        private bool AdvertiseExists(int id)
        {
            return _context.Advertises.Any(e => e.AdvertiseId == id);
        }
    }
}
