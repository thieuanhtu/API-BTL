namespace Model
{
    public class ItemModel
    {
        public string item_id { get; set; }
        public string item_group_id { get; set; }
        public string item_name { get; set; } = string.Empty;
        public string item_image { get; set; } = string.Empty;
        public decimal item_price { get; set; }
    }
}