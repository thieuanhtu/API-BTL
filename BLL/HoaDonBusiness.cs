using DAL;
using Model;
using System.Collections.Generic;

namespace BLL
{
    public class HoaDonBusiness : IHoaDonBusiness
    {
        private readonly IHoaDonRepository _res;

        public HoaDonBusiness(IHoaDonRepository res)
        {
            _res = res;
        }
        public bool TransferTable(string maHoaDon, string newTableId) => _res.TransferTable(maHoaDon, newTableId);

        public bool Merge(string maHoaDonMain, string maHoaDonSub) => _res.Merge(maHoaDonMain, maHoaDonSub);

        public bool Split(string maHoaDonSource, string maHoaDonNew, string newTableId, List<string> listMaChiTiet)
            => _res.Split(maHoaDonSource, maHoaDonNew, newTableId, listMaChiTiet);

        public bool Pay(string maHoaDon) => _res.Pay(maHoaDon);

        public bool Create(HoaDonModel model) => _res.Create(model);
        public bool Update(HoaDonModel model) => _res.Update(model);
        public HoaDonModel GetDatabyID(string id) => _res.GetDatabyID(id);
        public bool Delete(string id) => _res.Delete(id);
        public List<HoaDonModel> Search(int pageIndex, int pageSize, out long total, string hoten, string diachi)
            => _res.Search(pageIndex, pageSize, out total, hoten, diachi);
    }
}