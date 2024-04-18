using Microsoft.AspNetCore.Mvc;

namespace MyDotnetService.APIs;

[ApiController]
public class CitysController : CitysControllerBase
{
    public CitysController(ICitysService service)
        : base(service) { }
}
