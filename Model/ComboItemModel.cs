namespace Model
{
    public class ComboItemModel
    {
        public string combo_id { get; set; }
        public string item_id { get; set; }
        public int quantity { get; set; }
        public string? item_name { get; set; }
        public decimal? item_price { get; set; }
    }
}