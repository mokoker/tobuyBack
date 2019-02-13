using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TB.Db;
using TB.Db.Services;
using ToBuy.Common.DTOs;
using ToBuy.Common.Enums;
using ToBuy.Middleware;

namespace ToBuy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XController : BaseBController
    {
        private AdService service;
        private readonly IMemoryCache memoryCache;
        private static MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions().SetSize(1).SetSlidingExpiration(TimeSpan.FromSeconds(180));

        public XController(IHttpContextAccessor accessor, IMemoryCache memoryCache) :base(accessor)
        {
            this.memoryCache = memoryCache;
            ToBuyContext context = new ToBuyContext();
            service = new AdService(context);
        }

        [HttpGet("{id}", Name = "GeTAd")]
        public AdDto Get(int id)
        {
            return service.GetAd(id);
        }

        [AllowAnonymous]
        [JwtAuth(Roles.None)]
        [HttpGet("[Action]",Name = "SearchX")]
        public SearchAdResultDto SearchX([FromQuery]SearchAdDto dto)
        {
            if (dto.MyMessages)
            {
                dto.UserId = UserId;
                return service.SearchAd(dto);
            }
            else
            {
                SearchAdResultDto result;
                string cit = dto.Cities != null ? String.Join(",", dto.Cities) : "";
                string cacheKey = dto.CategoryId.ToString() + dto.Filter +cit + dto.Page + dto.ToSell;
                bool isExist = memoryCache.TryGetValue(cacheKey, out result);
                if (!isExist)
                {
                    result= service.SearchAd(dto);
               
                    memoryCache.Set(cacheKey, result, cacheEntryOptions);
                }
                return result ;
            }
        }

        [JwtAuth(Roles.User)]
        [HttpPost]
        public void Post([FromBody] AdDto value)
        {
            value.IpAddress = IpAddress;
            if (UserRole != Roles.Administrator)
            {
                value.PosterId = UserId;
            }
            service.AddNewAd(value);       
        }

        [JwtAuth(Roles.User)]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.Delete(id, UserId);
        }
    }
}
