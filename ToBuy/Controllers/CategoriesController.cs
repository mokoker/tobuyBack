using Microsoft.AspNetCore.Mvc;
using TB.Db;
using TB.Db.Services;
using ToBuy.Common.DTOs;
using ToBuy.Common.Enums;
using ToBuy.Middleware;

namespace ToBuy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private CategoryService cs;
        public CategoriesController()
        {
            ToBuyContext context = new ToBuyContext();
            cs = new CategoryService(context);
        }

        // GET: api/Categories/5
        [HttpGet("{id}", Name = "GetCategories")]
        public CategoryDto Get(int id)
        {
            return cs.GetCategory(id);
        }

        [JwtAuth(Roles.Administrator)]
        [HttpPost]
        public void Post([FromBody] CategoryDto value)
        {
            cs.AddNewCategory(value);
        }

    }
}
