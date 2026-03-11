// <summary>
// Application entry point. Configures the ASP.NET Core 9 pipeline:
// authentication, authorization, MediatR, FluentValidation, SignalR, Swagger, and CORS.
// </summary>
using Etmen.API.Exceptions;
using Etmen.API.Extensions;
using Etmen.API.Middleware;
using Etmen.Infrastructure.SignalR;

var builder = WebApplication.CreateBuilder(args);

// ── Service Registration ───────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithJwt();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddAuthorization();

// Google OAuth2
builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId     = builder.Configuration["Google:ClientId"]!;
        options.ClientSecret = builder.Configuration["Google:ClientSecret"]!;
    });

// .NET 8+ IExceptionHandler — order matters: Validation first, then Global
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// ── Middleware Pipeline ─────────────────────────────────────────────
app.UseExceptionHandler();
app.UseMiddleware<RequestLoggingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Et'men Platform API v3.0");
        c.RoutePrefix = "swagger";
    });
}

app.UseCors("FrontendPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();

app.MapControllers();
app.MapHub<ChatHub>("/hubs/chat");

app.Run();

// Required for WebApplicationFactory in integration tests
public partial class Program { }
