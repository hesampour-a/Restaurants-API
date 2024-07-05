using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using U4.Domain.Entities;
using U4.Domain.Interfaces;
using U4.Domain.Repositories;
using U4.Infrastructure.Persistence;
using U4.Infrastructure.Repositories;
using U4.Infrastructure.Seeders;
using U4.Infrastructure.Services;


namespace U4.Infrastructure.Extensions
{
    public static class ServiceCollectionExtesions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connetionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<RestaurantDbContext>(options => options.UseSqlServer(connetionString).EnableSensitiveDataLogging());



            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
            services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
            services.AddScoped<IDishesRepository, DishesRepository>();
            services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();

        }
    }
}