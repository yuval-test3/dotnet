using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MyDotnetService.APIs.Dtos;
using MyDotnetService.APIs.Errors;

namespace MyDotnetService.APIs;

[Route("api/[controller]")]
[ApiController]
public class ProductsControllerBase : ControllerBase
{
    protected readonly IProductsService _service;

    public ProductsControllerBase(IProductsService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct(ProductCreateInput input)
    {
        var dto = await _service.CreateProduct(input);
        return CreatedAtAction(nameof(Product), new { id = dto.Id }, dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        try
        {
            await _service.DeleteProduct(id);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> Products()
    {
        return Ok(await _service.products());
    }

    [HttpPost("{id}/orders")]
    public async Task<IActionResult> ConnectProduct(string id, [Required] string OrderId)
    {
        try
        {
            await _service.ConnectOrder(id, ProductId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}/orders")]
    public async Task<IActionResult> DisconnectProduct(string id, [Required] string OrderId)
    {
        try
        {
            await _service.DisconnectOrder(id, ProductId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("{id}/orders")]
    public async Task<IActionResult> Orders(string id)
    {
        try
        {
            return Ok(await _service.Orders(id));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPatch("{id}/orders")]
    public async Task<IActionResult> UpdateOrder(
        [FromRoute] ProductIdDto idDto,
        [FromBody] OrderIdDto[] orderIds
    )
    {
        try
        {
            await _service.UpdateOrder(id, OrderId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateProduct(string id, ProductDto productDto)
    {
        if (id != productDto.Id)
        {
            return BadRequest();
        }

        try
        {
            await _service.UpdateProduct(id, productDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
