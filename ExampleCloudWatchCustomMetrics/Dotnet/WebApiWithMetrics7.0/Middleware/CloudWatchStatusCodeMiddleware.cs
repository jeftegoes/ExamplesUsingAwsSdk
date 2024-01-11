using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

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

        if (!((HttpStatusCode)context.Response.StatusCode).IsSuccessStatusCode())
            return;

        await _amazonCloudWatch.PutMetricDataAsync(new PutMetricDataRequest()
        {
            Namespace = MetricEnumerator.NAMESPACE,
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