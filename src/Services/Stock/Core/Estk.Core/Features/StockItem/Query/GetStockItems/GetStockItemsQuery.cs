using Estk.Core.Domain;
using MediatR;

namespace Estk.Core.Features.StockItem.Query.GetStockItems
{
    public class GetStockItemsQuery : IRequest<Stock>
    {
        public GetStockItemsQuery(int stockId)
        {
            StockId = stockId;
        }
        public int StockId { get; set; }
    }
}
