using Model;
using System;
using System.Collections.Generic;

namespace BLL
{
    public interface IReportBusiness
    {
        List<RevenueReportModel> GetRevenue(DateTime fromDate, DateTime toDate, string ca);
        List<BestSellerModel> GetBestSellers(DateTime fromDate, DateTime toDate, int top);
    }
}