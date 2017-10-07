using System;
using System.Threading;
using System.Threading.Tasks;
using App.Metrics.Core;

namespace HealthChecks
{
    [Obsolete]
    public class IgnoreAttributeHealthCheck : HealthCheck
    {
        public IgnoreAttributeHealthCheck() : base("Referencing Assembly - Sample Ignore")
        {
        }

        protected override Task<HealthCheckResult> CheckAsync(CancellationToken token = default(CancellationToken))
        {
            return Task.FromResult(HealthCheckResult.Healthy("OK"));
        }
    }
}