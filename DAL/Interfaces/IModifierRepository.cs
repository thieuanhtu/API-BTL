using Model;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IModifierRepository
    {
        List<ModifierModel> GetAll(string itemGroupId);
        bool Create(ModifierModel model);
        bool Update(ModifierModel model);
        bool Delete(string id);
        bool SetForOrderItem(string maChiTiet, List<string> listModifierId);
        List<OrderItemModifierModel> GetByOrderItem(string maChiTiet);
    }
}