using ExampleAWSWebApiSearchCep.Interfaces;
using ExampleAWSWebApiSearchCep.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ExampleAWSWebApiSearchCep.Controllers;

[Route("api/[controller]")]
public class AddressController : ControllerBase
{
    private readonly IAddressApplication _addressApplication;

    public AddressController(IAddressApplication addressApplication)
    {
        _addressApplication = addressApplication;
    }

    [HttpGet("{zipCode}")]
    public async Task<ActionResult<AddressViewModel>> Get(string zipCode)
    {
        return Ok(await _addressApplication.Get(zipCode));
    }
}