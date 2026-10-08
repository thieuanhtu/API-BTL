using Model;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IRecipeRepository
    {
        bool Create(RecipeModel model);
        bool Update(RecipeModel model);
        bool Delete(string id);
        RecipeModel GetDatabyID(string id);
        List<RecipeModel> GetByItemID(string itemId);
    }
}