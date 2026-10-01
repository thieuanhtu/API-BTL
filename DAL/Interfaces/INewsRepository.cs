using Model;
using System.Collections.Generic;

namespace DAL
{
    public interface INewsRepository
    {
        NewsModel GetDatabyID(string id);
        bool Create(NewsModel model);
        bool Update(NewsModel model);
        bool Delete(string id);
        List<NewsModel> Search(int pageIndex, int pageSize, out long total, string title);
    }
}