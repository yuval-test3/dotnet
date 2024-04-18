using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using Microsoft.EntityFrameworkCore;
using MyDotnetService.APIs.Dtos;
using MyDotnetService.APIs.Errors;
using MyDotnetService.APIs.Extensions;
using MyDotnetService.Infrastructure;
using MyDotnetService.Infrastructure.Models;

namespace MyDotnetService.APIs;

public abstract class CitiesServiceBase : ICitiesService
{
    protected readonly MyDotnetServiceContext _context;

    public CitiesServiceBase(MyDotnetServiceContext context)
    {
        _context = context;
    }

    private bool CityExists(long id)
    {
        return _context.Cities.Any(e => e.Id == id);
    }

    public async Task<CityDto> CreateCity(CityCreateInput inputDto)
    {
        var model = new City { Id = inputDto.Id, Name = inputDto.Name, };
        _context.cities.Add(model);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<City>(model.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    public async Task DeleteCity(string id)
    {
        var city = await _context.cities.FindAsync(id);

        if (city == null)
        {
            throw new NotFoundException();
        }

        _context.cities.Remove(city);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<CityDto>> cities(CityFindMany findManyArgs)
    {
        var cities = await _context
            .cities.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();

        return cities.ConvertAll(city => city.ToDto());
    }

    public async Task UpdateCity(string id, CityDto cityDto)
    {
        var city = new City { Id = cityDto.Id, Name = cityDto.Name, };

        _context.Entry(city).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CityExists(id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
