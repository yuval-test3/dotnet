using MyDotnetService.Infrastructure;

namespace MyDotnetService.APIs;

public class CustomersService : CustomersServiceBase
{
    public CustomersService(MyDotnetServiceContext context)
        : base(context) { }
}
