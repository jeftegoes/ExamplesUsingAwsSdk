
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
    public async Task FunctionHandler(Parameters parameters)
    {
        Console.WriteLine("Process stared.");
        var stopWatch = new Stopwatch();

        var awsRegion = RegionEndpoint.SAEast1;
        var s3Client = new AmazonS3Client(awsRegion);
        var dynamoClient = new AmazonDynamoDBClient(awsRegion);
        var dbContextConfig = new DynamoDBContextConfig { Conversion = DynamoDBEntryConversion.V2 };
        var dynamoDBContext = new DynamoDBContext(dynamoClient, dbContextConfig);

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
            BucketName = "products-images-939645320583",
            Key = $"data_{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.json",
            ContentBody = jsonProducts
        };

        return await s3Client.PutObjectAsync(putRequest);
    }
}