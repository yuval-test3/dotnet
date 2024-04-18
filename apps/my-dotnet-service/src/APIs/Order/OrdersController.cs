using Microsoft.AspNetCore.Mvc;

namespace MyDotnetService.APIs;

[ApiController]
public class OrdersController : OrdersControllerBase
{
    public OrdersController(IOrdersService service)
        : base(service) { }
}
