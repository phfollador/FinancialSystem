using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Common.Extensions
{
    public static class DataTimeExtension
    {
        public static DateTime GetFirstDate(this DateTime date, int? year = null, int? month = null)
        {
            return new DateTime(year ?? date.Year, month ?? date.Month, 1);
        }

        public static DateTime GetLastDate(this DateTime date, int? year = null, int? month = null)
        {
            return new DateTime(year ?? date.Year, month ?? date.Month, 1).AddMonths(1).AddDays(-1);
        }
    }
}
