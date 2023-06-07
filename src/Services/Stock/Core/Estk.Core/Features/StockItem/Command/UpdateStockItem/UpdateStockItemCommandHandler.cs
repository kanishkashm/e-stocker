using Estk.Core.Domain;
using EStk.Core.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Estk.Core.Features.StockItem.Command.UpdateStockItem
{
    public class UpdateStockItemCommandHandler : IRequestHandler<UpdateStockItemCommand>
    {
        private readonly IItemRepository _itemRepo;
        private readonly ILogger<UpdateStockItemCommandHandler> _logger;

        public UpdateStockItemCommandHandler(IItemRepository itemRepo, ILogger<UpdateStockItemCommandHandler> logger)
        {
            _itemRepo = itemRepo;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateStockItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _itemRepo.GetByIdAsync(request.Id);
            if (item == null)
            {
                _logger.LogError($"Item not exist on database.");
                throw new ApplicationException($"Item not exist on database. item code: {request.Id}");
            }

            item.Name = request.Name;
            item.Price = request.Price;

            await _itemRepo.UpdateAsync(item);

            _logger.LogInformation($"Item {item.Id} is successfully updated.");

            return Unit.Value;
        }
    }
}
