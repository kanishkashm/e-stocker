using Estk.Core.Domain;
using Estk.Infrastructure.Data;
using Estk.Infrastructure.Repositories;
using EStk.Core.Contracts;

namespace EStk.Infrastructure.Repositories
{
    public class ItemRepository : RepositoryBase<Item>, IItemRepository
    {
        public ItemRepository(EstkDbContext dbContext) : base(dbContext)
        {
        }
    }
}
