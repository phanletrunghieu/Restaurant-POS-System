using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ReportType
    {
        public string RawDateCreated;
        public decimal? RawTotalRevenue;
        public int numOrder;

        public string DateCreated
        {
            get { return RawDateCreated; }
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

        public int NumOrder
        {
            get { return numOrder; }
            set { numOrder = value; }
        }
    }

    public class AnalyticsBLL
    {
        public List<ReportType> GetAnalyticsByMonth(int month, int year)
        {
            var report = from o in Connection.DBContext.Orders
                         where o.DateCreated.Value.Month == month && o.DateCreated.Value.Year == year
                         group o by new { DateCreated = o.DateCreated.Value.Day } into oGroup
                         select new ReportType
                         {
                             RawDateCreated = oGroup.Key.DateCreated+"/"+month+"/"+year,
                             RawTotalRevenue = oGroup.Sum(x => x.PriceAfter),
                             NumOrder = oGroup.Count(),
                         };
            return report.ToList();
        }

        public List<ReportType> GetAnalyticsByYear(int year)
        {
            var report = from o in Connection.DBContext.Orders
                         where o.DateCreated.Value.Year == year
                         group o by new { DateCreated = o.DateCreated.Value.Month } into oGroup
                         select new ReportType
                         {
                             RawDateCreated = oGroup.Key.DateCreated+"/"+year,
                             RawTotalRevenue = oGroup.Sum(x => x.PriceAfter),
                             NumOrder = oGroup.Count(),
                         };
            return report.ToList();
        }
    }
}
