using System.Text.Json;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using Models;

[assembly: LambdaSerializer(typeof(DefaultLambdaJsonSerializer))]

public class BookControoler
{
    private static string GetRegionName() =>
        Environment.GetEnvironmentVariable("AWS_REGION") ?? "sa-east-1";
    private static readonly AmazonDynamoDBClient dbClient = new AmazonDynamoDBClient(RegionEndpoint.GetBySystemName(GetRegionName()));
    private static readonly DynamoDBContext dbContext = new DynamoDBContext(dbClient);

    private APIGatewayProxyResponse GetDefaultResponse()
    {
        var response = new APIGatewayProxyResponse()
        {
            Headers = new Dictionary<string, string>(),
            StatusCode = 200
        };

        response.Headers.Add("Access-Control-Allow-Origin", "*");
        response.Headers.Add("Access-Control-Allow-Headers", "*");
        response.Headers.Add("Access-Control-Allow-Methods", "OPTIONS, POST");
        response.Headers.Add("Content-Type", "application/json");

        return response;
    }

    public async Task<APIGatewayProxyResponse> SaveBook(APIGatewayProxyRequest request, ILambdaContext context)
    {
        var book = JsonSerializer.Deserialize<Book>(request.Body);

        await dbContext.SaveAsync(book);

        var response = GetDefaultResponse();

        response.Body = JsonSerializer.Serialize(new { Message = "Book saved successfully!" });

        return response;
    }

    public async Task<APIGatewayProxyResponse> GetBooks(APIGatewayProxyRequest request, ILambdaContext context)
    {
        var books = await dbContext.ScanAsync<Book>(null).GetNextSetAsync();

        var response = GetDefaultResponse();

        response.Body = JsonSerializer.Serialize(books);

        return response;
    }

    public async Task<APIGatewayProxyResponse> GetBookById(APIGatewayProxyRequest request, ILambdaContext context)
    {
        var bookId = request.PathParameters["bookId"];

        var books = await dbContext.LoadAsync<Book>(bookId);

        var response = GetDefaultResponse();

        response.Body = JsonSerializer.Serialize(books);

        return response;
    }
}