using MediatR;

namespace Estk.Core.Features.StockItem.Command.IssueStock
{
    public class IssueStockCommand : IRequest<bool>
    {
        public int StockId { get; set; }
        public string TakenBy { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
