
using Domain.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.Configuration
{
    public static partial class ServiceCollectionExtensions
    {
        public static void AddAndConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICheeseRepository, CheeseRepository>();
        }
    }
}
