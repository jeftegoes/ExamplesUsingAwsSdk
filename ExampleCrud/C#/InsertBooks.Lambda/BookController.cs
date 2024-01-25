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

    private string GetRegionName() =>
        Environment.GetEnvironmentVariable("AWS_REGION") ?? "sa-east-1";

    public async Task<APIGatewayProxyResponse> SaveBook(APIGatewayProxyRequest request, ILambdaContext context)
    {
        var book = JsonSerializer.Deserialize<Book>(request.Body);

        var dbClient = new AmazonDynamoDBClient(RegionEndpoint.GetBySystemName(GetRegionName()));

        using (var dbContext = new DynamoDBContext(dbClient))
            await dbContext.SaveAsync(book);

        var response = GetDefaultResponse();

        response.Body = JsonSerializer.Serialize(new { Message = "Book saved successfully!" });

        return response;
    }
}