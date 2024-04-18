using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MyDotnetService.APIs.Dtos;
using MyDotnetService.APIs.Errors;

namespace MyDotnetService.APIs;

[Route("api/[controller]")]
[ApiController]
public class AddressesControllerBase : ControllerBase
{
    protected readonly IAddressesService _service;

    public AddressesControllerBase(IAddressesService service)
    {
        _service = service;
    }

    [HttpPost("{id}/customers")]
    public async Task<IActionResult> ConnectAddress(string id, [Required] string CustomerId)
    {
        try
        {
            await _service.ConnectCustomer(id, AddressId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}/customers")]
    public async Task<IActionResult> DisconnectAddress(string id, [Required] string CustomerId)
    {
        try
        {
            await _service.DisconnectCustomer(id, AddressId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("{id}/customers")]
    public async Task<IActionResult> Customers(string id)
    {
        try
        {
            return Ok(await _service.Customers(id));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPatch("{id}/customers")]
    public async Task<IActionResult> UpdateCustomer(
        [FromRoute] AddressIdDto idDto,
        [FromBody] CustomerIdDto[] customerIds
    )
    {
        try
        {
            await _service.UpdateCustomer(id, CustomerId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<AddressDto>> CreateAddress(AddressCreateInput input)
    {
        var dto = await _service.CreateAddress(input);
        return CreatedAtAction(nameof(Address), new { id = dto.Id }, dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAddress(string id)
    {
        try
        {
            await _service.DeleteAddress(id);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AddressDto>>> Addresses()
    {
        return Ok(await _service.addresses());
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateAddress(string id, AddressDto addressDto)
    {
        if (id != addressDto.Id)
        {
            return BadRequest();
        }

        try
        {
            await _service.UpdateAddress(id, addressDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
