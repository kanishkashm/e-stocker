using Estk.Infrastructure.Data;
using EStk.Core.Contracts;

namespace EStk.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EstkDbContext _dbContext;

        public UnitOfWork(EstkDbContext dbContext, IStockRepository stockRepo, IItemRepository itemRepo)
        {
            _dbContext = dbContext;
            Stocks = stockRepo;
            Items = itemRepo;
        }

        public IStockRepository Stocks { get; }
        public IItemRepository Items { get; }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}
