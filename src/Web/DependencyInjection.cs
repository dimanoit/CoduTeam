using System.Text.Json.Serialization;
using Azure.Identity;
using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Infrastructure.Data;
using CoduTeam.Web.Services;
using Microsoft.AspNetCore.Mvc;
using NSwag;
using NSwag.Generation.Processors.Security;
using ZymLabs.NSwag.FluentValidation;

namespace CoduTeam.Web;

public static class DependencyInjection
{
    public static readonly string CorsPolicyName = "CorsPolicy";

    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<IUser, CurrentUser>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddScoped(provider =>
        {
            IEnumerable<FluentValidationRule>? validationRules =
                provider.GetService<IEnumerable<FluentValidationRule>>();
            ILoggerFactory? loggerFactory = provider.GetService<ILoggerFactory>();

            return new FluentValidationSchemaProcessor(provider, validationRules, loggerFactory);
        });

        services.AddCors(o => o.AddPolicy(CorsPolicyName, builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        }));

        // Customise default API behaviour
        services
            .Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true
            )
            .ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            }).Configure<JsonOptions>(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        services.AddEndpointsApiExplorer();

        services.AddOpenApiDocument((configure, sp) =>
        {
            configure.Title = "CoduTeam API";


            // Add the fluent validations schema processor
            FluentValidationSchemaProcessor fluentValidationSchemaProcessor =
                sp.CreateScope().ServiceProvider.GetRequiredService<FluentValidationSchemaProcessor>();

            configure.SchemaSettings.SchemaProcessors.Add(fluentValidationSchemaProcessor);

            // Add JWT
            configure.AddSecurity("JWT", Enumerable.Empty<string>(),
                new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

            configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
        });

        return services;
    }

    public static IServiceCollection AddKeyVaultIfConfigured(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        string? keyVaultUri = configuration["KeyVaultUri"];
        if (!string.IsNullOrWhiteSpace(keyVaultUri))
        {
            configuration.AddAzureKeyVault(
                new Uri(keyVaultUri),
                new DefaultAzureCredential());
        }

        return services;
    }

}
