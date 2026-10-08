using DAL;
using DAL.Interfaces;
using Model;
using System.Collections.Generic;

namespace BLL
{
    public class RecipeBusiness : IRecipeBusiness
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeBusiness(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public bool Create(RecipeModel model) => _recipeRepository.Create(model);

        public bool Update(RecipeModel model) => _recipeRepository.Update(model);

        public bool Delete(string id) => _recipeRepository.Delete(id);

        public RecipeModel GetDatabyID(string id) => _recipeRepository.GetDatabyID(id);

        public List<RecipeModel> GetByItemID(string itemId) => _recipeRepository.GetByItemID(itemId);
    }
}