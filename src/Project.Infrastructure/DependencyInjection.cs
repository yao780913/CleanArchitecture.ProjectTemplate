using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Project.Application.Common.Interfaces;
using Project.Domain.Common.Interfaces;
using Project.Infrastructure.Authentication.PasswordEncryption;
using Project.Infrastructure.Authentication.TokenGenerator;
using Project.Infrastructure.Common.Persistence;

namespace Project.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure (
        this IServiceCollection services,
        IConfiguration configuration,
        string connectionString)
    {
        return services
            .AddAuthentication(configuration)
            .AddPasswordEncryption(configuration)
            .AddPersistence(connectionString);
    }
    
    private static IServiceCollection AddPersistence (this IServiceCollection services, string connectionString)
    {
        services.AddDbContext(connectionString);
        services.AddRepositories();

        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<GymManagementDbContext>());
        
        return services;
    }

        
    private static IServiceCollection AddDbContext (this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<GymManagementDbContext>(options =>
            options.UseSqlite(connectionString));
        
        return services;
    }

    private static IServiceCollection AddPasswordEncryption (this IServiceCollection services, IConfiguration configuration)
    {
        var passwordEncryptionSettings = new PasswordEncryptionSettings();
        configuration.Bind(PasswordEncryptionSettings.Section, passwordEncryptionSettings);
        services.AddSingleton(Options.Create(passwordEncryptionSettings));
        
        services.AddScoped<IPasswordEncryptor, PasswordEncryptor>();

        return services;
    }
    
    private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        
        configuration.Bind(JwtSettings.Section, jwtSettings);
        services.AddSingleton(Options.Create(jwtSettings));
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                });
        
        return services;
    }
    
    private static IServiceCollection AddRepositories (this IServiceCollection services)
    {
        
        return services;
    }
}