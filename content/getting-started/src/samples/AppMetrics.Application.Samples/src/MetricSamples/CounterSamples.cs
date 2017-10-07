using System;
using App.Metrics;
using MetricSamples.SupportingCodeForDemoPurposes;

namespace MetricSamples
{
    public class CounterSamples
    {
        private readonly IMetrics _metrics;

        public CounterSamples(IMetrics metrics)
        {
            _metrics = metrics;
        }

        public void RunSamples()
        {
            RunItemSample();
        }

        private void RunItemSample()
        {
            Action<IEmail> sendEmail = email =>
            {
                // Increment counter by one
                _metrics.Increment(SampleMetricRegistry.SentEmailsCounter, email.GetType().Name.ToLowerInvariant());
            };

            Action<IEmail, int> sendBatchEmails = (email, count) =>
            {
                // Increment Counter by more than one
                _metrics.Increment(SampleMetricRegistry.SentEmailsCounter, count);
            };

            for (var i = 0; i < 30; i++)
            {
                var emailIndex = new Random().Next() % 3;
                if (emailIndex == 1) sendEmail(new EmailAFriend());
                if (emailIndex == 2) sendEmail(new ForgotPassword());
                if (emailIndex == 3) sendEmail(new AccountVerification());
            }

            sendBatchEmails(new EmailAFriend(), 2);
        }
    }
}