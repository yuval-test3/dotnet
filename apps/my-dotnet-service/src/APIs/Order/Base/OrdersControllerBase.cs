using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MyDotnetService.APIs.Dtos;
using MyDotnetService.APIs.Errors;

namespace MyDotnetService.APIs;

[Route("api/[controller]")]
[ApiController]
public class OrdersControllerBase : ControllerBase
{
    protected readonly IOrdersService _service;

    public OrdersControllerBase(IOrdersService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> CreateOrder(OrderCreateInput input)
    {
        var dto = await _service.CreateOrder(input);
        return CreatedAtAction(nameof(Order), new { id = dto.Id }, dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(string id)
    {
        try
        {
            await _service.DeleteOrder(id);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> Orders()
    {
        return Ok(await _service.orders());
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateOrder(string id, OrderDto orderDto)
    {
        if (id != orderDto.Id)
        {
            return BadRequest();
        }

        try
        {
            await _service.UpdateOrder(id, orderDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
