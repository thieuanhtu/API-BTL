using Model;
using System.Collections.Generic;

namespace BLL
{
    public interface IItemGroupBusiness
    {
        bool Create(ItemGroupModel model);
        bool Update(ItemGroupModel model);
        bool Delete(string id);
        ItemGroupModel GetDatabyID(string id);
        List<ItemGroupModel> Search(int pageIndex, int pageSize, out long total, string itemGroupName);
    }
}