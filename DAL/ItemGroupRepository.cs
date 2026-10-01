using DAL.Helper.Interfaces;
using Model;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DAL
{
    public class ItemGroupRepository : IItemGroupRepository
    {
        private readonly IDatabaseHelper _dbHelper;

        private sealed class ItemGroupSearchRow : ItemGroupModel
        {
            public long RecordCount { get; set; }
        }

        public ItemGroupRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public bool Create(ItemGroupModel model)
        {
            _dbHelper.Execute("sp_item_group_create", new
            {
                model.item_group_id,
                model.parent_item_group_id,
                model.item_group_name,
                model.seq_num,
                model.url
            }, CommandType.StoredProcedure);
            return true;
        }

        public bool Update(ItemGroupModel model)
        {
            _dbHelper.Execute("sp_item_group_update", new
            {
                model.item_group_id,
                model.parent_item_group_id,
                model.item_group_name,
                model.seq_num,
                model.url
            }, CommandType.StoredProcedure);
            return true;
        }

        public bool Delete(string id)
        {
            _dbHelper.Execute("sp_item_group_delete", new { item_group_id = id }, CommandType.StoredProcedure);
            return true;
        }

        public ItemGroupModel GetDatabyID(string id)
        {
            return _dbHelper.QueryFirstOrDefault<ItemGroupModel>("sp_item_group_get_by_id",
                new { item_group_id = id }, CommandType.StoredProcedure);
        }

        public List<ItemGroupModel> Search(int pageIndex, int pageSize, out long total, string itemGroupName)
        {
            var rows = _dbHelper.Query<ItemGroupSearchRow>("sp_item_group_search", new
            {
                page_index = pageIndex,
                page_size = pageSize,
                item_group_name = itemGroupName
            }, CommandType.StoredProcedure).ToList();
            total = rows.FirstOrDefault()?.RecordCount ?? 0;
            return rows.Cast<ItemGroupModel>().ToList();
        }
    }
}