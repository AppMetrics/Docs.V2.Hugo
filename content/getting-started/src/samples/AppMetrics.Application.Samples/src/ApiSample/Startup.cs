using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Metrics;
using App.Metrics.Extensions.Reporting.InfluxDB;
using App.Metrics.Reporting.Interfaces;
using HealthChecks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ApiSample
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IDatabase, Database>();

            services.AddMvc(options => options.AddMetricsResourceFilter());

            services
                .AddMetrics(Configuration.GetSection("AppMetrics"))
                .AddJsonSerialization()
                .AddReporting(factory =>
                {
                    var influxFilter = new MetricsFilter()
                        .WithHealthChecks(true)
                        .WithEnvironmentInfo(true);

                    factory.AddInfluxDb(new InfluxDbReporterSettings
                    {
                        BaseAddress = new Uri("http://127.0.0.1:8086"),
                        Database = "appmetricsapi",
                        ReportInterval = TimeSpan.FromSeconds(5)
                    }, influxFilter);
                })
                .AddHealthChecks(factory =>
                {
                    //factory.RegisterProcessPrivateMemorySizeHealthCheck("Private Memory Size", 200);
                    //factory.RegisterProcessVirtualMemorySizeHealthCheck("Virtual Memory Size", 200);
                    //factory.RegisterProcessPhysicalMemoryHealthCheck("Working Set", 200);
                })
                .AddMetricsMiddleware(Configuration.GetSection("AspNetMetrics"));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            ILoggerFactory loggerFactory, IApplicationLifetime lifetime)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMetrics();
            //app.UseMetricsReporting(lifetime);

            app.UseMvc();
        }
    }
}
