using System;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB;
using Microsoft.Extensions.Hosting;
using Prometheus;
using Pushinator.Web.Model;

namespace Pushinator.Web.HostedServices
{
    public class BackgroundHostedService: IHostedService
    {
        private readonly Context _context;
        private readonly Gauge _usersCountGauge = Metrics.CreateGauge("users_count", "Number of active users");

        private readonly Histogram _randomNumberHistogram = Metrics.CreateHistogram("random_number", "Random number", new HistogramConfiguration
        {
            Buckets = Histogram.LinearBuckets(0, 10, 100)
        });
        private readonly Random _random = new Random();
        
        public BackgroundHostedService(Context context)
        {
            _context = context;
        }
        
        public Task StartAsync(CancellationToken ct)
        {
            return Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    var usersCount = await _context.Users.CountAsync(ct);
                    _usersCountGauge.Set(usersCount);

                    _randomNumberHistogram.Observe(_random.NextDouble() * 100);
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