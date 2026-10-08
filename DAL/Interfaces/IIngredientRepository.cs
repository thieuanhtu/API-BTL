using Model;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IIngredientRepository
    {
        bool Create(IngredientModel model);
        bool Update(IngredientModel model);
        bool Delete(string id);
        IngredientModel GetDatabyID(string id);
        List<IngredientModel> Search(int pageIndex, int pageSize, out long total, string ingredientName);
    }
}