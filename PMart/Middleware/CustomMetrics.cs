#region Header
// Author:      Shubham Gaikwad
// Date:        06/01/2024
#endregion

using Prometheus;
// Provides custom metrics for monitoring the application.
public static class CustomMetrics
{
	// Counter to track the total number of HTTP requests received, labeled by status code.
	public static readonly Counter HttpRequestCounter = Metrics.CreateCounter(
		"http_requests_total",
		"Total number of HTTP requests received",
		new CounterConfiguration
		{
			LabelNames = new[] { "status_code" }
		});
}