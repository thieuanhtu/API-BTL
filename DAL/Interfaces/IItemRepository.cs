using Model;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IItemRepository
    {
        ItemModel GetDatabyID(string id);
        bool Create(ItemModel model);
        bool Update(ItemModel model);
        bool Delete(string id);
        bool AddComboItem(string comboId, string itemId, int quantity);
        bool RemoveComboItem(string comboId, string itemId);
        List<ComboItemModel> GetComboItems(string comboId);
        List<ItemModel> Search(int pageIndex, int pageSize, out long total, string item_group_id, string item_name);
    }
}