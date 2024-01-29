using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using NotesWithTags.Adapters.Data;
using NotesWithTags.Adapters.Data.Models;
using NotesWithTags.Adapters.Data.Repositories;
using NotesWithTags.API.Validators;
using NotesWithTags.Services.App;
using NotesWithTags.Services.App.Dep;
using NotesWithTags.Services.App.Int;

namespace NotesWithTags.API;

public static class ExtensionMethods
{
    public static void RegisterOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JsonWebTokensSettings>(configuration.GetSection(nameof(JsonWebTokensSettings)));
    }

    public static void RegisterComponents(this IServiceCollection services)
    {
        services
            .AddScoped<INoteService, NoteService>()
            .AddScoped<ITagService, TagService>()
            .AddScoped<INoteRepository, NoteRepository>();

        services
            .AddScoped<IUserService, UserService>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IJsonWebTokenProvider, JsonWebTokenProvider>();
    }

    public static void RegisterExternalServices(this IServiceCollection services, IConfiguration configuration )
    {
        services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DataContext")));
    }

    public static void RegisterSecurityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JSONWebTokensSettings:Issuer"],
                    ValidAudience = configuration["JSONWebTokensSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JSONWebTokensSettings:Key"]))
                };
            });
    }

    public static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<NoteUpdateRequestValidator>();
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