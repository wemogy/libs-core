using System;
using System.Collections.Generic;
using System.Reflection;

namespace Wemogy.Core.Monitoring
{
    public class MonitoringEnvironment
    {
        public string ServiceName { get; }
        public string? ServiceNamespace { get; }
        public string? ServiceInstanceId { get; }
        public string ServiceVersion { get; }
        public string ApplicationInsightsConnectionString { get; private set; }
        public float ApplicationInsightsSamplingRatio { get; private set; }
        public bool UseApplicationInsights { get; private set; }
        public Uri? OtlpExportEndpoint { get; private set; }
        public bool UseOtlpExporter => OtlpExportEndpoint != null;
        public bool UsePrometheus { get; private set; }
        public HashSet<string> MeterNames { get; private set; }
        public HashSet<string> ActivitySourceNames { get; private set; }

        public MonitoringEnvironment()
        {
            ServiceName = Assembly.GetEntryAssembly().GetName().Name!;
            ServiceVersion = Assembly.GetEntryAssembly().GetName().Version != null
                ? Assembly.GetExecutingAssembly().GetName().Version!.ToString()
                : "0.0.0";

            ApplicationInsightsConnectionString = string.Empty;
            UsePrometheus = false;
            OtlpExportEndpoint = null;
            MeterNames = new HashSet<string>();
            ActivitySourceNames = new HashSet<string>();
        }

        public MonitoringEnvironment(string serviceName, string serviceVersion)
            : this()
        {
            ServiceName = serviceName;
            ServiceVersion = serviceVersion;
        }

        public MonitoringEnvironment(string serviceName, string serviceNamespace, string serviceInstanceId, string serviceVersion)
            : this(serviceName, serviceVersion)
        {
            ServiceName = serviceName;
            ServiceNamespace = serviceNamespace;
            ServiceInstanceId = serviceInstanceId;
            ServiceVersion = serviceVersion;
        }

        /// <summary>
        /// Adds an Application Insights exporter to publish metrics to.
        /// Fails, if the Connection String is null or empty.
        /// </summary>
        /// <param name="connectionString">The connection string to export to.</param>
        /// <param name="samplingRatio">The sampling ratio to use.</param>
        public MonitoringEnvironment WithApplicationInsights(string connectionString, float samplingRatio = 1f)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("The connection string must not be null or empty.", nameof(connectionString));
            }

            ApplicationInsightsConnectionString = connectionString;
            ApplicationInsightsSamplingRatio = samplingRatio;
            UseApplicationInsights = true;
            return this;
        }

        /// <summary>
        /// Adds an Application Insights exporter to publish metrics to.
        /// Skips, if the Connection String is null or empty.
        /// </summary>
        /// <param name="connectionString">The connection string to export to.</param>
        /// <param name="samplingRatio">The sampling ratio to use.</param>
        public MonitoringEnvironment WithOptionalApplicationInsights(string? connectionString, float samplingRatio = 1f)
        {
            if (!string.IsNullOrEmpty(connectionString))
            {
                ApplicationInsightsConnectionString = connectionString!;
                ApplicationInsightsSamplingRatio = samplingRatio;
                UseApplicationInsights = true;
            }
            else
            {
                UseApplicationInsights = false;
            }

            return this;
        }

        /// <summary>
        /// Adds an OpenTelemetry protocol (OTLP) exporter (e.g. Jaeger) to publish metrics to.
        /// </summary>
        /// <param name="endpoint">The endpoint to export to.</param>
        public MonitoringEnvironment WithOtlpExporter(Uri endpoint)
        {
            OtlpExportEndpoint = endpoint;
            return this;
        }

        public MonitoringEnvironment WithPrometheus()
        {
            UsePrometheus = true;
            return this;
        }

        /// <summary>
        /// Registers a System.Diagnostics.Metrics.Meter at the environment. The meter itself has to be created outside the environment.
        /// </summary>
        /// <param name="meterName">Name of the meter</param>
        public MonitoringEnvironment WithMeter(string meterName)
        {
            MeterNames.Add(meterName);
            return this;
        }

        /// <summary>
        /// Registers a System.Diagnostics.ActivitySource at the environment. The activity source itself has to be created outside the environment.
        /// </summary>
        /// <param name="activitySourceName">Name of the activity source</param>
        public MonitoringEnvironment WithActivitySource(string activitySourceName)
        {
            ActivitySourceNames.Add(activitySourceName);
            return this;
        }
    }
}
