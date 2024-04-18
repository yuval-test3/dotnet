using MyDotnetService.Infrastructure;

namespace MyDotnetService.APIs;

public class CitiesService : CitiesServiceBase
{
    public CitiesService(MyDotnetServiceContext context)
        : base(context) { }
}
