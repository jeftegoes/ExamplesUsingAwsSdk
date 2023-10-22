using Amazon.DynamoDBv2.DataModel;

[DynamoDBTable("Products")]
public class Product
{
    [DynamoDBHashKey]
    public string Id { get; set; } = string.Empty;

    [DynamoDBRangeKey]
    public string Isbn { get; set; } = string.Empty;

    [DynamoDBProperty("Name")]
    public string Name { get; set; } = string.Empty;

    [DynamoDBProperty("RegistrationDate")]
    public string RegistrationDate { get; set; } = string.Empty;

    [DynamoDBProperty("Segment")]
    public int Segment { get; set; }
}