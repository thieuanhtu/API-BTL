using DAL.Helper.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class NewsRepository : INewsRepository
    {
        private readonly IDatabaseHelper _dbHelper;

        public NewsRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public bool Create(NewsModel model)
        {
            string msgError = "";
            _dbHelper.ExecuteScalar("sp_news_create", out msgError,
                "@news_id", model.NewsId,
                "@title", model.Title,
                "@content", model.Content);
            return string.IsNullOrEmpty(msgError);
        }

        public bool Update(NewsModel model)
        {
            string msgError = "";
            _dbHelper.ExecuteScalar("sp_news_update", out msgError,
                "@news_id", model.NewsId,
                "@title", model.Title,
                "@content", model.Content);
            return string.IsNullOrEmpty(msgError);
        }

        public bool Delete(string id)
        {
            string msgError = "";
            _dbHelper.ExecuteScalar("sp_news_delete", out msgError, "@news_id", id);
            return string.IsNullOrEmpty(msgError);
        }

        public NewsModel GetDatabyID(string id)
        {
            string msgError = "";
            var dt = _dbHelper.ExecuteQuery("sp_news_get_by_id", out msgError, "@news_id", id);
            if (!string.IsNullOrEmpty(msgError)) throw new Exception(msgError);

            var news = new NewsModel();
            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                news.NewsId = Convert.ToInt32(row["news_id"]);
                news.Title = row["title"].ToString();
                news.Content = row["content"].ToString();
            }
            return news;
        }

        public List<NewsModel> Search(int pageIndex, int pageSize, out long total, string title)
        {
            string msgError = "";
            total = 0;
            var dt = _dbHelper.ExecuteQuery("sp_news_search", out msgError,
                "@page_index", pageIndex,
                "@page_size", pageSize,
                "@title", title);

            if (!string.IsNullOrEmpty(msgError)) throw new Exception(msgError);

            var list = new List<NewsModel>();
            if (dt.Rows.Count > 0)
            {
                total = Convert.ToInt64(dt.Rows[0]["RecordCount"]);
                foreach (DataRow row in dt.Rows)
                {
                    var news = new NewsModel();
                    news.NewsId = Convert.ToInt32(row["news_id"]);
                    news.Title = row["title"].ToString();
                    news.Content = row["content"].ToString();
                    list.Add(news);
                }
            }
            return list;
        }
    }
}