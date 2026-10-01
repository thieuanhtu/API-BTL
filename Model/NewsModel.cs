using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class NewsModel
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}