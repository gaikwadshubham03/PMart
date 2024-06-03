using Prometheus;

public static class CustomMetrics
{
	public static readonly Counter HttpRequestCounter = Metrics.CreateCounter(
		"http_requests_total",
		"Total number of HTTP requests received",
		new CounterConfiguration
		{
			LabelNames = new[] { "status_code" }
		});
}