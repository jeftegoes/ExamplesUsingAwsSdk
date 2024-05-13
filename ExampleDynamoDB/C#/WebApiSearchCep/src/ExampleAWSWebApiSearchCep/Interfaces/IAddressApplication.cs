using ExampleAWSWebApiSearchCep.ViewModels;

namespace ExampleAWSWebApiSearchCep.Interfaces
{
    public interface IAddressApplication
    {
        Task<AddressViewModel> Get(string zipCode);
    }
}