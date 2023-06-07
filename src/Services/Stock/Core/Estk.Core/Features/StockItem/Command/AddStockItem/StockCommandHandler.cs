using Estk.Core.Domain;
using EStk.Core.Contracts;
using MediatR;
using System.Linq.Expressions;

namespace Estk.Core.Features.StockItem.Command.AddStockItem
{
    public class StockCommandHandler : IRequestHandler<StockCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public StockCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(StockCommand request, CancellationToken cancellationToken)
        {
            var itemId = 0;
            var isNew = false;
            var includes = new List<Expression<Func<Stock, object>>>
            {
                x => x.Items
            };
            var stock = (await _unitOfWork.Stocks
                .GetAsync(x => x.Name.ToLower() == request.Name.ToLower(), null, includes)).FirstOrDefault();
            if (stock == null) 
            {
                stock = new Stock
                {
                    Name = request.Name,
                    Items = new List<Item>()
                };
                isNew = true;
            }
            
            if (isNew)
            {
                var x = request.Item;
                stock.Items.Add(new Item { Name = x.Name, Price = x.Price });
                await _unitOfWork.Stocks.AddAsync(stock);
                itemId = stock.Items.Max(x => x.Id);
            }
            else
            {
                var x = request.Item;
                var newItem = new Item
                {
                  Name = x.Name, Price = x.Price, StockId = stock.Id 
                };
                await _unitOfWork.Items.AddAsync(newItem);
                itemId = newItem.Id;
            }
            return itemId;
        }
    }
}
