using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stockly.Domain.Abstractions.Persistence;
using Stockly.Infrastructure.Persistence;
using Stockly.Infrastructure.Persistence.Repositories;

namespace Stockly.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<StocklyDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductVariationRepository, ProductVariationRepository>();
        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddScoped<IInventoryMovementRepository, InventoryMovementRepository>();
        services.AddScoped<IPurchaseRepository, PurchaseRepository>();

        return services;
    }
}
