using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class ReportBusiness : IReportBusiness
    {
        private readonly IReportRepository _repo;

        public ReportBusiness(IReportRepository repo)
        {
            _repo = repo;
        }

        public List<RevenueReportModel> GetRevenue(DateTime fromDate, DateTime toDate, string ca)
        {
            if (fromDate > toDate)
                throw new ArgumentException("Từ ngày phải nhỏ hơn hoặc bằng đến ngày");

            if (!string.IsNullOrWhiteSpace(ca))
            {
                var hopLe = new[] { "Sáng", "Chiều", "Tối", "Ngoài ca" };
                if (!hopLe.Contains(ca))
                    throw new ArgumentException("Ca phải là: Sáng, Chiều, Tối hoặc Ngoài ca");
            }
            else
            {
                ca = null;
            }

            return _repo.GetRevenue(fromDate, toDate, ca);
        }

        public List<BestSellerModel> GetBestSellers(DateTime fromDate, DateTime toDate, int top)
        {
            if (fromDate > toDate)
                throw new ArgumentException("Từ ngày phải nhỏ hơn hoặc bằng đến ngày");
            if (top <= 0) top = 10;
            return _repo.GetBestSellers(fromDate, toDate, top);
        }
    }
}