using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Action.Common.Mongo
{
    public static class Extensions
    {
        public static void AddMongoDB(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoOptions>(configuration.GetSection("mongo"));

            services.AddSingleton<MongoClient>(c =>
            {
                var opt = c.GetService<IOptions<MongoOptions>>();
                return new MongoClient(opt.Value.ConnectionString);
            });

            services.AddScoped<IMongoDatabase>(c =>
            {
                var opt = c.GetService<IOptions<MongoOptions>>();
                var client = c.GetService<MongoClient>();
                return client.GetDatabase(opt.Value.DataBase);
            });
            services.AddScoped<IDatabaseInitializer, MongoInitializer>();
            services.AddScoped<IDatabaseSeeder, MongoSeeder>();

        }
    }
}