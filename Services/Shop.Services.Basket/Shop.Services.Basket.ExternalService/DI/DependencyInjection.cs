using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Services.Basket.ExternalService.Abstractions;
using Shop.Services.Basket.ExternalService.Services;

namespace Shop.Services.Basket.ExternalService.DI;

public static class DependencyInjection
{
    public static void AddDataExternalServiceDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IDiscountGrpcService, DiscountGrpcService>();
    }
}