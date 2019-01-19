using System.Collections.Generic;

namespace ToBuy.Common.DTOs
{
    public class SearchAdResultDto : SearchAdDto
    {
        public SearchAdResultDto()
        {

        }
        public SearchAdResultDto(SearchAdDto baseDto)
        {
            Filter = baseDto.Filter;
            Per_page = baseDto.Per_page;
            Page = baseDto.Page;
            CategoryId = baseDto.CategoryId;
        }
        public int From { get { return (Page - 1) * Per_page; } }
        public int To { get { return (Page ) * Per_page; } }
        public List<AdDto> Data{get;set;}
        public int Last_page { get; set; }
    }
}
