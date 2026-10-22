using System;
using System.Collections.Generic;

namespace Model
{
    public class KitchenTicketModel
    {
        public string ticket_id { get; set; }
        public string ma_hoa_don { get; set; }
        public string station { get; set; }
        public string? status { get; set; }
        public DateTime created_at { get; set; }
        public string? table_id { get; set; }
        public List<KitchenTicketItemModel> listjson_items { get; set; } = new List<KitchenTicketItemModel>();
    }

    public class KitchenTicketItemModel
    {
        public string ma_chi_tiet { get; set; } = string.Empty;
        public string item_id { get; set; } = string.Empty;
        public string item_name { get; set; } = string.Empty;
        public int so_luong { get; set; }
        public string? ghi_chu_mon { get; set; }
        public List<ModifierLineModel> modifiers { get; set; } = new List<ModifierLineModel>();
    }
}