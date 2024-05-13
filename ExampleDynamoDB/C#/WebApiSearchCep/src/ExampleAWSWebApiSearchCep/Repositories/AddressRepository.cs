using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using ExampleAWSWebApiSearchCep.Interfaces;
using ExampleAWSWebApiSearchCep.Models;

namespace ExampleAWSWebApiSearchCep.Services
{
    public class AddressRepository : IAddressRepository
    {
        public AddressRepository()
        {

        }

        public Task<Address> Get()
        {
            var client = new AmazonDynamoDBClient();

            var request = new ScanRequest("Address");

            var response = client.ScanAsync(request);

            throw new NotImplementedException();
        }
    }
}