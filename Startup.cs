using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoTutorialDemo.DatabaseContext;
using MongoTutorialDemo.Extensions;

namespace MongoTutorialDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen();

            services.Configure<MongoDbConnectionSettings>(
                        Configuration.GetSection(DbConnectionConfigs.MongoDBConnectionSetting));

            services.AddSingleton<IMongoDbConnectionSettings>(sp =>
                        sp.GetRequiredService<IOptions<MongoDbConnectionSettings>>().Value);

            services.AddSingleton<MongoDbContext>();

            services.AddBusinessService();

            services.AddVehicleService();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        //private static MongoDbContext InitializeMongoDb(IConfigurationSection configurationSection)
        //{
        //    var connectionString= configurationSection.GetSection("ConnectionString").Value;
        //    string databaseName = configurationSection.GetSection("DatabaseName").Value;
        //    var client = new MongoClient(connectionString);
        //    MongoDbContext mongoDbContext = new MongoDbContext(client, databaseName);
        //    return mongoDbContext;
        //}
    }
}