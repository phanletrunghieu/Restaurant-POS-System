using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ReportType
    {
        public int? RawDateCreated;
        public decimal? RawTotalRevenue;

        public int DateCreated
        {
            get
            {
                if (this.RawDateCreated == null)
                    return 0;
                return (int)RawDateCreated;
            }
        }

        public decimal TotalRevenue
        {
            get
            {
                if (this.RawTotalRevenue == null)
                    return 0;
                return (decimal)RawTotalRevenue;
            }
        }
    }

    public class AnalyticsBLL
    {
        public List<ReportType> GetAnalyticsByMonth(int month, int year)
        {
            var report = from o in Connection.DBContext.Orders
                         where o.DateCreated.Value.Month == month && o.DateCreated.Value.Year == year
                         group o by new { DateCreated = o.DateCreated.Value.Month } into oGroup
                         select new ReportType
                         {
                             RawDateCreated = oGroup.Key.DateCreated,
                             RawTotalRevenue = oGroup.Sum(x => x.PriceAfter)
                         };
            return report.ToList();
        }
    }
}
