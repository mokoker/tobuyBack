using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TB.Db;
using TB.Db.Services;
using ToBuy.Common.DTOs;

namespace ToBuy
{
    public class MainController : Controller
    {
        private CategoryService cs;
        private AdService ass;
        public MainController()
        {
            ToBuyContext context = new ToBuyContext();
            cs = new CategoryService(context);
            ass = new AdService(context);
        }
        public IActionResult Index()
        {
            var dto = cs.GetCategory(1);
            return View("Categories", dto);
        }
        public IActionResult Category(int id)
        {
            var dto = ass.SearchAd(new SearchAdDto { CategoryId = id, Per_page = 1000 });
            return View("Posts", dto);
        }
        public IActionResult Post(int id)
        {
            var dto = ass.GetAd(id);
            return View("Post", dto);
        }
    }
}