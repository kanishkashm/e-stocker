using Estk.Core.Domain;
using EStk.Core.Contracts;
using MediatR;
using System.Linq.Expressions;

namespace Estk.Core.Features.StockItem.Query.GetStockItems
{
    public class GetStockItemsQueryHandler : IRequestHandler<GetStockItemsQuery, Stock>
    {
        private readonly IStockRepository _stockRepo;

        public GetStockItemsQueryHandler(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }

        public async Task<Stock> Handle(GetStockItemsQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Stock, object>>>
            {
                x => x.Items
            };
            var stock = (await _stockRepo
                .GetAsync(x => x.Id == request.StockId, null, includes)).FirstOrDefault();
            return stock;
        }
    }
}
