using System.Diagnostics;
using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;

namespace WebApiWithMetrics.Middleware
{
    public class CloudWatchExecutionTimeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAmazonCloudWatch _amazonCloudWatch;

        public CloudWatchExecutionTimeMiddleware(RequestDelegate next, IAmazonCloudWatch amazonCloudWatch)
        {
            _next = next;
            _amazonCloudWatch = amazonCloudWatch;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopWatch = new Stopwatch();

            stopWatch.Start();

            await _next(context);

            stopWatch.Stop();

            await _amazonCloudWatch.PutMetricDataAsync(new PutMetricDataRequest()
            {
                Namespace = "ExampleWebApi",
                MetricData = new List<MetricDatum>()
                {
                    new MetricDatum()
                    {
                        MetricName = "AspNetExecutionTime",
                        Value = stopWatch.ElapsedMilliseconds,
                        Unit = StandardUnit.Milliseconds,
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
}