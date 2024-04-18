using Microsoft.AspNetCore.Mvc;

namespace MyDotnetService.APIs;

[ApiController]
public class CustomersController : CustomersControllerBase
{
    public CustomersController(ICustomersService service)
        : base(service) { }
}
