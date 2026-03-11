using System.Text;
using Etmen.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Etmen.API.Extensions;

/// <summary>
/// Extension methods for IServiceCollection.
/// Keeps Program.cs clean by grouping related registrations.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>Register JWT Bearer authentication using appsettings JwtSettings section.</summary>
    public static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services, IConfiguration config)
    {
        var key = Encoding.UTF8.GetBytes(config["JwtSettings:SecretKey"]!);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey         = new SymmetricSecurityKey(key),
                ValidateIssuer           = true,
                ValidIssuer              = config["JwtSettings:Issuer"],
                ValidateAudience         = true,
                ValidAudience            = config["JwtSettings:Audience"],
                ValidateLifetime         = true,
                ClockSkew                = TimeSpan.Zero
            };

            // Pass JWT via query string for SignalR WebSocket upgrade
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = ctx =>
                {
                    var token = ctx.Request.Query["access_token"];
                    if (!string.IsNullOrEmpty(token))
                        ctx.Token = token;
                    return Task.CompletedTask;
                }
            };
        });

        return services;
    }

    /// <summary>Register Swagger/OpenAPI with JWT Bearer security definition.</summary>
    public static IServiceCollection AddSwaggerWithJwt(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title       = "Et'men Platform API",
                Version     = "v3.0",
                Description = "Predictive Healthcare Platform — ASP.NET Core 9 / .NET 9"
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name        = "Authorization",
                Type        = SecuritySchemeType.Http,
                Scheme      = "Bearer",
                BearerFormat = "JWT",
                In          = ParameterLocation.Header,
                Description = "Enter your JWT token. Example: Bearer eyJhbGci..."
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }

    /// <summary>Register MediatR, FluentValidation, Infrastructure layer, CORS, and rate limiting.</summary>
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services, IConfiguration config)
    {
        // MediatR — scans Application assembly for IRequestHandler implementations
        // + ValidationPipelineBehavior runs FluentValidation before every handler
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(
                typeof(Etmen.Application.UseCases.Auth.RegisterCommand).Assembly);
        });

        // FluentValidation — auto-registers all validators in Application assembly
        services.AddValidatorsFromAssembly(
            typeof(Etmen.Application.Validators.RegisterRequestValidator).Assembly);

        // Infrastructure (DbContext, Repositories, Services)
        services.AddInfrastructure(config);

        // CORS — allow local frontend dev + production domain
        services.AddCors(o => o.AddPolicy("FrontendPolicy", p =>
            p.WithOrigins("http://localhost:3000", "https://etmen.app")
             .AllowAnyMethod()
             .AllowAnyHeader()
             .AllowCredentials()));

        // Rate limiting — 100 req/min per IP on all routes
        services.AddRateLimiter(o =>
            o.AddFixedWindowLimiter("AuthLimit", opts =>
            {
                opts.PermitLimit  = 100;
                opts.Window       = TimeSpan.FromMinutes(1);
            }));

        return services;
    }
}
