using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Wrappers
{
    public class PagedResponse<T> : Response<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalRows { get; set; }
        public int TotalPages { get; set; }


        public PagedResponse(T data, int pageNumber, int pageSize,int TotalRows,int TotalPages)
        {
            this.TotalRows = TotalRows;
            this.TotalPages = TotalPages;
            this.CurrentPage = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;

        }
    }
}
