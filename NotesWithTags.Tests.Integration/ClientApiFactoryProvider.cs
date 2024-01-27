using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NotesWithTags.Tests.Integration.Host;

namespace NotesWithTags.Tests.Integration;

static class ClientApiFactoryProvider
{
    public static WebApplicationFactory<Program> Create()
    {
        return Create((context, services) => {});
    }

    public static WebApplicationFactory<Program> Create(
        Action<WebHostBuilderContext, IServiceCollection> configureServices)
    {
        return new WebApplicationFactory<Program>()
            .WithWebHostBuilder(webHostBuilder => webHostBuilder
                .UseTestConfiguration()
                .ConfigureServices(configureServices));
    }
}