using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MyDotnetService.APIs.Dtos;
using MyDotnetService.APIs.Errors;

namespace MyDotnetService.APIs;

[Route("api/[controller]")]
[ApiController]
public class CustomersControllerBase : ControllerBase
{
    protected readonly ICustomersService _service;

    public CustomersControllerBase(ICustomersService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<CustomerDto>> CreateCustomer(CustomerCreateInput input)
    {
        var dto = await _service.CreateCustomer(input);
        return CreatedAtAction(nameof(Customer), new { id = dto.Id }, dto);
    }

    [HttpPost("{id}/orders")]
    public async Task<IActionResult> ConnectCustomer(string id, [Required] string OrderId)
    {
        try
        {
            await _service.ConnectOrder(id, CustomerId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost("{id}/addresses")]
    public async Task<IActionResult> ConnectCustomer(string id, [Required] string AddressId)
    {
        try
        {
            await _service.ConnectAddress(id, CustomerId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}/orders")]
    public async Task<IActionResult> DisconnectCustomer(string id, [Required] string OrderId)
    {
        try
        {
            await _service.DisconnectOrder(id, CustomerId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}/addresses")]
    public async Task<IActionResult> DisconnectCustomer(string id, [Required] string AddressId)
    {
        try
        {
            await _service.DisconnectAddress(id, CustomerId);
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

    [HttpGet("{id}/addresses")]
    public async Task<IActionResult> Addresses(string id)
    {
        try
        {
            return Ok(await _service.Addresses(id));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPatch("{id}/orders")]
    public async Task<IActionResult> UpdateOrder(
        [FromRoute] CustomerIdDto idDto,
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

    [HttpPatch("{id}/addresses")]
    public async Task<IActionResult> UpdateAddress(
        [FromRoute] CustomerIdDto idDto,
        [FromBody] AddressIdDto[] addressIds
    )
    {
        try
        {
            await _service.UpdateAddress(id, AddressId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(string id)
    {
        try
        {
            await _service.DeleteCustomer(id);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> Customers()
    {
        return Ok(await _service.customers());
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateCustomer(string id, CustomerDto customerDto)
    {
        if (id != customerDto.Id)
        {
            return BadRequest();
        }

        try
        {
            await _service.UpdateCustomer(id, customerDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
