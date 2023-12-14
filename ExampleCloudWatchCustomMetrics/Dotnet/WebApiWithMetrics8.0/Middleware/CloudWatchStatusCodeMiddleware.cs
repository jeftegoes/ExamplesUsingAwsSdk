using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;

public class CloudWatchStatusCodeMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IAmazonCloudWatch _amazonCloudWatch;

    public CloudWatchStatusCodeMiddleware(RequestDelegate next, IAmazonCloudWatch amazonCloudWatch)
    {
        _next = next;
        _amazonCloudWatch = amazonCloudWatch;
    }

    public async Task Invoke(HttpContext context)
    {
        await _next(context);

        await _amazonCloudWatch.PutMetricDataAsync(new PutMetricDataRequest()
        {
            Namespace = "ExampleWebApi",
            MetricData = new List<MetricDatum>()
            {
                new MetricDatum()
                {
                    MetricName = "HTTPCode_Target_2XX_Count",
                    Value = 1,
                    Unit = StandardUnit.Count,
                    TimestampUtc = DateTime.UtcNow,
                    Dimensions = new List<Dimension>()
                    {
                        new Dimension()
                        {
                            Name = "Method",
                            Value = context.Request.Method
                        },
                        new Dimension()
                        {
                            Name = "Path",
                            Value = context.Request.Path
                        }
                    }
                }
            }
        });
    }
}