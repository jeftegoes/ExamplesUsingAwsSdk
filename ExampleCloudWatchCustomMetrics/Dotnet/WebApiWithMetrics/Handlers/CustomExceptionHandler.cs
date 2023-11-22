using System.Net;
using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;
using Microsoft.AspNetCore.Diagnostics;

public class CustomExceptionHandler : IExceptionHandler
{
    private readonly IAmazonCloudWatch _amazonCloudWatch;

    public CustomExceptionHandler(IAmazonCloudWatch amazonCloudWatch)
    {
        _amazonCloudWatch = amazonCloudWatch;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        await _amazonCloudWatch.PutMetricDataAsync(new PutMetricDataRequest()
        {
            Namespace = "ExampleWebApi",
            MetricData = new List<MetricDatum>()
            {
                new MetricDatum()
                {
                    MetricName = "HTTPCode_Target_5XX_Count",
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

        return true;
    }
}