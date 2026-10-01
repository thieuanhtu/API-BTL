using System;
using System.Collections.Generic;

namespace Model
{
    public class ChiTietHoaDonModel
    {
        public string ma_chi_tiet { get; set; } = string.Empty;
        public string ma_hoa_don { get; set; } = string.Empty;
        public string item_id { get; set; } = string.Empty;
        public string item_name { get; set; } = string.Empty;
        public int so_luong { get; set; }
        public float unit_price { get; set; }
        public string? ghi_chu_mon { get; set; }
    }
}