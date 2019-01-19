using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TB.Db;
using TB.Db.Services;
using ToBuy.Common.DTOs;

namespace ToBuy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        // GET: api/Categories/5
        [HttpGet("{id}", Name = "GetCategories")]
        public CategoryDto Get(int id)
        {
            ToBuyContext context = new ToBuyContext();
            CategoryService cs = new CategoryService(context);
            return cs.GetCategory(id);
        }

        // POST: api/Categories
        [HttpPost]
        public void Post([FromBody] CategoryDto value)
        {
            ToBuyContext context = new ToBuyContext();
            CategoryService cs = new CategoryService(context);
            cs.AddNewCategory(value);
        }

    }
}
