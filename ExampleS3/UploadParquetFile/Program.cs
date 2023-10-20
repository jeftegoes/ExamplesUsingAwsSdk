using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using UploadParquetFile;
using Parquet.Serialization;

var people = new List<Person>
{
    new Person() { Id = 1, Name = "Jefté" },
    new Person() { Id = 2, Name = "Brenno" },
    new Person() { Id = 3, Name = "Bárbara" }
};

var parqueFileName = "data2.parquet";

await ParquetSerializer.SerializeAsync<Person>(people, parqueFileName);
var s3Client = new AmazonS3Client(RegionEndpoint.SAEast1);

var fileTransferUtility = new TransferUtility(s3Client);
await fileTransferUtility.UploadAsync(parqueFileName, "products-images-939645320583");
