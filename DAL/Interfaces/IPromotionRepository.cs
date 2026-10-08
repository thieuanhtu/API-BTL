using Model;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IPromotionRepository
    {
        bool Create(PromotionModel model);
        bool Update(PromotionModel model);
        bool Delete(string id);
        PromotionModel GetDatabyID(string id);
        List<PromotionModel> Search(int pageIndex, int pageSize, out long total, string promotionName);
        bool ApplyToHoaDon(string maHoaDon, string promotionId);
    }
}