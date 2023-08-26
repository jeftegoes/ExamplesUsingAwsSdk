using System.Diagnostics;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;

[assembly: LambdaSerializer(typeof(DefaultLambdaJsonSerializer))]

public class BulkInsertion
{
    public async Task FunctionHandler(Parameters parameters)
    {
        Console.WriteLine("Process stared.");
        var stopWatch = new Stopwatch();

        var dynamoClient = new AmazonDynamoDBClient(RegionEndpoint.SAEast1);
        var dbContextConfig = new DynamoDBContextConfig { Conversion = DynamoDBEntryConversion.V2 };
        var context = new DynamoDBContext(dynamoClient, dbContextConfig);

        stopWatch.Start();

        Console.WriteLine(await DeleteAllItemsInTable(context));
        Console.WriteLine(await BatchInsertion(context, parameters.NumberOfItems));

        stopWatch.Stop();

        Console.WriteLine(string.Format("It worked well! {0}", stopWatch.Elapsed));
    }

    private async Task<string> DeleteAllItemsInTable(DynamoDBContext context)
    {
        AsyncSearch<Product> search = context.FromScanAsync<Product>(new ScanOperationConfig
        {
            Select = SelectValues.SpecificAttributes,
            AttributesToGet = new List<string> { "Id", "Isbn" }
        });

        var products = new List<Product>();
        do
        {
            products.Clear();
            products.AddRange(await search.GetNextSetAsync());

            products.ForEach(async product =>
            {
                await context.DeleteAsync<Product>(product.Id, product.Isbn);
            });

        } while (!search.IsDone);

        return "Items deleted.";
    }

    private async Task<string> BatchInsertion(DynamoDBContext context, int numberOfItems)
    {
        var products = new List<Product>();

        for (int i = 0; i < numberOfItems; i++)
            products.Add(new Product { Id = i.ToString(), Isbn = Guid.NewGuid().ToString(), Name = string.Format("Name {0}", i) });

        var batchWrite = context.CreateBatchWrite<Product>();
        batchWrite.AddPutItems(products);
        await batchWrite.ExecuteAsync();

        return "Items Inserted.";
    }

    private async Task<string> SlowerInsertion(DynamoDBContext context, int numberOfItems)
    {
        for (int i = 0; i < numberOfItems; i++)
        {
            var product = new Product { Id = i.ToString(), Isbn = Guid.NewGuid().ToString(), Name = string.Format("Name {0}", i) };
            await context.SaveAsync(product);
        }

        return "Items Inserted.";
    }
}
