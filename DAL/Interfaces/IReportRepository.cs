using Model;
using System;
using System.Collections.Generic;

namespace DAL
{
    public interface IReportRepository
    {
        List<RevenueReportModel> GetRevenue(DateTime fromDate, DateTime toDate, string ca);
        List<BestSellerModel> GetBestSellers(DateTime fromDate, DateTime toDate, int top);
    }
}