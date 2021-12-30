using System;
namespace Application.DTOs
{
    public class ProductViewModel_Filter
    {

        public int numberPage { get; set; }
        public int pageSize { get; set; }



        public string nameColumnFilter { get; set; }
        public string valueColumnFilter { get; set; }
    }
}
