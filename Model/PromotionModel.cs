using System;

namespace Model
{
    public class PromotionModel
    {
        public string promotion_id { get; set; }
        public string promotion_name { get; set; }
        public string discount_type { get; set; } 
        public double discount_value { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public string? status { get; set; }
    }
}