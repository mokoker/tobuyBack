using System.Collections.Generic;
using System.Xml;
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
        private void GetCats(CategoryDto dto, List<string> catList)
        {
            if (dto.Children != null)
            {
                foreach (CategoryDto cat in dto.Children)
                {
                    catList.Add(cat.Id + "?" + cat.Name.Replace(" ",""));
                    GetCats(cat, catList);
                }
            }
        }

        [HttpGet("main/sitemap.xml")]
        public void SitemapXml()
        {
            var dto = cs.GetCategory(1);
            List<string> categoryUrls = new List<string>();
            GetCats(dto, categoryUrls);
            var dto2 = ass.SearchAd(new SearchAdDto { Per_page = 1000 });
            string host = Request.Scheme + "://" + Request.Host;
            Response.ContentType = "application/xml";
            using (var xml = XmlWriter.Create(Response.Body, new XmlWriterSettings { Indent = true }))
            {
                xml.WriteStartDocument();
                xml.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");
                xml.WriteStartElement("url");
                foreach (string location in categoryUrls)
                {
                    xml.WriteElementString("loc", "https://karasayfa.com/main/category/" + location);
                }
                foreach (var ad in dto2.Data)
                {
                    xml.WriteElementString("loc", "https://karasayfa.com/main/post/" + ad.Id);
                }
                xml.WriteEndElement();
                xml.WriteEndElement();
            }
        }
        public IActionResult Index()
        {
            var dto = ass.SearchAd(new SearchAdDto { Per_page = 1000 ,GetAll = true});
            return View("Posts", dto);
        }
        public IActionResult Categories()
        {
            var dto = cs.GetCategory(1);
            return View("Categories", dto);
        }
        public IActionResult Category(int id)
        {
            var dto = ass.SearchAd(new SearchAdDto { CategoryId = id, Per_page = 1000 ,GetAll = true});
            return View("Posts", dto);
        }
        public IActionResult Post(int id)
        {
            var dto = ass.GetAd(id);
            if (dto == null)
                return NotFound("post not found");
            else
                return View("Post", dto); 
        }
    }
}