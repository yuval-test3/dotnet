using Microsoft.AspNetCore.Mvc;

namespace MyDotnetService.APIs;

[ApiController]
public class ProductsController : ProductsControllerBase
{
    public ProductsController(IProductsService service)
        : base(service) { }
}
