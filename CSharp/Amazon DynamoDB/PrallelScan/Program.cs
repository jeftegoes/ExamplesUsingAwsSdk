using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

var client = new AmazonDynamoDBClient(RegionEndpoint.SAEast1);
var dbContextConfig = new DynamoDBContextConfig { Conversion = DynamoDBEntryConversion.V2 };
var context = new DynamoDBContext(client, dbContextConfig);

async Task ParallelScan()
{
    int totalSegments = 24;
    var tasks = new List<Task<IEnumerable<Product>>>();

    for (int segment = 0; segment < totalSegments; segment++)
    {
        var config = new DynamoDBOperationConfig
        {
            ConsistentRead = true
        };

        tasks.Add(ScanSegmentAsync(context, segment, config));
    }

    await Task.WhenAll(tasks);

    foreach (var task in tasks)
    {
        var items = await task;

        foreach (var item in items)
        {
            Console.WriteLine($"Scanned item: ID={item.Id}, Name={item.Isbn}, Age={item.Name}");
        }
    }
}

async Task<IEnumerable<Product>> ScanSegmentAsync(DynamoDBContext context, int segment, DynamoDBOperationConfig config)
{
    var scanConditions = new List<ScanCondition>
    {
        new ScanCondition("RegistrationDate", ScanOperator.Equal, "19/10/2023"),
        new ScanCondition("Segment", ScanOperator.Equal, segment.ToString())
    };

    var search = context.ScanAsync<Product>(scanConditions, config);
    var results = await search.GetNextSetAsync();

    return results;
}

await ParallelScan();