using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

public class CustomTableRepository
{
    private DynamoDBContext _context = new DynamoDBContext(new AmazonDynamoDBClient(Amazon.RegionEndpoint.SAEast1));

    public async Task<List<CustomTable>> GetAllAsync()
    {
        return await _context.ScanAsync<CustomTable>(null).GetNextSetAsync();
    }

    public async Task<CustomTable> GetItemAsync(string id)
    {
        return await _context.LoadAsync<CustomTable>(id);
    }

    public async Task SaveAsync(CustomTable customTable)
    {
        await _context.SaveAsync(customTable);
    }

    public async Task DeleteAsync(string id)
    {
        await _context.DeleteAsync<CustomTable>(id);
    }
}