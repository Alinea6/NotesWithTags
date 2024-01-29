using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NotesWithTags.Adapters.Data;
using NotesWithTags.API.Validators;

namespace NotesWithTags.API;

public static class ExtensionMethods
{
    public static void RegisterOptions(this IServiceCollection services)
    {
        
    }

    public static void RegisterComponents(this IServiceCollection services)
    {
        
    }

    public static void RegisterExternalServices(this IServiceCollection services, IConfiguration configuration )
    {
        services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DataContext")));
    }

    public static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<ValidatorsNamespace>();
        services.AddFluentValidationAutoValidation();
    }
    
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Notes with tags", Version = "v1" });
            var openApiSecurityScheme = new OpenApiSecurityScheme()
            {
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference()
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            c.AddSecurityDefinition(openApiSecurityScheme.Reference.Id, openApiSecurityScheme);

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                { openApiSecurityScheme, new List<string>() }
            });
        });
    }
}