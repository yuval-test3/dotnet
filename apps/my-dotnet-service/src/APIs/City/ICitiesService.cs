using System.ComponentModel.DataAnnotations;
using MyDotnetService.APIs.Dtos;

namespace MyDotnetService.APIs;

public interface ICitiesService
{
    public Task<City> CreateCity(CityCreateInput input);
    public Task DeleteCity(string id);
    public Task<IEnumerable<City>> Cities();
    public Task UpdateCity(string id, City dto);
}
