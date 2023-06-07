namespace EStk.Core.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IStockRepository Stocks { get; }
        IItemRepository Items { get; }
        int Save();
    }
}
