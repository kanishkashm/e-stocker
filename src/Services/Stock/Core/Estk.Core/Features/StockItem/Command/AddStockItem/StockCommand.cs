using MediatR;

namespace Estk.Core.Features.StockItem.Command.AddStockItem
{
    public class StockCommand : IRequest<int>
    {
        public string Name { get; set; }
        public ItemCommand Item { get; set; }
    }
}
