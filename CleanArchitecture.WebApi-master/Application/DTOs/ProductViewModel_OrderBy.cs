using System;
namespace Application.DTOs
{
    public class ProductViewModel_OrderBy
    {
        public int numberPage { get; set; }
        public int pageSize { get; set; }


        public string nameColumnOrderBy { get; set; }
        public string typeColumnOrderBy { get; set; }
    }
}
