using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QueryBuilder.Infrastucture.Services;
using QueryBuilder.Shared.Infrastucture.Abstractions.Services;

namespace QueryBuilder.Infrastucture
{
    public static class DependencyInjectionsExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IConnectionExecutor, ConnectionExecutor>();
            services.AddScoped<IQueryExecutor, QueryExecutor>();
        }
    }
}
