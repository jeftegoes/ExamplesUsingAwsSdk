
using System.Diagnostics;
using System.Text.Json;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using Amazon.S3;
using Amazon.S3.Model;

[assembly: LambdaSerializer(typeof(DefaultLambdaJsonSerializer))]

public class DynamoDBToS3
{
    private static RegionEndpoint awsRegion = RegionEndpoint.SAEast1;
    private static AmazonS3Client s3Client = new AmazonS3Client(awsRegion);
    private static AmazonDynamoDBClient dynamoClient = new AmazonDynamoDBClient(awsRegion);
    private static DynamoDBContextConfig dbContextConfig = new DynamoDBContextConfig { Conversion = DynamoDBEntryConversion.V2 };
    private static DynamoDBContext dynamoDBContext = new DynamoDBContext(dynamoClient, dbContextConfig);

    public async Task FunctionHandler()
    {
        Console.WriteLine("Process stared.");
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        Console.WriteLine("Getting DynamoDB items...");
        var products = await GetDynamodbItems(dynamoDBContext);
        Console.WriteLine("DynamoDB items captured successfully!");

        var response = await PutS3Objects(s3Client, products);

        stopWatch.Stop();

        if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
        {
            Console.WriteLine("Data stored in S3 successfully.");
        }
        else
        {
            Console.WriteLine("Failed to store data in S3.");
        }
        Console.WriteLine(string.Format("It worked well! {0}", stopWatch.Elapsed));
    }

    public async Task<List<Product>> GetDynamodbItems(DynamoDBContext dynamoDBContext)
    {
        var conditions = new List<ScanCondition>();
        return await dynamoDBContext.ScanAsync<Product>(conditions).GetRemainingAsync();
    }

    public async Task<PutObjectResponse> PutS3Objects(AmazonS3Client s3Client, List<Product> products)
    {
        var jsonProducts = JsonSerializer.Serialize(products);

        var putRequest = new PutObjectRequest
        {
            BucketName = "products-images-XXXXXXXXXXX",
            Key = $"data_{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.json",
            ContentBody = jsonProducts
        };

        return await s3Client.PutObjectAsync(putRequest);
    }
}