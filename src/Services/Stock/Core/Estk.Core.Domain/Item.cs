namespace Estk.Core.Domain
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public int StockId { get; set; }
        public virtual Stock Stock { get; set; }
    }
}
