using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedDomain.Contexts;
using SharedDomain.Repositories;

namespace SharedDomain
{
    public static class DiServices
    {
        public static void AddSharedDomainDependencyInjection(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(opt => { opt.UseInMemoryDatabase("SharedDomainDb"); }, ServiceLifetime.Singleton);
            services.AddTransient<PersonRepository>();
            services.AddTransient<OccurrencyRepository>();
        }
    }
}