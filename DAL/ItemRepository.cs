using DAL.Helper.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class ItemRepository : IItemRepository
    {
        private readonly IDatabaseHelper _dbHelper;

        public ItemRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public bool Create(ItemModel model)
        {
            string msgError = "";
            var result = _dbHelper.ExecuteScalar("sp_item_create", out msgError,
                "@item_id", model.item_id,
                "@item_group_id", model.item_group_id,
                "@item_name", model.item_name,
                "@item_image", model.item_image,
                "@item_price", model.item_price);

            if (!string.IsNullOrEmpty(msgError)) return false;
            return true;
        }

        public bool Update(ItemModel model)
        {
            string msgError = "";
            var result = _dbHelper.ExecuteScalar("sp_item_update", out msgError,
                "@item_id", model.item_id,
                "@item_group_id", model.item_group_id,
                "@item_name", model.item_name,
                "@item_image", model.item_image,
                "@item_price", model.item_price);

            if (!string.IsNullOrEmpty(msgError)) return false;
            return true;
        }
        public bool Delete(string id)
        {
            string msgError = "";
            var result = _dbHelper.ExecuteScalar("sp_item_delete", out msgError, "@item_id", id);
            if (!string.IsNullOrEmpty(msgError)) return false;
            return true;
        }

        public ItemModel GetDatabyID(string id)
        {
            string msgError = "";
            var dt = _dbHelper.ExecuteQuery("sp_item_get_by_id", out msgError, "@item_id", id);
            if (!string.IsNullOrEmpty(msgError)) throw new Exception(msgError);

            var item = new ItemModel();
            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                item.item_id = row["item_id"].ToString();
                item.item_group_id = row["item_group_id"].ToString();
                item.item_name = row["item_name"].ToString();
                item.item_image = row["item_image"].ToString();
                item.item_price = Convert.ToDecimal(row["item_price"]);
            }
            return item;
        }

        public List<ItemModel> Search(int pageIndex, int pageSize, out long total, string item_group_id, string item_name)
        {
            string msgError = "";
            total = 0;
            var dt = _dbHelper.ExecuteQuery("sp_item_search", out msgError,
                "@page_index", pageIndex,
                "@page_size", pageSize,
                "@item_group_id", item_group_id,
                "@item_name", item_name);

            if (!string.IsNullOrEmpty(msgError)) throw new Exception(msgError);

            var list = new List<ItemModel>();
            if (dt.Rows.Count > 0)
            {
                total = Convert.ToInt64(dt.Rows[0]["RecordCount"]);
                foreach (DataRow row in dt.Rows)
                {
                    var item = new ItemModel();
                    item.item_id = row["item_id"].ToString();
                    item.item_group_id = row["item_group_id"].ToString();
                    item.item_name = row["item_name"].ToString();
                    item.item_image = row["item_image"].ToString();
                    item.item_price = Convert.ToDecimal(row["item_price"]);
                    list.Add(item);
                }
            }
            return list;
        }
        public bool AddComboItem(string comboId, string itemId, int quantity)
        {
            string msgError = "";
            _dbHelper.ExecuteScalar("sp_combo_item_add", out msgError,
                "@combo_id", comboId, "@item_id", itemId, "@quantity", quantity);
            return string.IsNullOrEmpty(msgError);
        }

        public bool RemoveComboItem(string comboId, string itemId)
        {
            string msgError = "";
            _dbHelper.ExecuteScalar("sp_combo_item_remove", out msgError,
                "@combo_id", comboId, "@item_id", itemId);
            return string.IsNullOrEmpty(msgError);
        }

        public List<ComboItemModel> GetComboItems(string comboId)
        {
            string msgError = "";
            var dt = _dbHelper.ExecuteQuery("sp_combo_get_items", out msgError, "@combo_id", comboId);
            if (!string.IsNullOrEmpty(msgError)) throw new Exception(msgError);

            var list = new List<ComboItemModel>();
            foreach (DataRow row in dt.Rows)
            {
                var ci = new ComboItemModel();
                ci.combo_id = row["combo_id"].ToString();
                ci.item_id = row["item_id"].ToString();
                ci.quantity = Convert.ToInt32(row["quantity"]);
                ci.item_name = row["item_name"].ToString();
                ci.item_price = Convert.ToDecimal(row["item_price"]);
                list.Add(ci);
            }
            return list;
        }
    }
}