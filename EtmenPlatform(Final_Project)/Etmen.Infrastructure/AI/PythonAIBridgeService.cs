namespace Etmen.Infrastructure.AI;

/// <summary>
/// Optional bridge to a Python FastAPI microservice (scikit-learn / XGBoost).
/// Use when the ML.NET model is replaced with a full Python ML pipeline.
/// Configure endpoint via appsettings: AIModel:PythonServiceUrl.
/// </summary>
public sealed class PythonAIBridgeService
{
    private readonly HttpClient _http;
    public PythonAIBridgeService(HttpClient http) => _http = http;

    // TODO: POST features to Python service and deserialize response
}
