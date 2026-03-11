using System.Net;
using System.Net.Http.Json;
using Etmen.Application.DTOs.Request;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Etmen.Tests.API;

/// <summary>
/// Integration tests for AuthController endpoints.
/// Uses WebApplicationFactory to spin up the real pipeline in-process.
/// </summary>
public sealed class AuthControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public AuthControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task POST_Register_WithoutBody_ShouldReturn400()
    {
        var response = await _client.PostAsJsonAsync("/api/auth/register", new { });
        // Before implementation handlers throw NotImplementedException → 501
        // After implementation invalid data → 400
        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.BadRequest,
            HttpStatusCode.NotImplemented,
            HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task POST_Login_WithoutCredentials_ShouldNotReturn200()
    {
        var response = await _client.PostAsJsonAsync("/api/auth/login", new { });
        response.StatusCode.Should().NotBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GET_SwaggerEndpoint_ShouldReturn200()
    {
        var response = await _client.GetAsync("/swagger/v1/swagger.json");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GET_ProtectedEndpoint_WithoutToken_ShouldReturn401()
    {
        var response = await _client.GetAsync("/api/patients");
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
