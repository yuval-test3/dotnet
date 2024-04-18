using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using Microsoft.EntityFrameworkCore;
using MyDotnetService.APIs.Dtos;
using MyDotnetService.APIs.Errors;
using MyDotnetService.APIs.Extensions;
using MyDotnetService.Infrastructure;
using MyDotnetService.Infrastructure.Models;

namespace MyDotnetService.APIs;

public abstract class UsersServiceBase : IUsersService
{
    protected readonly MyDotnetServiceContext _context;

    public UsersServiceBase(MyDotnetServiceContext context)
    {
        _context = context;
    }

    private bool UserExists(long id)
    {
        return _context.Users.Any(e => e.Id == id);
    }

    public async Task<UserDto> CreateUser(UserCreateInput inputDto)
    {
        var model = new User { Id = inputDto.Id, Name = inputDto.Name, };
        _context.users.Add(model);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<User>(model.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    public async Task DeleteUser(string id)
    {
        var user = await _context.users.FindAsync(id);

        if (user == null)
        {
            throw new NotFoundException();
        }

        _context.users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<UserDto>> users(UserFindMany findManyArgs)
    {
        var users = await _context
            .users.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();

        return users.ConvertAll(user => user.ToDto());
    }

    public async Task UpdateUser(string id, UserDto userDto)
    {
        var user = new User { Id = userDto.Id, Name = userDto.Name, };

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
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
