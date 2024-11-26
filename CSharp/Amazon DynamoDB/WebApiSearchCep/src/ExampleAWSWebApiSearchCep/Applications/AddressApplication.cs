using AutoMapper;
using ExampleAWSWebApiSearchCep.Interfaces;
using ExampleAWSWebApiSearchCep.ViewModels;

namespace ExampleAWSWebApiSearchCep.Applications
{
    public class AddressApplication : IAddressApplication
    {
        private readonly IAddressService _viaCepService;
        private readonly IMapper _mapper;

        public AddressApplication(IMapper mapper,
                                  IAddressService viaCepService)
        {
            _mapper = mapper;
            _viaCepService = viaCepService;
        }

        public async Task<AddressViewModel> Get(string zipCode)
        {
            var address = await _viaCepService.GetAddress(zipCode);

            return _mapper.Map<AddressViewModel>(address);
        }
    }
}