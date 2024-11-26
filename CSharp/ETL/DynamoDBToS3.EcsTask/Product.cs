using Amazon.DynamoDBv2.DataModel;

[DynamoDBTable("Products")]
public class Product
{
    [DynamoDBHashKey("Id")]
    public string Id { get; set; } = string.Empty;

    [DynamoDBRangeKey("Isbn")]
    public string Isbn { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
}