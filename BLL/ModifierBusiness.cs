using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class ModifierBusiness : IModifierBusiness
    {
        private readonly IModifierRepository _res;

        public ModifierBusiness(IModifierRepository res)
        {
            _res = res;
        }

        private static void Kiemtra(ModifierModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.modifier_id))
                throw new ArgumentException("Mã lựa chọn không được để trống");
            if (string.IsNullOrWhiteSpace(model.modifier_name))
                throw new ArgumentException("Tên lựa chọn không được để trống");
            if (model.extra_price < 0)
                throw new ArgumentException("Giá cộng thêm không được âm");
        }

        public List<ModifierModel> GetAll(string itemGroupId) => _res.GetAll(itemGroupId);

        public bool Create(ModifierModel model)
        {
            Kiemtra(model);
            return _res.Create(model);
        }

        public bool Update(ModifierModel model)
        {
            Kiemtra(model);
            return _res.Update(model);
        }

        public bool Delete(string id) => _res.Delete(id);

        public bool SetForOrderItem(string maChiTiet, List<string> listModifierId)
        {
            if (string.IsNullOrWhiteSpace(maChiTiet))
                throw new ArgumentException("Mã dòng món không được để trống");
            return _res.SetForOrderItem(maChiTiet, listModifierId);
        }

        public List<OrderItemModifierModel> GetByOrderItem(string maChiTiet) => _res.GetByOrderItem(maChiTiet);
    }
}