using Round_2.Application.Service;
using Round_2.Application.Service.Interface;

namespace Round_2.Application;

public static class ApplicationCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        // request scoped services
        services.AddScoped<IMainService, MainService>();
        
        
        return services;
    }
}