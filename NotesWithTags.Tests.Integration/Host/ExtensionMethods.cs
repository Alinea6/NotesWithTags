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
        //TODO: Add UserSercrets
        return builder
            .AddJsonFile("appsettings.Test.json")
            .AddUserSecrets("")
            .AddEnvironmentVariables();
    }
}