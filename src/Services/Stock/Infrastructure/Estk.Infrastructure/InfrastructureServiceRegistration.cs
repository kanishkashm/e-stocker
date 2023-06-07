using Estk.Core.Contracts;
using Estk.Infrastructure.Data;
using Estk.Infrastructure.Repositories;
using EStk.Core.Contracts;
using EStk.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EStk.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EstkDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();

            return services;
        }
    }
}
