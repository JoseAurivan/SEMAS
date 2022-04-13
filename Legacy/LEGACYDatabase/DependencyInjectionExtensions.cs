using Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Database
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDatabaseContext(this IServiceCollection serviceCollection)
        {
            string connectionString;
            using (var serviceProvider = serviceCollection.BuildServiceProvider())
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                connectionString = configuration.GetConnectionString("DefaultConnection");
            }

            serviceCollection.AddDbContext<Context>(options => options.UseSqlServer(connectionString));
            serviceCollection.AddScoped<IContext>(sp => sp.GetRequiredService<Context>());
            return serviceCollection;
        }
    }
}