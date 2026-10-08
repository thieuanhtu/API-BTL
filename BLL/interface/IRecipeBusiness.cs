using Model;
using System.Collections.Generic;

namespace BLL
{
    public interface IRecipeBusiness
    {
        bool Create(RecipeModel model);
        bool Update(RecipeModel model);
        bool Delete(string id);
        RecipeModel GetDatabyID(string id);
        List<RecipeModel> GetByItemID(string itemId);
    }
}