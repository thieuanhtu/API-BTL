using DAL.Helper.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly IDatabaseHelper _dbHelper;

        public RecipeRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public bool Create(RecipeModel model)
        {
            string msgError = "";
            _dbHelper.ExecuteScalar("sp_recipe_create", out msgError,
                "@recipe_id", model.recipe_id,
                "@item_id", model.item_id,
                "@ingredient_id", model.ingredient_id,
                "@quantity_required", model.quantity_required);
            return string.IsNullOrEmpty(msgError);
        }

        public bool Update(RecipeModel model)
        {
            string msgError = "";
            _dbHelper.ExecuteScalar("sp_recipe_update", out msgError,
                "@recipe_id", model.recipe_id,
                "@item_id", model.item_id,
                "@ingredient_id", model.ingredient_id,
                "@quantity_required", model.quantity_required);
            return string.IsNullOrEmpty(msgError);
        }

        public bool Delete(string id)
        {
            string msgError = "";
            _dbHelper.ExecuteScalar("sp_recipe_delete", out msgError, "@recipe_id", id);
            return string.IsNullOrEmpty(msgError);
        }

        public RecipeModel GetDatabyID(string id)
        {
            string msgError = "";
            var dt = _dbHelper.ExecuteQuery("sp_recipe_get_by_id", out msgError, "@recipe_id", id);
            if (!string.IsNullOrEmpty(msgError)) throw new Exception(msgError);

            var recipe = new RecipeModel();
            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                recipe.recipe_id = row["recipe_id"].ToString();
                recipe.item_id = row["item_id"].ToString();
                recipe.ingredient_id = row["ingredient_id"].ToString();
                recipe.quantity_required = Convert.ToDouble(row["quantity_required"]);
            }
            return recipe;
        }

        public List<RecipeModel> GetByItemID(string itemId)
        {
            string msgError = "";
            var dt = _dbHelper.ExecuteQuery("sp_recipe_get_by_item_id", out msgError, "@item_id", itemId);
            if (!string.IsNullOrEmpty(msgError)) throw new Exception(msgError);

            var list = new List<RecipeModel>();
            foreach (DataRow row in dt.Rows)
            {
                var recipe = new RecipeModel();
                recipe.recipe_id = row["recipe_id"].ToString();
                recipe.item_id = row["item_id"].ToString();
                recipe.ingredient_id = row["ingredient_id"].ToString();
                recipe.quantity_required = Convert.ToDouble(row["quantity_required"]);
                recipe.ingredient_name = row["ingredient_name"].ToString();
                recipe.unit = row["unit"].ToString();
                list.Add(recipe);
            }
            return list;
        }
    }
}