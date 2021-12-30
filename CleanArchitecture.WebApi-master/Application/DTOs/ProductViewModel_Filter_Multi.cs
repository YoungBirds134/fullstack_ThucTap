namespace Application.DTOs
{
    public class ProductViewModel_Filter_Multi
    {
        public int numberPage { get; set; }
        public int pageSize { get; set; }



        public string nameSearch { get; set; }
        public string barcodeSearch { get; set; }

        public string descriptionSearch { get; set; }


        public string nameOrderBy { get; set; }
        public string barcodeOrderBy { get; set; }
        public string descriptionOrderBy { get; set; }
        public string rateOrderBy { get; set; }
    }
}