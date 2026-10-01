using DAL.Helper;
using DAL.Helper.Interfaces;
using Model;
using System.Collections.Generic;
using System.Data;
using System.Linq;



namespace DAL
{
    public class HoaDonRepository : IHoaDonRepository
    {
        private readonly IDatabaseHelper _dbHelper;
        private sealed class HoaDonSearchRow
        {
            public string ma_hoa_don { get; set; }
            public string table_id { get; set; }
            public string user_id { get; set; }
            public string ho_ten { get; set; }
            public string dia_chi { get; set; }
            public string status { get; set; }
            public DateTime ngay_tao { get; set; }
            public string listjson_chitiet { get; set; }
            public long RecordCount { get; set; }
        }

        public List<HoaDonModel> Search(int pageIndex, int pageSize, out long total, string hoten, string diachi)
        {
            var rows = _dbHelper.Query<HoaDonSearchRow>("sp_hoa_don_search", new
            {
                page_index = pageIndex,
                page_size = pageSize,
                hoten,
                diachi
            }, CommandType.StoredProcedure).ToList();

            total = rows.FirstOrDefault()?.RecordCount ?? 0;

            return rows.Select(r => new HoaDonModel
            {
                ma_hoa_don = r.ma_hoa_don,
                table_id = r.table_id,
                user_id = r.user_id,
                ho_ten = r.ho_ten,
                dia_chi = r.dia_chi,
                status = r.status,
                ngay_tao = r.ngay_tao,
                listjson_chitiet = !string.IsNullOrEmpty(r.listjson_chitiet)
                    ? Newtonsoft.Json.JsonConvert.DeserializeObject<List<ChiTietHoaDonModel>>(r.listjson_chitiet)
                    : null
            }).ToList();
        }



        public HoaDonRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public bool Create(HoaDonModel model)
        {
            _dbHelper.Execute("sp_hoa_don_create", new
            {
                model.ma_hoa_don,
                model.table_id,
                model.user_id,
                model.ho_ten,
                model.dia_chi,
                listjson_chitiet = model.listjson_chitiet != null ? Newtonsoft.Json.JsonConvert.SerializeObject(model.listjson_chitiet) : null
            }, CommandType.StoredProcedure);
            return true;
        }

        public bool Update(HoaDonModel model)
        {
            _dbHelper.Execute("sp_hoa_don_update", new
            {
                model.ma_hoa_don,
                model.table_id,
                model.user_id,
                model.ho_ten,
                model.dia_chi,
                listjson_chitiet = model.listjson_chitiet != null ? Newtonsoft.Json.JsonConvert.SerializeObject(model.listjson_chitiet) : null
            }, CommandType.StoredProcedure);
            return true;
        }

        public bool Delete(string id)
        {
            _dbHelper.Execute("sp_hoa_don_delete", new { ma_hoa_don = id }, CommandType.StoredProcedure);
            return true;
        }

        // Class tạm để nhận đúng kiểu string thô từ SQL trả về (không kế thừa HoaDonModel, tránh trùng tên field)
        private sealed class HoaDonRawRow
        {
            public string ma_hoa_don { get; set; }
            public string table_id { get; set; }
            public string user_id { get; set; }
            public string ho_ten { get; set; }
            public string dia_chi { get; set; }
            public string status { get; set; }
            public DateTime ngay_tao { get; set; }
            public string listjson_chitiet { get; set; }
        }

        public HoaDonModel GetDatabyID(string id)
        {
            var raw = _dbHelper.QueryFirstOrDefault<HoaDonRawRow>("sp_hoa_don_get_by_id",
                new { ma_hoa_don = id }, CommandType.StoredProcedure);

            if (raw == null) return null;

            return new HoaDonModel
            {
                ma_hoa_don = raw.ma_hoa_don,
                table_id = raw.table_id,
                user_id = raw.user_id,
                ho_ten = raw.ho_ten,
                dia_chi = raw.dia_chi,
                status = raw.status,
                ngay_tao = raw.ngay_tao,
                listjson_chitiet = !string.IsNullOrEmpty(raw.listjson_chitiet)
                    ? Newtonsoft.Json.JsonConvert.DeserializeObject<List<ChiTietHoaDonModel>>(raw.listjson_chitiet)
                    : null
            };
        }


    }
}