using System;
using System.Threading;
using App.Metrics;
using App.Metrics.Reporting.Interfaces;
using MetricSamples;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleSample
{
    public class Application
    {
        public Application(IServiceProvider provider)
        {
            Metrics = provider.GetRequiredService<IMetrics>();

            CounterSamples = new CounterSamples(Metrics);

            var reporterFactory = provider.GetRequiredService<IReportFactory>();
            Reporter = reporterFactory.CreateReporter();

            Token = new CancellationToken();
        }

        public CounterSamples CounterSamples { get; private set; }

        public IMetrics Metrics { get; set; }

        public IReporter Reporter { get; set; }

        public CancellationToken Token { get; set; }
    }
}