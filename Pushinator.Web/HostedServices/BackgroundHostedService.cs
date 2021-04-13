using System;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prometheus;
using Pushinator.Web.Model;

namespace Pushinator.Web.HostedServices
{
    public class BackgroundHostedService: IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Gauge _usersCountGauge = Metrics.CreateGauge("users_count", "Number of active users");

        private readonly Histogram _randomNumberHistogram = Metrics.CreateHistogram("random_number", "Random number", new HistogramConfiguration
        {
            Buckets = Histogram.LinearBuckets(0, 10, 100)
        });
        private readonly Random _random = new Random();
        
        public BackgroundHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public Task StartAsync(CancellationToken ct)
        {
            return Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetService<Context>();
                        var usersCount = await context.Users.CountAsync(ct);
                        _usersCountGauge.Set(usersCount);

                        _randomNumberHistogram.Observe(_random.NextDouble() * 100);
                    }
                    
                    
                    Thread.Sleep(1000);
                }

            }, ct);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}