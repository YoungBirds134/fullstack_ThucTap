using System;
namespace Application.DTOs
{
    public class ProductViewModel
    {

        public int numberPage { get; set; }
        public int pageSize { get; set; }


        public string nameColumnOrderBy { get; set; }
        public string typeColumnOrderBy { get; set; }


        public string nameColumnFilter { get; set; }
        public string valueColumnFilter { get; set; }
    }
}
