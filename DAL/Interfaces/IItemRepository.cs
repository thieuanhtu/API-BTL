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
        List<ItemModel> Search(int pageIndex, int pageSize, out long total, string item_group_id, string item_name);
    }
}