using Estk.Core.Features.StockItem.Command.UpdateStockItem;
using EStk.Core.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Estk.Core.Features.StockItem.Command.IssueStock
{
    public class IssueStockCommandHandler : IRequestHandler<IssueStockCommand, bool>
    {
        private readonly IStockRepository _stockRepo;
        private readonly ILogger<UpdateStockItemCommandHandler> _logger;

        public IssueStockCommandHandler(IStockRepository stockRepo, ILogger<UpdateStockItemCommandHandler> logger)
        {
            _stockRepo = stockRepo;
            _logger = logger;
        }

        public async Task<bool> Handle(IssueStockCommand request, CancellationToken cancellationToken)
        {
            var stock = await _stockRepo.GetByIdAsync(request.StockId);
            if (stock == null)
            {
                _logger.LogError($"Stock not exist on database.");
                throw new ApplicationException($"Stock not exist on database. stock id: {request.StockId}");
            }
            if(stock.RowVersion != request.RowVersion)
            {
                _logger.LogError($"Stock is not available. It was already taken by someone else :(");
                throw new ApplicationException($"Stock is not available. It was already taken by someone else :(. stock id: {request.StockId}");
            }
            stock.Availability = false;
            stock.TakenBy = request.TakenBy;
            await _stockRepo.UpdateAsync(stock);
            return true;
        }
    }
}
