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
        public bool AddComboItem(string comboId, string itemId, int quantity) => _itemRepository.AddComboItem(comboId, itemId, quantity);

        public bool RemoveComboItem(string comboId, string itemId) => _itemRepository.RemoveComboItem(comboId, itemId);

        public List<ComboItemModel> GetComboItems(string comboId) => _itemRepository.GetComboItems(comboId);

        public List<ItemModel> Search(int pageIndex, int pageSize, out long total, string item_group_id, string item_name)
            => _itemRepository.Search(pageIndex, pageSize, out total, item_group_id, item_name);
    }
}