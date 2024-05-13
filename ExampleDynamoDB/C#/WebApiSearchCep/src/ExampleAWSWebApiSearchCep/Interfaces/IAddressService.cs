using ExampleAWSWebApiSearchCep.Models;

namespace ExampleAWSWebApiSearchCep.Interfaces
{
    public interface IAddressService
    {
        Task<Address> GetAddress(string zipCode);
    }
}