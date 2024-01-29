using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace NotesWithTags.Tests.Integration.Host;

public static class ExtensionMethods
{
    public static IWebHostBuilder UseTestConfiguration(this IWebHostBuilder builder)
    {
        var config = new ConfigurationBuilder()
            .AddTestConfiguration()
            .Build();

        return builder.UseConfiguration(config);
    }

    public static IConfigurationBuilder AddTestConfiguration(this IConfigurationBuilder builder)
    {
        return builder
            .AddJsonFile("appsettings.Test.json")
            .AddUserSecrets("d857d7b0-f6da-4d1f-81c8-e84d8a0b8aa1")
            .AddEnvironmentVariables();
    }
}