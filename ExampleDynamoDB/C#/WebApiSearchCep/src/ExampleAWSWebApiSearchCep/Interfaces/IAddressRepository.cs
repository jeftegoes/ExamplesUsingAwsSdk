using ExampleAWSWebApiSearchCep.Models;

namespace ExampleAWSWebApiSearchCep.Interfaces
{
    public interface IAddressRepository
    {
        Task<Address> Get();
    }
}