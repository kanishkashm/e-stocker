namespace EStk.API.Data
{
    public class Stock
    {
        public int Id { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        public bool Availability { get; set; } = true;
        public string TakenBy { get; set; } = string.Empty;
    }
}
