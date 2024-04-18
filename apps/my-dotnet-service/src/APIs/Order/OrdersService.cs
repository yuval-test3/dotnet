using MyDotnetService.Infrastructure;

namespace MyDotnetService.APIs;

public class OrdersService : OrdersServiceBase
{
    public OrdersService(MyDotnetServiceContext context)
        : base(context) { }
}
