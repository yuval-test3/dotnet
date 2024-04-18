using MyDotnetService.Infrastructure;

namespace MyDotnetService.APIs;

public class ProductsService : ProductsServiceBase
{
    public ProductsService(MyDotnetServiceContext context)
        : base(context) { }
}
