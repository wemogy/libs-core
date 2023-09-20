using Prometheus;

namespace Wemogy.Core.Monitoring
{
    public class MonitoringService : IMonitoringService
    {
        readonly MonitoringEnvironment _env;

        public MonitoringService(MonitoringEnvironment env)
        {
            _env = env;
        }

        public void TrackEvent(string eventName, string eventDescription = "")
        {
            // TODO: Event remove Prometheus. All should be tracked in OTLP

            // Prometheus
            if (_env.UsePrometheus)
            {
                var counter = Metrics.CreateCounter(eventName, eventDescription);
                counter.Inc();
            }
        }
    }
}
