using DAL;
using DAL.Interfaces;
using Model;
using System.Collections.Generic;

namespace BLL
{
    public class IngredientBusiness : IIngredientBusiness
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientBusiness(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public bool Create(IngredientModel model) => _ingredientRepository.Create(model);

        public bool Update(IngredientModel model) => _ingredientRepository.Update(model);

        public bool Delete(string id) => _ingredientRepository.Delete(id);

        public IngredientModel GetDatabyID(string id) => _ingredientRepository.GetDatabyID(id);

        public List<IngredientModel> Search(int pageIndex, int pageSize, out long total, string ingredientName)
            => _ingredientRepository.Search(pageIndex, pageSize, out total, ingredientName);
    }
}