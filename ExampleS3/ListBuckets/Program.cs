using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;

IAmazonS3 Connect()
{
    const string ACCESS_KEY = "YOUR_ACCESS_KEY";
    const string SECRET_KEY = "YOUR_SECRET_KEY";

    var credentials = new BasicAWSCredentials(ACCESS_KEY, SECRET_KEY);
    var s3Client = new AmazonS3Client(credentials, RegionEndpoint.SAEast1);

    return s3Client;
}

async Task<ListBucketsResponse> GetBuckets(IAmazonS3 client)
{
    return await client.ListBucketsAsync();
}

void DisplayBucketList(List<S3Bucket> buckets)
{
    buckets.ForEach(b =>
    {
        Console.WriteLine("Bucket name: {0}", b.BucketName);
    });
}

var client = Connect();
var response = await GetBuckets(client);
DisplayBucketList(response.Buckets);