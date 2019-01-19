using System;
using System.Collections.Generic;
using System.Text;
using ToBuy.Common.DTOs;

namespace TB.Db
{
    public class Category : BaseEntity<Category, CategoryDto>
    {
        public string Name { get; set; }
        public Category Parent { get; set; }
        public List<Category> Childs { get; set; }

        private List<int> childCats;
        public List<int> ChildCategories
        {
            get
            {
                if (childCats == null)
                {
                    childCats = new List<int>();
                    if (Childs != null)
                        foreach (var x in Childs)
                        {
                            childCats.AddRange(x.ChildCategories);
                        }
                    childCats.Add(Id);
                }
                return childCats;

            }
        }

        public override CategoryDto GetDto(CategoryDto dto)
        {
            dto.Id = Id;
            dto.Name = Name;
            if (Parent != null)
                dto.ParentId = Parent.Id;
            if (Childs != null)
            {
                dto.Children = new List<CategoryDto>();
                foreach (Category cat in Childs)
                {
                    dto.Children.Add(cat.GetDto());
                }
            }
            return dto;
        }

        public override void MapEntity(CategoryDto dto)
        {
            Name = dto.Name;
            Id = dto.Id;
        }
    }


}
