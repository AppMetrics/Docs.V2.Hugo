using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using App.Metrics;
using App.Metrics.Core;
using App.Metrics.Extensions.Reporting.Console;
using App.Metrics.Extensions.Reporting.TextFile;
using App.Metrics.Scheduling;
using HealthChecks;
using MetricSamples;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Serilog;

namespace ConsoleSample
{
    public class Program
    {
        private static IMetrics _metrics;

        public static void Main()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ConfigureMetrics(serviceCollection);

            var provider = serviceCollection.BuildServiceProvider();

            var application = new Application(provider);
            _metrics = application.Metrics;

            SetupSampleScheduler(application);
            SetupResetMetrics();

            application.Reporter.RunReports(application.Metrics, application.Token);

            Console.WriteLine("Setup Complete, waiting for report run...");
            Console.ReadKey();
        }

        private static void ConfigureMetrics(IServiceCollection services)
        {
            services
                .AddMetrics(options =>
                {
                    options.ReportingEnabled = true;
                    options.GlobalTags.Add("env", "uat");
                })
                .AddHealthChecks(factory =>
                {
                    factory.Register("DatabaseConnected", () => Task.FromResult("Database Connection OK"));
                    factory.Register("DiskSpace", () =>
                    {
                        var processPhysicalMemoryBytes = GetProcessPhysicalMemoryBytes();

                        return Task.FromResult(processPhysicalMemoryBytes <= 512
                            ? HealthCheckResult.Unhealthy("Process Reached Max Allowed Physical Memory: {0}", processPhysicalMemoryBytes)
                            : HealthCheckResult.Unhealthy("Process Physical Memory OK: {0}", processPhysicalMemoryBytes));
                    });
                })
                .AddReporting(factory =>
                {
                    factory.AddConsole(new ConsoleReporterSettings
                    {
                        ReportInterval = TimeSpan.FromSeconds(5),
                    });

                    factory.AddTextFile(new TextFileReporterSettings
                    {
                        ReportInterval = TimeSpan.FromSeconds(5),
                        FileName = @"C:\metrics\sample.txt"
                    });
                });
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var env = PlatformServices.Default.Application;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.RollingFile(Path.Combine($@"C:\logs\{env.ApplicationName}", "log-{Date}.txt"))
                .CreateLogger();

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddConsole((l, s) => s == LogLevel.Trace);
            loggerFactory.AddSerilog(Log.Logger);

            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddLogging();
            services.AddTransient<IDatabase, Database>();
        }

        private static long GetProcessPhysicalMemoryBytes()
        {
            var process = Process.GetCurrentProcess();
            return process.WorkingSet64;
        }

        private static void SetupResetMetrics()
        {
            var resetMetricScheduler = new DefaultTaskScheduler();
            resetMetricScheduler.Interval(TimeSpan.FromSeconds(10), TaskCreationOptions.None, () =>
            {
                // Reset the counter to zero
                _metrics.Advanced.Counter(SampleMetricRegistry.SentEmailsCounter).Reset();
            });
        }

        private static void SetupSampleScheduler(Application application)
        {
            var scheduler = new DefaultTaskScheduler();
            scheduler.Interval(TimeSpan.FromMilliseconds(300), TaskCreationOptions.None,() => { application.CounterSamples.RunSamples(); });
        }
    }
}