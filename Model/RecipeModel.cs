namespace Model
{
    public class RecipeModel
    {
        public string recipe_id { get; set; }
        public string item_id { get; set; }
        public string ingredient_id { get; set; }
        public double quantity_required { get; set; }
        public string? ingredient_name { get; set; } 
        public string? unit { get; set; } 
    }
}