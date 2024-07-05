using Microsoft.Extensions.DependencyInjection;
using U4.Application.Restaurants;
using U4.Application.User;

namespace U4.Application.Extesions
{
    public static class ServiceCollectionExtesion
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(ServiceCollectionExtesion).Assembly));
            services.AddAutoMapper(typeof(ServiceCollectionExtesion).Assembly);

            services.AddScoped<IUserContext, UserContext>();

            
        }
    }
}
