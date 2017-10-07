using App.Metrics;
using App.Metrics.Core.Options;

namespace MetricSamples
{
    public static class SampleMetricRegistry
    {
        public static CounterOptions SentEmailsCounter = new CounterOptions
        {
            Name = "Sent Emails",
            MeasurementUnit = Unit.Calls
        };
    }
}