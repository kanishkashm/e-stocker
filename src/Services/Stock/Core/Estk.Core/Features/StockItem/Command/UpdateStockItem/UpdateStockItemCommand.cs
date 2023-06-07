using MediatR;

namespace Estk.Core.Features.StockItem.Command.UpdateStockItem
{
    public class UpdateStockItemCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
