using BookClub.Generics;
using Microsoft.Extensions.DependencyInjection;

namespace BookClub.Extensions
{
    public static class ServiceExtensions
    {

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
    }
}
