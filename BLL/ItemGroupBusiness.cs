using DAL;
using Model;
using System.Collections.Generic;

namespace BLL
{
    public class ItemGroupBusiness : IItemGroupBusiness
    {
        private readonly IItemGroupRepository _itemGroupRepository;

        public ItemGroupBusiness(IItemGroupRepository itemGroupRepository)
        {
            _itemGroupRepository = itemGroupRepository;
        }

        public bool Create(ItemGroupModel model) => _itemGroupRepository.Create(model);

        public bool Update(ItemGroupModel model) => _itemGroupRepository.Update(model);

        public bool Delete(string id) => _itemGroupRepository.Delete(id);

        public ItemGroupModel GetDatabyID(string id) => _itemGroupRepository.GetDatabyID(id);

        public List<ItemGroupModel> Search(int pageIndex, int pageSize, out long total, string itemGroupName)
            => _itemGroupRepository.Search(pageIndex, pageSize, out total, itemGroupName);
    }
}