using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OBJS.API.Models;
using OBJS.API.Models.Advertises;

namespace OBJS.API.Controllers
{
    public class AdvertisesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public AdvertisesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Advertises
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.Advertises.Include(a => a.Advertisestate).Include(a => a.Category).Include(a => a.Customer);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: Advertises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertise = await _context.Advertises
                .Include(a => a.Advertisestate)
                .Include(a => a.Category)
                .Include(a => a.Customer)
                .FirstOrDefaultAsync(m => m.AdvertiseID == id);
            if (advertise == null)
            {
                return NotFound();
            }

            return View(advertise);
        }

        // GET: Advertises/Create
        public IActionResult Create()
        {
            ViewData["AdvertisestateID"] = new SelectList(_context.AdvertiseStates, "AdvertiseStateID", "AdvertiseStateID");
            ViewData["CategoryID"] = new SelectList(_context.SubCategories, "SubcategoryID", "SubcategoryID");
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "Password");
            return View();
        }

        // POST: Advertises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdvertiseID,IsActive,Startdate,EndDate,AdvertisestateID,CategoryID,CustomerID")] Advertise advertise)
        {
            if (ModelState.IsValid)
            {
                _context.Add(advertise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdvertisestateID"] = new SelectList(_context.AdvertiseStates, "AdvertiseStateID", "AdvertiseStateID", advertise.AdvertisestateID);
            ViewData["CategoryID"] = new SelectList(_context.SubCategories, "SubcategoryID", "SubcategoryID", advertise.CategoryID);
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "Password", advertise.CustomerID);
            return View(advertise);
        }

        // GET: Advertises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertise = await _context.Advertises.FindAsync(id);
            if (advertise == null)
            {
                return NotFound();
            }
            ViewData["AdvertisestateID"] = new SelectList(_context.AdvertiseStates, "AdvertiseStateID", "AdvertiseStateID", advertise.AdvertisestateID);
            ViewData["CategoryID"] = new SelectList(_context.SubCategories, "SubcategoryID", "SubcategoryID", advertise.CategoryID);
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "Password", advertise.CustomerID);
            return View(advertise);
        }

        // POST: Advertises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdvertiseID,IsActive,Startdate,EndDate,AdvertisestateID,CategoryID,CustomerID")] Advertise advertise)
        {
            if (id != advertise.AdvertiseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(advertise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertiseExists(advertise.AdvertiseID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdvertisestateID"] = new SelectList(_context.AdvertiseStates, "AdvertiseStateID", "AdvertiseStateID", advertise.AdvertisestateID);
            ViewData["CategoryID"] = new SelectList(_context.SubCategories, "SubcategoryID", "SubcategoryID", advertise.CategoryID);
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "Password", advertise.CustomerID);
            return View(advertise);
        }

        // GET: Advertises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertise = await _context.Advertises
                .Include(a => a.Advertisestate)
                .Include(a => a.Category)
                .Include(a => a.Customer)
                .FirstOrDefaultAsync(m => m.AdvertiseID == id);
            if (advertise == null)
            {
                return NotFound();
            }

            return View(advertise);
        }

        // POST: Advertises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advertise = await _context.Advertises.FindAsync(id);
            _context.Advertises.Remove(advertise);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvertiseExists(int id)
        {
            return _context.Advertises.Any(e => e.AdvertiseID == id);
        }
    }
}
