using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Project.Api;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddConfigurations(this WebApplicationBuilder builder)
    {
        var env = builder.Environment;

        builder.Configuration
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
            .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
            .AddEnvironmentVariables();
        
        return builder;

    }
    
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });
        
        services.AddEndpointsApiExplorer();
        services.AddSwagger();
        services.AddProblemDetails();
        services.AddHttpContextAccessor();
        
        // TODO: Add the CurrentUserProvider service
        // services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
        
        return services;
    }
    
        private static IServiceCollection AddSwagger (this IServiceCollection services)
    {
        services.AddSwaggerGen(
            options =>
            {
                // options.SwaggerDoc("v1", new OpenApiInfo
                // {
                //     Title = "PTR API"
                // });
                // options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                // {
                //     In = ParameterLocation.Header,
                //     Description = "Please enter token",
                //     Name = "Authorization",
                //     Type = SecuritySchemeType.Http,
                //     BearerFormat = "JWT",
                //     Scheme = "bearer"
                // });
                //
                // options.AddSecurityRequirement(new OpenApiSecurityRequirement
                // {
                //     {
                //         new OpenApiSecurityScheme
                //         {
                //             Reference = new OpenApiReference
                //             {
                //                 Type=ReferenceType.SecurityScheme,
                //                 Id="Bearer"
                //             }
                //         },
                //         new string[]{}
                //     }
                // });
                
                options.TagActionsBy(
                    api =>
                    {
                        if (api.GroupName != null)
                        {
                            return new[] { api.GroupName };
                        }

                        if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                        {
                            return new[] { controllerActionDescriptor.ControllerName };
                        }

                        throw new InvalidOperationException("Unable to determine tag for endpoint.");
                    });
                options.DocInclusionPredicate((name, api) => true);
            });

        return services;
    }

    
}