using System;
using Microsoft.ApplicationInsights;
using Prometheus;

namespace Wemogy.Core.Monitoring
{
    public class MonitoringService : IMonitoringService
    {
        readonly TelemetryClient? _appInsightsTelemetryClient;
        readonly MonitoringEnvironment _env;

        public MonitoringService(MonitoringEnvironment env, IServiceProvider services)
        {
            if (env.UseApplicationInsights)
            {
                // Don't get the TelemetryClient from the DI via constructor, as it
                // might not be registered when ApplicationInsights is disabled. Instead, get it
                // from the DI container here when it's actually needed.
                _appInsightsTelemetryClient = (TelemetryClient)services.GetService(typeof(TelemetryClient));
            }

            _env = env;
        }

        public void TrackEvent(string eventName, string eventDescription = "")
        {
            // Application Insights
            if (_env.UseApplicationInsights && _appInsightsTelemetryClient != null)
            {
                _appInsightsTelemetryClient.TrackEvent(eventName);
            }

            // Prometheus
            if (_env.UsePrometheus)
            {
                var counter = Metrics.CreateCounter(eventName, eventDescription);
                counter.Inc();
            }
        }
    }
}
