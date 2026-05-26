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
    public interface IVoucherHandler
    {
        Task<Response<Voucher?>> GetByNumberAsync(GetVoucherByNumberRequest request);
    }
}
