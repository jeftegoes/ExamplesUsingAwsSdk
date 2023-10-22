using System.Text.Json;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

var client = new AmazonDynamoDBClient(RegionEndpoint.SAEast1);
var dbContextConfig = new DynamoDBContextConfig { Conversion = DynamoDBEntryConversion.V2 };
var context = new DynamoDBContext(client, dbContextConfig);


var config = new DynamoDBOperationConfig
{
    ConsistentRead = false
};

var batchGet = context.CreateBatchGet<Product>(config);
batchGet.AddKey(new Product() { Id = "1", Isbn = "1" });
await batchGet.ExecuteAsync();

foreach (var item in batchGet.Results)
{
    Console.WriteLine($"Retrieved Item: {item.Id}, {item.Name}, {item.RegistrationDate}");
}