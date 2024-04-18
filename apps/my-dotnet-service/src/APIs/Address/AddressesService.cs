using MyDotnetService.Infrastructure;

namespace MyDotnetService.APIs;

public class AddressesService : AddressesServiceBase
{
    public AddressesService(MyDotnetServiceContext context)
        : base(context) { }
}
