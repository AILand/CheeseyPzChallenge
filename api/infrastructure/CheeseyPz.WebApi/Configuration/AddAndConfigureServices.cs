using Application.Core;
using Application.UseCases;
using CheeseyPz.WebApi.Core;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CheeseyPz.WebApi.Configuration
{
    public static partial class ServiceCollectionExtensions
    {
        public static void AddAndConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ServiceFactory>(p => p.GetService);
            services.AddScoped<IApplicationUseCaseHandler, ApplicationUseCaseHandler>();

            services.Scan(scanner => scanner
                .FromAssembliesOf(typeof(Startup))
                .AddClasses(a => a.AssignableTo(typeof(IValidator<>))).AsSelfWithInterfaces().WithScopedLifetime());

            services.Scan(scanner => scanner
                .FromAssemblies(typeof(ApplicationUseCasesSymbol).Assembly, typeof(Startup).Assembly)
                .AddClasses(a => a.AssignableTo(typeof(IUseCaseHandler<,>))).AsSelfWithInterfaces()
                .WithScopedLifetime());
        }
    }
}
