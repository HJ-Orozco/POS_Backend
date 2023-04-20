using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POS.Infrastucture.Persistences.Contexts;
using POS.Infrastucture.Persistences.Interfaces;
using POS.Infrastucture.Persistences.Repositories;

namespace POS.Infrastucture.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection addInjectionInfraestructure(this IServiceCollection service, IConfiguration configuration)
        {
            var assembly = typeof(POSContext).Assembly.FullName;

            service.AddDbContext<POSContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("POSConnection"), b => b.MigrationsAssembly(assembly)), ServiceLifetime.Transient
                );

            service.AddTransient<IUnitOfWork, UnitOfWork>();
            return service;
        }
    }
}
