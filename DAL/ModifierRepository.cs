using DAL.Helper.Interfaces;
using DAL.Interfaces;
using Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DAL
{
    public class ModifierRepository : IModifierRepository
    {
        private readonly IDatabaseHelper _dbHelper;

        public ModifierRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public List<ModifierModel> GetAll(string itemGroupId)
        {
            return _dbHelper.Query<ModifierModel>("sp_modifier_get_all", new
            {
                item_group_id = itemGroupId
            }, CommandType.StoredProcedure).ToList();
        }

        public bool Create(ModifierModel model)
        {
            _dbHelper.Execute("sp_modifier_create", new
            {
                model.modifier_id,
                model.modifier_name,
                model.extra_price,
                item_group_id = model.item_group_id ?? ""
            }, CommandType.StoredProcedure);
            return true;
        }

        public bool Update(ModifierModel model)
        {
            _dbHelper.Execute("sp_modifier_update", new
            {
                model.modifier_id,
                model.modifier_name,
                model.extra_price,
                item_group_id = model.item_group_id ?? ""
            }, CommandType.StoredProcedure);
            return true;
        }

        public bool Delete(string id)
        {
            _dbHelper.Execute("sp_modifier_delete", new { modifier_id = id }, CommandType.StoredProcedure);
            return true;
        }

        public bool SetForOrderItem(string maChiTiet, List<string> listModifierId)
        {
            _dbHelper.Execute("sp_order_item_modifier_set", new
            {
                ma_chi_tiet = maChiTiet,
                list_modifier_id = JsonConvert.SerializeObject(listModifierId ?? new List<string>())
            }, CommandType.StoredProcedure);
            return true;
        }

        public List<OrderItemModifierModel> GetByOrderItem(string maChiTiet)
        {
            return _dbHelper.Query<OrderItemModifierModel>("sp_order_item_modifier_get", new
            {
                ma_chi_tiet = maChiTiet
            }, CommandType.StoredProcedure).ToList();
        }
    }
}