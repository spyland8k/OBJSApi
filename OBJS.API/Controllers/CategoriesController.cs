using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OBJS.API.Models;
using OBJS.API.Models.Categories;

namespace OBJS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public CategoriesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            
            if (categories == null)
            {
                return NotFound("Sistemde kategori bulunmamaktadır.");
            }

            return categories;
        }


        // responses, requested category has a main category send it back with parents
        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            List<Category> categories = new List<Category>();
            categories.Add(category);

            //that category has a main category, check for parents
            while (category.ParentID != null)
            {
                //get parent category
                var maincategory = await _context.Categories.FindAsync(category.ParentID);
                //add to the list parent category
                categories.Add(maincategory);
                //after added swap their positions, for check parentId of parent
                category = maincategory;
            }

            return categories;
        }
    }
}
