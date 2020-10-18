using Microsoft.Extensions.DependencyInjection;
using MongoTutorialDemo.Services;

namespace MongoTutorialDemo.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddBusinessService(this IServiceCollection services)
        {
            services.AddScoped<BookService>();

            return services;
        }

        public static IServiceCollection AddVehicleService(this IServiceCollection services)
        {
            services.AddScoped<ITestService, ATestService>();

            services.AddScoped<ITestService, BTestService>();

            services.AddScoped<ITestService, CTestService>();

            services.AddScoped<ProviderManager>();

            services.AddScoped<TestItemService>();

            return services;
        }
    }
}