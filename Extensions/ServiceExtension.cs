using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoTutorialDemo.DatabaseContext;
using MongoTutorialDemo.Services;

namespace MongoTutorialDemo.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddMongoDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbConnectionSettings>(
                       configuration.GetSection(DbConnectionConfigs.MongoDBConnectionSetting));

            services.AddSingleton<IMongoDbConnectionSettings>(sp =>
                        sp.GetRequiredService<IOptions<MongoDbConnectionSettings>>().Value);

            services.AddSingleton<MongoDbContext>();

            return services;
        }

        public static IServiceCollection AddBusinessService(this IServiceCollection services)
        {
            services.AddScoped<BookService>();

            return services;
        }

        public static IServiceCollection AddTestService(this IServiceCollection services)
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