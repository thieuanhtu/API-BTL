using System;

namespace Model
{
    public class KitchenTicketModel
    {
        public string ticket_id { get; set; }
        public string ma_hoa_don { get; set; }
        public string station { get; set; } 
        public string? status { get; set; } 
        public DateTime created_at { get; set; }
    }
}