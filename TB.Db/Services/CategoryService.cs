using System;
using System.Collections.Generic;
using System.Text;
using ToBuy.Common.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TB.Db.Services
{
    public class CategoryService : BaseService
    {
        private static CategoryDto mainCategory;
        private static object lockObject = new object();
        public CategoryService(ToBuyContext context) : base(context)
        {
        }

        protected internal CategoryDto GetMainCategory()
        {
            if (mainCategory == null)
            {
                lock (lockObject)
                {
                    mainCategory = context.Categories.ToList().First(x => x.Id == 1).GetDto();
                }
            }
            return mainCategory;
        }
        public CategoryDto GetCategory(int id)
        {
            if (id == 1)
            {
              return  GetMainCategory();
            }

            return context.Categories.Include(x => x.Childs).First(c => c.Id == id).GetDto();
        }

        public void AddNewCategory(CategoryDto dto)
        {
            Category cat = Category.GetEnt(dto);

            context.Categories.Add(cat);
            context.Entry(cat).Property("ParentId").CurrentValue = dto.ParentId;
            context.SaveChanges();
        }

        public void UpdateCategory(CategoryDto dto)
        {
            context.Categories.Update(Category.GetEnt(dto));
            context.SaveChanges();
        }
    }
}
