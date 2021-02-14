using System;

namespace webapi.Services.Wrappers
{
    public class PageResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int Limit { get; set; }
        public Uri FirstPage { get; set; }
        public Uri LastPage { get; set; }
        public int TotalPages { get; set; }
        public int Total { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }

        public PageResponse(T data, int pageNumber, int limit)
        {
            PageNumber = pageNumber;
            Limit = limit;
            Data = data;
            Message = null;
            Succeeded = true;
            Errors = null;
        }
        
    }
}