using DAL;
using DAL.Interfaces;
using Model;
using System.Collections.Generic;

namespace BLL
{
    public class PromotionBusiness : IPromotionBusiness
    {
        private readonly IPromotionRepository _promotionRepository;

        public PromotionBusiness(IPromotionRepository promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        public bool Create(PromotionModel model) => _promotionRepository.Create(model);

        public bool Update(PromotionModel model) => _promotionRepository.Update(model);

        public bool Delete(string id) => _promotionRepository.Delete(id);

        public PromotionModel GetDatabyID(string id) => _promotionRepository.GetDatabyID(id);

        public List<PromotionModel> Search(int pageIndex, int pageSize, out long total, string promotionName)
            => _promotionRepository.Search(pageIndex, pageSize, out total, promotionName);

        public bool ApplyToHoaDon(string maHoaDon, string promotionId) => _promotionRepository.ApplyToHoaDon(maHoaDon, promotionId);
    }
}