using System;
using System.Collections.Generic;

namespace Model
{
    public class HoaDonModel
    {
        public string ma_hoa_don { get; set; }
        public string table_id { get; set; }
        public string user_id { get; set; }
        public string ho_ten { get; set; }
        public string dia_chi { get; set; }
        public string status { get; set; }
        public DateTime ngay_tao { get; set; }
        public List<ChiTietHoaDonModel> listjson_chitiet { get; set; }
    }
}