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
            List<Advertise> advertises;

            if (startdate != null && enddate == null)
            {
                advertises = await _context.Advertises
                    .Where(a => a.Startdate >= startdate).ToListAsync();
            }else if (startdate == null && enddate != null)
            {
                advertises = await _context.Advertises
                    .Where(a => a.EndDate <= enddate).ToListAsync();
            }else if(startdate != null && enddate != null)
            {
                advertises = await _context.Advertises
                    .Where(a => a.Startdate >= startdate && a.EndDate <= enddate).ToListAsync();
            }else
            {
                advertises = await _context.Advertises.ToListAsync();
            }
            
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
                .Where(a => a.CategoryId == id)
                .Include(k => k.AdvertiseDetails).ToListAsync();


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

            var adv = await _context.Advertises.FindAsync(id);

            if (adv == null)
            {
                return NotFound("İlan bulunmamaktadır");
            }

            if(adv.AdvertiseId != id)
            {
                //parametre'den ve gönderilen içerikteki advertiseId'ler eşleşmiyor
                return BadRequest("İlana erişim yetkisi bulunmuyor");
            }

            var advertiseDetail = new AdvertiseDetail();
            advertiseDetail.AdvertiseId = advertise.AdvertiseId;

            advertiseDetail.Title = advertise.AdvertiseDetails.FirstOrDefault().Title;

            advertiseDetail.Description = advertise.AdvertiseDetails.FirstOrDefault().Description;

            advertiseDetail.ImagePath = advertise.AdvertiseDetails.FirstOrDefault().ImagePath;


            _context.AdvertiseDetails.Add(advertiseDetail);

            await _context.SaveChangesAsync();

            return Ok("İlan oluşturuldu/güncellendi");
        }

        // POST: api/Advertises/5/Feedbacks
        [HttpPost("{id:int}/Feedbacks", Name = "PostAdvertiseFeedbackbyId")]
        public async Task<ActionResult<Feedback>> PostAdvertiseFeedbackbyId(int id, Feedback feedback)
        {
            if (feedback == null)
            {
                return BadRequest("İstek içeriği boş olamaz");
            }

            var advertise = await _context.Advertises.FindAsync(id);

            await _context.Advertises
                .Include(k => k.Advertisestate).ToListAsync();

            if (advertise == null)
            {
                return NotFound("İlan bulunmamaktadır");
            }

            if(advertise.IsActive == false)
            {
                return NotFound("İlan aktif değildir, yorum yapılamaz");
            }
            
            //İlan başarıyla tamamlandıktan sonra işi veren ve işi yapan kişiler birbirlerine geri bildirim(yorum) gönderebilir.
            if (advertise.Advertisestate.IsStarted == true || advertise.Advertisestate.IsContinue == true)
            {
                return NotFound("İlan, ilanı oluşturan kişi tarafından onaylanmadan yorum yapılamaz");
            }

            feedback.AdvertiseId = advertise.AdvertiseId;
            feedback.OwnerID = advertise.CustomerId;

            _context.Feedbacks.Add(feedback);

            await _context.SaveChangesAsync();

            return feedback;
        }

        private bool AdvertiseExists(int id)
        {
            return _context.Advertises.Any(e => e.AdvertiseId == id);
        }
    }
}
