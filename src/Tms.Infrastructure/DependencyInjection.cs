using Microsoft.Extensions.DependencyInjection;
using Tms.Domain.Interfaces;
using Tms.Infrastructure.Repositories;

namespace Tms.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IBidRepository, BidRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IStatusRepository, StatusRepository>();
        services.AddScoped<ITenderRepository, TenderRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IVendorRepository, VendorRepository>();

        return services;
    }
}
