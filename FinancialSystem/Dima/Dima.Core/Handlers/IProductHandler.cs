using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Handlers
{
    internal interface IProductHandler
    {
        Task<PagedResponse<List<Product>?>> GetAllAsync(GetAllProductsRequest request);
        Task<Response<Product?>> GetBySlugAsync(GetProductBySlugRequest request);
    }
}
