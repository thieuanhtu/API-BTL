using DAL;
using DAL.Interfaces;
using Model;
using System.Collections.Generic;

namespace BLL
{
    public class NewsBusiness : INewsBusiness
    {
        private readonly INewsRepository _newsRepository;

        public NewsBusiness(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public NewsModel GetDatabyID(int id) => _newsRepository.GetDatabyID(id);

        public bool Create(NewsModel model) => _newsRepository.Create(model);

        public bool Update(NewsModel model) => _newsRepository.Update(model);

        public bool Delete(int id) => _newsRepository.Delete(id);

        public List<NewsModel> Search(int pageIndex, int pageSize, out long total, string title)
            => _newsRepository.Search(pageIndex, pageSize, out total, title);
    }
}