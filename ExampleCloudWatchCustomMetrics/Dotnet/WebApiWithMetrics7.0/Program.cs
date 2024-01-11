using Amazon;
using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAWSService<IAmazonCloudWatch>();

var cloudWatchClient = new AmazonCloudWatchClient(RegionEndpoint.SAEast1);

var app = builder.Build();

app.UseExceptionHandler(options =>
{

});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    throw new NotImplementedException();
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapPost("create_anything", () =>
{
    return TypedResults.Created(Guid.NewGuid().ToString());
});

app.MapGet("thrownotimplementedexception", () =>
{
    throw new NotImplementedException();
});

app.UseMiddleware<CloudWatchStatusCodeMiddleware>();
app.UseMiddleware<CloudWatchExecutionTimeMiddleware>();
app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        await cloudWatchClient.PutMetricDataAsync(new PutMetricDataRequest()
        {
            Namespace = MetricEnumerator.NAMESPACE,
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
    });
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
