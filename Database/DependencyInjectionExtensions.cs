using Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Repositories;

namespace Database
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserRepository,UserRepository>();
            return serviceCollection;
        }

        public static IServiceCollection AddUnitOfWork(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            return serviceCollection;
        }

        public static IServiceCollection AddDatabaseContext(this IServiceCollection serviceCollection)
        {
            string connectionString;
            using (var serviceProvider = serviceCollection.BuildServiceProvider())
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                connectionString = configuration.GetConnectionString("DefaultConnection");
            }

            serviceCollection.AddDbContext<Context>(options => options.UseSqlServer(connectionString));
            return serviceCollection;
        }
    }
}