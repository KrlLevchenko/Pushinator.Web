using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace Pushinator.Web.AppStart
{
    public static class MigratorConfig
    {
        public static IServiceCollection AddMigrator(this IServiceCollection services, string connectionString)
        {
            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add SQLite support to FluentMigrator
                    .AddMySql5()
                    // Set the connection string
                    .WithGlobalConnectionString(connectionString)
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(Startup).Assembly).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole());

            return services;
        }
    }
}