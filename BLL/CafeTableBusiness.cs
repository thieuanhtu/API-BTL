using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class CafeTableBusiness : ICafeTableBusiness
    {
        private static readonly string[] TrangThaiHopLe = { "Trống", "Đang phục vụ", "Đã đặt" };

        private readonly ICafeTableRepository _res;

        public CafeTableBusiness(ICafeTableRepository res)
        {
            _res = res;
        }

        private static void Kiemtra(CafeTableModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.table_id))
                throw new ArgumentException("Mã bàn không được để trống");
            if (string.IsNullOrWhiteSpace(model.status))
                model.status = "Trống";
            if (!TrangThaiHopLe.Contains(model.status))
                throw new ArgumentException("Trạng thái phải là: Trống, Đang phục vụ hoặc Đã đặt");
        }

        public CafeTableModel GetDatabyID(string id) => _res.GetDatabyID(id);

        public bool Create(CafeTableModel model)
        {
            Kiemtra(model);
            return _res.Create(model);
        }

        public bool Update(CafeTableModel model)
        {
            Kiemtra(model);
            return _res.Update(model);
        }

        public List<CafeTableModel> GetDataAll() => _res.GetDataAll();
    }
}