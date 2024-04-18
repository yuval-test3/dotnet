using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MyDotnetService.APIs.Dtos;
using MyDotnetService.APIs.Errors;

namespace MyDotnetService.APIs;

[Route("api/[controller]")]
[ApiController]
public class CitiesControllerBase : ControllerBase
{
    protected readonly ICitiesService _service;

    public CitiesControllerBase(ICitiesService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<CityDto>> CreateCity(CityCreateInput input)
    {
        var dto = await _service.CreateCity(input);
        return CreatedAtAction(nameof(City), new { id = dto.Id }, dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCity(string id)
    {
        try
        {
            await _service.DeleteCity(id);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CityDto>>> Cities()
    {
        return Ok(await _service.cities());
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateCity(string id, CityDto cityDto)
    {
        if (id != cityDto.Id)
        {
            return BadRequest();
        }

        try
        {
            await _service.UpdateCity(id, cityDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
