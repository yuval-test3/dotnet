using Microsoft.AspNetCore.Mvc;

namespace MyDotnetService.APIs;

[ApiController]
public class UsersController : UsersControllerBase
{
    public UsersController(IUsersService service)
        : base(service) { }
}
