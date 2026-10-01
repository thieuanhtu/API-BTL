using DAL;
using DAL.Interfaces;
using Model;
using System.Collections.Generic;

namespace BLL
{
    public class ItemBusiness : IItemBusiness
    {
        private readonly IItemRepository _itemRepository;

        public ItemBusiness(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public ItemModel GetDatabyID(string id) => _itemRepository.GetDatabyID(id);

        public bool Create(ItemModel model) => _itemRepository.Create(model);

        public bool Update(ItemModel model) => _itemRepository.Update(model);

        public bool Delete(string id) => _itemRepository.Delete(id);

        public List<ItemModel> Search(int pageIndex, int pageSize, out long total, string item_group_id, string item_name)
            => _itemRepository.Search(pageIndex, pageSize, out total, item_group_id, item_name);
    }
}