using DAL.Helper.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DAL
{
    public class ReportRepository : IReportRepository
    {
        private readonly IDatabaseHelper _dbHelper;

        public ReportRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public List<RevenueReportModel> GetRevenue(DateTime fromDate, DateTime toDate, string ca)
        {
            return _dbHelper.Query<RevenueReportModel>("sp_report_revenue", new
            {
                from_date = fromDate,
                to_date = toDate,
                ca = ca
            }, CommandType.StoredProcedure).ToList();
        }

        public List<BestSellerModel> GetBestSellers(DateTime fromDate, DateTime toDate, int top)
        {
            return _dbHelper.Query<BestSellerModel>("sp_report_best_sellers", new
            {
                from_date = fromDate,
                to_date = toDate,
                top = top
            }, CommandType.StoredProcedure).ToList();
        }
    }
}