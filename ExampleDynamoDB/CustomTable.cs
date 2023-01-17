using Amazon.DynamoDBv2.DataModel;

[DynamoDBTable("CustomTable")]
public class CustomTable
{
    [DynamoDBHashKey]
    public string Id { get; set; }

    [DynamoDBProperty("Description")]
    public string Description { get; set; } = string.Empty;
}