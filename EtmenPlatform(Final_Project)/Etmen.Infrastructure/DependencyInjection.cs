using Etmen.Application.Interfaces;
using Etmen.Application.Services;
using Etmen.Infrastructure.AI;
using Etmen.Infrastructure.Auth;
using Etmen.Infrastructure.Geo;
using Etmen.Infrastructure.Notifications;
using Etmen.Infrastructure.Persistence;
using Etmen.Infrastructure.Persistence.Repositories;
using Etmen.Infrastructure.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Etmen.Infrastructure;

/// <summary>
/// Composition root for the Infrastructure layer.
/// Call from Program.cs: builder.Services.AddInfrastructure(builder.Configuration);
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // ── Database ─────────────────────────────────────────────
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        // ── Core Repositories ─────────────────────────────────────
        services.AddScoped<IUserRepository,           UserRepository>();
        services.AddScoped<IPatientRepository,        PatientRepository>();
        services.AddScoped<IMedicalRecordRepository,  MedicalRecordRepository>();
        services.AddScoped<IRiskAssessmentRepository, RiskAssessmentRepository>();
        services.AddScoped<IAlertRepository,          AlertRepository>();
        services.AddScoped<IChatRepository,           ChatRepository>();

        // ── v3.0 Repositories ─────────────────────────────────────
        services.AddScoped<IHealthcareProviderRepository, HealthcareProviderRepository>();
        services.AddScoped<IAppointmentRepository,        AppointmentRepository>();
        services.AddScoped<ILabResultRepository,          LabResultRepository>();
        services.AddScoped<IFamilyLinkRepository,         FamilyLinkRepository>();

        // ── Core Services ─────────────────────────────────────────
        services.AddSingleton<IRiskCalculationService, MLModelService>();
        services.AddScoped<INotificationService,       EmailNotificationService>();
        services.AddScoped<ITokenService,              JwtTokenService>();

        // ── v3.0 Services ─────────────────────────────────────────
        services.AddScoped<IGeoSearchService,      GoogleGeoSearchService>();
        services.AddScoped<IOcrService,            OcrService>();
        services.AddScoped<ILlmPatientChatService, LlmPatientChatService>();
        services.AddScoped<IFamilyService,         FamilyService>();

        // ── HttpClients ───────────────────────────────────────────
        services.AddHttpClient<GoogleGeoSearchService>();
        services.AddHttpClient<LlmPatientChatService>();
        services.AddHttpClient<PythonAIBridgeService>();

        // ── SignalR ───────────────────────────────────────────────
        services.AddSignalR();

        return services;
    }
}
