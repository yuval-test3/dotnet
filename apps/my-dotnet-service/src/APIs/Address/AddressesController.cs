using Microsoft.AspNetCore.Mvc;

namespace MyDotnetService.APIs;

[ApiController]
public class AddresssController : AddresssControllerBase
{
    public AddresssController(IAddresssService service)
        : base(service) { }
}
