﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TB.Db;
using TB.Db.Services;
using ToBuy.Common.DTOs;
using ToBuy.Common.Helpers;
using ToBuy.Middleware;


namespace ToBuy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XController : BaseBController
    {
        private AdService service;
        public XController(IHttpContextAccessor accessor) :base(accessor)
        {
            ToBuyContext context = new ToBuyContext();
            service = new AdService(context);
        }

        // GET: api/Ad/5
        [HttpGet("{id}", Name = "GeTAd")]
        public AdDto Get(int id)
        {
            return service.GetAd(id);
        }

        [AllowAnonymous]
        [JwtAuth(Common.Enums.Roles.None)]
        [HttpGet("[Action]",Name = "SearchX")]
        public SearchAdResultDto SearchX([FromQuery]SearchAdDto dto)
        {
            if (dto.MyMessages)
                dto.UserId = UserId;

            return service.SearchAd(dto);
        }

        // POST: api/Ad
        [JwtAuth(Common.Enums.Roles.User)]
        [HttpPost]
        public void Post([FromBody] AdDto value)
        {
            value.IpAddress = IpAddress;
            value.PosterId = UserId;
            service.AddNewAd(value);
        
        }

        [HttpPost("[Action]",Name = "FillRandomBulk")]
        public void FillRandomBulk()
        {
            for(int i = 0;i<20;i++)
            {
                AdDto dto = new AdDto();
                dto.CategoryId = 5;
                dto.City = Common.Enums.Cities.Ankara;
                dto.Message = RandomTextGenerator.RandomString(100);
                dto.Title = RandomTextGenerator.RandomString(20);
                dto.PosterId = 1;
                service.AddNewAd(dto);
            }
        }

        // PUT: api/Ad/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [JwtAuth(Common.Enums.Roles.User)]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.Delete(id, UserId);
        }
    }
}
