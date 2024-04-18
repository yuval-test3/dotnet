using Microsoft.AspNetCore.Mvc;
using MyDotnetService.Infrastructure.Models;

namespace MyDotnetService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class UserFindMany : FindManyInput<User, UserWhereInput> { }
