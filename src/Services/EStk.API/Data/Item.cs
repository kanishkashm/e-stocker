namespace EStk.API.Data
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public int StockId { get; set; }
    }
}
