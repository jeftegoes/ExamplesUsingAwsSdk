using AutoMapper;
using ExampleAWSWebApiSearchCep.Models;
using ExampleAWSWebApiSearchCep.ViewModels;

namespace ExampleAWSWebApiSearchCep.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressViewModel>().ReverseMap();
        }
    }
}