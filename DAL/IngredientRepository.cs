using DAL.Helper.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly IDatabaseHelper _dbHelper;

        public IngredientRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public bool Create(IngredientModel model)
        {
            string msgError = "";
            _dbHelper.ExecuteScalar("sp_ingredient_create", out msgError,
                "@ingredient_id", model.ingredient_id,
                "@ingredient_name", model.ingredient_name,
                "@unit", model.unit,
                "@stock_quantity", model.stock_quantity);
            return string.IsNullOrEmpty(msgError);
        }

        public bool Update(IngredientModel model)
        {
            string msgError = "";
            _dbHelper.ExecuteScalar("sp_ingredient_update", out msgError,
                "@ingredient_id", model.ingredient_id,
                "@ingredient_name", model.ingredient_name,
                "@unit", model.unit,
                "@stock_quantity", model.stock_quantity);
            return string.IsNullOrEmpty(msgError);
        }

        public bool Delete(string id)
        {
            string msgError = "";
            _dbHelper.ExecuteScalar("sp_ingredient_delete", out msgError, "@ingredient_id", id);
            return string.IsNullOrEmpty(msgError);
        }

        public IngredientModel GetDatabyID(string id)
        {
            string msgError = "";
            var dt = _dbHelper.ExecuteQuery("sp_ingredient_get_by_id", out msgError, "@ingredient_id", id);
            if (!string.IsNullOrEmpty(msgError)) throw new Exception(msgError);

            var ing = new IngredientModel();
            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                ing.ingredient_id = row["ingredient_id"].ToString();
                ing.ingredient_name = row["ingredient_name"].ToString();
                ing.unit = row["unit"].ToString();
                ing.stock_quantity = Convert.ToDouble(row["stock_quantity"]);
            }
            return ing;
        }

        public List<IngredientModel> Search(int pageIndex, int pageSize, out long total, string ingredientName)
        {
            string msgError = "";
            total = 0;
            var dt = _dbHelper.ExecuteQuery("sp_ingredient_search", out msgError,
                "@page_index", pageIndex,
                "@page_size", pageSize,
                "@ingredient_name", ingredientName);

            if (!string.IsNullOrEmpty(msgError)) throw new Exception(msgError);

            var list = new List<IngredientModel>();
            if (dt.Rows.Count > 0)
            {
                total = Convert.ToInt64(dt.Rows[0]["RecordCount"]);
                foreach (DataRow row in dt.Rows)
                {
                    var ing = new IngredientModel();
                    ing.ingredient_id = row["ingredient_id"].ToString();
                    ing.ingredient_name = row["ingredient_name"].ToString();
                    ing.unit = row["unit"].ToString();
                    ing.stock_quantity = Convert.ToDouble(row["stock_quantity"]);
                    list.Add(ing);
                }
            }
            return list;
        }
    }
}