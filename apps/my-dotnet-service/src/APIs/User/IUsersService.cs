using System.ComponentModel.DataAnnotations;
using MyDotnetService.APIs.Dtos;

namespace MyDotnetService.APIs;

public interface IUsersService
{
    public Task<User> CreateUser(UserCreateInput input);
    public Task DeleteUser(string id);
    public Task<IEnumerable<User>> Users();
    public Task UpdateUser(string id, User dto);
}
