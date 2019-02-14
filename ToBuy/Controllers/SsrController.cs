using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TB.Db;
using TB.Db.Services;
using ToBuy.Common.DTOs;

namespace ToBuy
{
    public class SsrController : Controller
    {
        private CategoryService cs;
        private AdService ass;
        public SsrController()
        {
            ToBuyContext context = new ToBuyContext();
            cs = new CategoryService(context);
            ass = new AdService(context);
        }
        public IActionResult Main()
        {
           var dto= cs.GetCategory(1);
            return View("Categories",dto);
        }
        public IActionResult Category(int id)
        {
            var dto = ass.SearchAd(new SearchAdDto { CategoryId = id ,Per_page=1000});
            return View("Posts", dto);
        }
        public IActionResult Post(int id)
        {
            var dto = ass.GetAd(id);
            return View("Post", dto);
        }
    }
}