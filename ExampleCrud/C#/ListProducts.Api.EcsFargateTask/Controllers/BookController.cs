using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly ILogger<BookController> _logger;

    public BookController(ILogger<BookController> logger)
    {
        _logger = logger;
    }

    private static string GetRegionName() =>
        Environment.GetEnvironmentVariable("AWS_REGION") ?? "sa-east-1";
    private static readonly AmazonDynamoDBClient dbClient = new AmazonDynamoDBClient(RegionEndpoint.GetBySystemName(GetRegionName()));
    private static readonly DynamoDBContext dbContext = new DynamoDBContext(dbClient);

    [HttpGet("getAll")]
    public async Task<IActionResult> Get()
    {
        var books = await dbContext.ScanAsync<Book>(null).GetNextSetAsync();

        return Ok(books);
    }

    [HttpGet("health")]
    public IActionResult Health()
    {
        return Ok();
    }
}