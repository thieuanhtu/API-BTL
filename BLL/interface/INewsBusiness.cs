using Model;
using System.Collections.Generic;

namespace BLL
{
    public interface INewsBusiness
    {
        NewsModel GetDatabyID(int id);
        bool Create(NewsModel model);
        bool Update(NewsModel model);
        bool Delete(int id);
        List<NewsModel> Search(int pageIndex, int pageSize, out long total, string title);
    }
}