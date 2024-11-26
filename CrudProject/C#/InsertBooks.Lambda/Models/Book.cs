using Amazon.DynamoDBv2.DataModel;

namespace Models
{
    [DynamoDBTable("Books")]
    public class Book
    {
        [DynamoDBHashKey("Id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string Author { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}