﻿using System;
using System.Collections.Generic;
using System.Text;
using TB.Db.Entities;
using ToBuy.Common.DTOs;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TB.Db.Services
{
    public class AdService : BaseService
    {
        private Dictionary<int, List<int>> lookup;
        public AdService(ToBuyContext context) : base(context)
        {
            CategoryService service = new CategoryService(context);
            Category cat = service.GetMainCategory();
            lookup = new Dictionary<int, List<int>>();
            FillDict(cat);
        }

        private void FillDict(Category cat)
        {
            lookup.Add(cat.Id, cat.ChildCategories);
            if (cat.Childs != null)
                foreach (Category c in cat.Childs)
                {
                    FillDict(c);
                }
        }

        public AdDto GetAd(int id)
        {
            return context.Ads.Include(y => y.Poster).SingleOrDefault(x=>x.Id==id).GetDto();
        }
        public void AddNewAd(AdDto dto)
        {
            var ent = Ad.GetEnt(dto);
            ent.PostDate = DateTime.Now;
            context.Ads.Add(ent);
            context.SaveChanges();
        }

        public List<AdDto> GetAds()
        {
            List<AdDto> ads = new List<AdDto>();
            var x = context.Ads.OrderByDescending(k => k.PostDate).Take(20).Include(y => y.Poster).Include(z => z.Category);
            foreach (var y in x)
            {
                ads.Add(y.GetDto());
            }
            return ads;
        }

        public SearchAdResultDto SearchAd(SearchAdDto dto)
        {
            SearchAdResultDto result = new SearchAdResultDto(dto);
            
            List<AdDto> ads = new List<AdDto>();
            IQueryable<Ad> adEnts = context.Ads;

            if (dto.CategoryId != 0)
            {
                List<int> looky;
                lookup.TryGetValue(dto.CategoryId, out looky);
                adEnts = adEnts.Where(j => looky.Contains(j.CategoryId));
            }
            if(!string.IsNullOrEmpty(dto.Filter))
            {
                adEnts = adEnts
               .Where(p => p.SearchVector.Matches(dto.Filter));
            }
            adEnts = adEnts.Where(z => z.ToSell == dto.ToSell);
            result.Last_page = (adEnts.Count() + dto.Per_page - 1) / dto.Per_page;
            var x = adEnts.OrderByDescending(j=>j.PostDate).Skip((dto.Page - 1) * 20).Take(20).Include(y => y.Poster).Include(z => z.Category);
            foreach (var y in x)
            {
                ads.Add(y.GetDto());
            }
            result.Data = ads;
            return result;
        }
    }
}
