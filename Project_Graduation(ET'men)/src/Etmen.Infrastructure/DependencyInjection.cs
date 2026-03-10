using Etmen.Application.Interfaces;
using Etmen.Application.Services;
using Etmen.Infrastructure.AI;
using Etmen.Infrastructure.Auth;
using Etmen.Infrastructure.Geo;
using Etmen.Infrastructure.Notifications;
using Etmen.Infrastructure.Persistence;
using Etmen.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Etmen.Infrastructure;

/// <summary>
/// Registers all Infrastructure dependencies. Call from Program.cs:
/// builder.Services.AddInfrastructure(builder.Configuration);
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration config)
    {
        // ── Database ─────────────────────────────────────────────────────
        services.AddDbContext<AppDbContext>(o =>
            o.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        // ── Repositories ─────────────────────────────────────────────────
        services.AddScoped<IUserRepository,               UserRepository>();
        services.AddScoped<IPatientRepository,            PatientRepository>();
        services.AddScoped<IMedicalRecordRepository,      MedicalRecordRepository>();
        services.AddScoped<IRiskAssessmentRepository,     RiskAssessmentRepository>();
        services.AddScoped<IAlertRepository,              AlertRepository>();
        services.AddScoped<IChatRepository,               ChatRepository>();
        services.AddScoped<IHealthcareProviderRepository, HealthcareProviderRepository>();
        services.AddScoped<IAppointmentRepository,        AppointmentRepository>();
        services.AddScoped<ILabResultRepository,          LabResultRepository>();
        services.AddScoped<IFamilyLinkRepository,         FamilyLinkRepository>();

        // ── Domain Services ───────────────────────────────────────────────
        services.AddScoped<IRiskCalculationService, MLModelService>();        // swap for PythonAIBridgeService if needed
        services.AddScoped<INotificationService,    EmailNotificationService>();
        services.AddScoped<ITokenService,           JwtTokenService>();
        services.AddScoped<IOcrService,             OcrService>();
        services.AddScoped<ILlmPatientChatService,  LlmPatientChatService>();
        services.AddScoped<IGeoSearchService,       GoogleGeoSearchService>();

        // ── SignalR ───────────────────────────────────────────────────────
        services.AddSignalR();

        // ── Google OAuth2 ─────────────────────────────────────────────────
        services.AddAuthentication()
                .AddGoogle(o => {
                    o.ClientId     = config["Google:ClientId"]!;
                    o.ClientSecret = config["Google:ClientSecret"]!;
                });

        // ── HTTP Clients (for Python bridge, Google Maps, LLM APIs) ───────
        services.AddHttpClient();

        return services;
    }
}
