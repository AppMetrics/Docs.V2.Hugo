using System.Threading;
using System.Threading.Tasks;
using App.Metrics.Core;

namespace HealthChecks
{
    public class DatabaseHealthCheck : HealthCheck
    {
        private readonly IDatabase _database;

        public DatabaseHealthCheck(IDatabase database)
            : base("DatabaseCheck")
        {
            _database = database;
        }

        protected override Task<HealthCheckResult> CheckAsync(CancellationToken token = default(CancellationToken))
        {
            // exceptions will be caught and 
            // the result will be unhealthy
            _database.Ping();

            return Task.FromResult(HealthCheckResult.Healthy());
        }
    }
}