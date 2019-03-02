using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TB.Db.Entities;
using ToBuy.Common.DTOs;
using ToBuy.Common.Enums;

namespace TB.Db.Services
{
    public class AdService : BaseService
    {
        private static Dictionary<int, List<int>> lookup;
        private static Dictionary<int, string> namer;
        private static object lockme = new object();
        public AdService(ToBuyContext context) : base(context)
        {
            if (lookup == null)
            {
                lock (lockme)
                {
                    if (lookup == null)
                    {
                        CategoryService service = new CategoryService(context);
                        Category cat = context.Categories.ToList().First(x => x.Id == 1);
                        lookup = new Dictionary<int, List<int>>();
                        namer = new Dictionary<int, string>();
                        FillNamer(cat);
                        FillDict(cat);
                    }
                }
            }
        }

        private void FillNamer(Category cat)
        {
            namer.Add(cat.Id, cat.Name);
            if (cat.Childs != null)
            {
                foreach (Category c in cat.Childs)
                {
                    FillNamer(c);
                }
            }
        }

        private void FillDict(Category cat)
        {
            lookup.Add(cat.Id, cat.ChildCategories);
            if (cat.Childs != null)
            {
                foreach (Category c in cat.Childs)
                {
                    FillDict(c);
                }
            }
        }

        public static Dictionary<int, string> Namer
        {
            get { return namer; }
        }

        public AdDto GetAd(int id)
        {
            var t = context.Ads.Include(y => y.Poster).SingleOrDefault(x => x.Id == id);
            if (t != null)
            {
                return t.GetDto();
            }
            else
            {
                return null;
            }
        }
        public void AddNewAd(AdDto dto)
        {
            var ent = Ad.GetEnt(dto);
            ent.PostDate = DateTime.Now;
            context.Ads.Add(ent);
            context.SaveChanges();
        }

        public void Delete(int id, int userId)
        {
            var ent = context.Ads.Single(x => x.Id == id && x.PosterId == userId);
            ent.State = PostState.Inactive;
            context.SaveChanges();
        }

        public SearchAdResultDto SearchAd(SearchAdDto dto)
        {
            if (dto.Per_page == 0)
            {
                dto.Per_page = 20;
            }

            if (dto.Page == 0)
            {
                dto.Page = 1;
            }

            SearchAdResultDto result = new SearchAdResultDto(dto);

            List<AdDto> ads = new List<AdDto>();
            IQueryable<Ad> adEnts = context.Ads.Where(k => k.State == PostState.Active);

            if (dto.CategoryId != 0)
            {
                result.CategoryName = Namer[dto.CategoryId];
                List<int> looky;
                lookup.TryGetValue(dto.CategoryId, out looky);
                adEnts = adEnts.Where(j => looky.Contains(j.CategoryId));
            }
            if (dto.UserId != 0)
            {
                adEnts = adEnts.Where(p => p.PosterId == dto.UserId);
            }
            else if (!string.IsNullOrEmpty(dto.Filter))
            {
                string query = String.Join(" | ", dto.Filter.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                adEnts = adEnts.Where(p => p.SearchVector.Matches(EF.Functions.ToTsQuery("turkish", query)));
            }
            else
            {
                if (!dto.GetAll)
                {
                    adEnts = adEnts.Where(z => z.ToSell == dto.ToSell);
                }

                if (dto.Cities != null && dto.Cities.Count > 0)
                {
                    adEnts = adEnts.Where(p => dto.Cities.Contains(p.City));
                }
            }
            result.Last_page = (adEnts.Count() + dto.Per_page - 1) / dto.Per_page;
            var x = adEnts.OrderByDescending(j => j.PostDate).Skip((dto.Page - 1) * dto.Per_page).Take(dto.Per_page).Include(y => y.Poster);
            foreach (var y in x)
            {
                ads.Add(y.GetDto());
            }
            result.Data = ads;
            return result;
        }
    }
}
