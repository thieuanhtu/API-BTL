using DAL.Helper.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly IDatabaseHelper _dbHelper;

        public PromotionRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public bool Create(PromotionModel model)
        {
            string msgError = "";
            _dbHelper.ExecuteScalar("sp_promotion_create", out msgError,
                "@promotion_id", model.promotion_id,
                "@promotion_name", model.promotion_name,
                "@discount_type", model.discount_type,
                "@discount_value", model.discount_value,
                "@start_date", model.start_date,
                "@end_date", model.end_date);
            return string.IsNullOrEmpty(msgError);
        }

        public bool Update(PromotionModel model)
        {
            string msgError = "";
            _dbHelper.ExecuteScalar("sp_promotion_update", out msgError,
                "@promotion_id", model.promotion_id,
                "@promotion_name", model.promotion_name,
                "@discount_type", model.discount_type,
                "@discount_value", model.discount_value,
                "@start_date", model.start_date,
                "@end_date", model.end_date,
                "@status", model.status);
            return string.IsNullOrEmpty(msgError);
        }

        public bool Delete(string id)
        {
            string msgError = "";
            _dbHelper.ExecuteScalar("sp_promotion_delete", out msgError, "@promotion_id", id);
            return string.IsNullOrEmpty(msgError);
        }

        public PromotionModel GetDatabyID(string id)
        {
            string msgError = "";
            var dt = _dbHelper.ExecuteQuery("sp_promotion_get_by_id", out msgError, "@promotion_id", id);
            if (!string.IsNullOrEmpty(msgError)) throw new Exception(msgError);

            var promo = new PromotionModel();
            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                promo.promotion_id = row["promotion_id"].ToString();
                promo.promotion_name = row["promotion_name"].ToString();
                promo.discount_type = row["discount_type"].ToString();
                promo.discount_value = Convert.ToDouble(row["discount_value"]);
                promo.start_date = Convert.ToDateTime(row["start_date"]);
                promo.end_date = Convert.ToDateTime(row["end_date"]);
                promo.status = row["status"].ToString();
            }
            return promo;
        }

        public List<PromotionModel> Search(int pageIndex, int pageSize, out long total, string promotionName)
        {
            string msgError = "";
            total = 0;
            var dt = _dbHelper.ExecuteQuery("sp_promotion_search", out msgError,
                "@page_index", pageIndex,
                "@page_size", pageSize,
                "@promotion_name", promotionName);

            if (!string.IsNullOrEmpty(msgError)) throw new Exception(msgError);

            var list = new List<PromotionModel>();
            if (dt.Rows.Count > 0)
            {
                total = Convert.ToInt64(dt.Rows[0]["RecordCount"]);
                foreach (DataRow row in dt.Rows)
                {
                    var promo = new PromotionModel();
                    promo.promotion_id = row["promotion_id"].ToString();
                    promo.promotion_name = row["promotion_name"].ToString();
                    promo.discount_type = row["discount_type"].ToString();
                    promo.discount_value = Convert.ToDouble(row["discount_value"]);
                    promo.start_date = Convert.ToDateTime(row["start_date"]);
                    promo.end_date = Convert.ToDateTime(row["end_date"]);
                    promo.status = row["status"].ToString();
                    list.Add(promo);
                }
            }
            return list;
        }

        public bool ApplyToHoaDon(string maHoaDon, string promotionId)
        {
            string msgError = "";
            _dbHelper.ExecuteScalar("sp_hoa_don_apply_promotion", out msgError,
                "@ma_hoa_don", maHoaDon, "@promotion_id", promotionId);
            return string.IsNullOrEmpty(msgError);
        }
    }
}