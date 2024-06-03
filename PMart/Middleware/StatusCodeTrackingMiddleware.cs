public class StatusCodeTrackingMiddleware
{
	private readonly RequestDelegate _next;

	public StatusCodeTrackingMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		await _next(context);

		var statusCode = context.Response.StatusCode.ToString();
		CustomMetrics.HttpRequestCounter.WithLabels(statusCode).Inc();
	}
}