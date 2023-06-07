using Estk.Core.Domain;
using Estk.Infrastructure.Data;
using Estk.Infrastructure.Repositories;
using EStk.Core.Contracts;

namespace EStk.Infrastructure.Repositories
{
    public class StockRepository : RepositoryBase<Stock>, IStockRepository
    {
        public StockRepository(EstkDbContext dbContext) : base(dbContext)
        {
        }
    }
}
