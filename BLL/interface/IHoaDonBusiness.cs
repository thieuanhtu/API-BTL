using Model;
using System.Collections.Generic;

namespace BLL
{
    public interface IHoaDonBusiness
    {
        bool Create(HoaDonModel model);
        bool Update(HoaDonModel model);
        HoaDonModel GetDatabyID(string id);
        bool Delete(string id);
        List<HoaDonModel> Search(int pageIndex, int pageSize, out long total, string hoten, string diachi);
    }
}