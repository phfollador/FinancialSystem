using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dima.Core.Responses
{
    public class PagedResponse<T> : Response<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public int PageSize { get; set; } = Configuration.DefaultPageSige;
        public int TotalCount { get; set; }

        public PagedResponse(T? data, int code = Configuration.defaultStatusCode, string message = null) : base(data, code, message)
        {
            
        }

        [JsonConstructor]
        public PagedResponse(T? data, int totalCount, int currentPage, int pageSize = Configuration.DefaultPageSige) : base(data) 
        {
            Data = data;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
    }
}