using System.ComponentModel.DataAnnotations;

namespace Estk.Core.Domain
{
    public class Stock
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Item> Items { get; set; }
        public bool Availability { get; set; } = true;
        public string TakenBy { get; set; } = string.Empty;

        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}
