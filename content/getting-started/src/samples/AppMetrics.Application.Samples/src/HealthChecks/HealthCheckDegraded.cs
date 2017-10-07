using System.Threading;
using System.Threading.Tasks;
using App.Metrics.Core;

namespace HealthChecks
{
    public class HealthCheckDegraded : HealthCheck
    {
        public HealthCheckDegraded() : base("Referencing Assembly - Sample Degraded")
        {
        }

        protected override Task<HealthCheckResult> CheckAsync(CancellationToken token = default(CancellationToken))
        {
            return Task.FromResult(HealthCheckResult.Degraded());
        }
    }
}