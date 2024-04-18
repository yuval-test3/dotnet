using MyDotnetService.Infrastructure;

namespace MyDotnetService.APIs;

public class UsersService : UsersServiceBase
{
    public UsersService(MyDotnetServiceContext context)
        : base(context) { }
}
