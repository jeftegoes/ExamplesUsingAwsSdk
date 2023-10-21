using System.Text.Json;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

var client = new AmazonDynamoDBClient(RegionEndpoint.SAEast1);
var dbContextConfig = new DynamoDBContextConfig { Conversion = DynamoDBEntryConversion.V2 };
var context = new DynamoDBContext(client, dbContextConfig);

var queryValue1 = "19/10/2023";
var queryValue2 = "2";

var conditions = new List<ScanCondition>
{
    new ScanCondition("RegistrationDate", ScanOperator.Equal, queryValue1),
    new ScanCondition("Segment", ScanOperator.Equal, queryValue2)
};

var query = context.QueryAsync<Product>(queryValue1, new DynamoDBOperationConfig
{
    IndexName = "RegistrationDate-Segment-index",
    QueryFilter = conditions
});

List<Product> results = await query.GetRemainingAsync();

foreach (var item in results)
{
    var json = JsonSerializer.Serialize(item);
    Console.WriteLine(json);
}