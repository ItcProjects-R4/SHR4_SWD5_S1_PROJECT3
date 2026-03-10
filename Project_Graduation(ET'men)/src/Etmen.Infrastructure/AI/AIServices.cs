using Etmen.Application.Services;
using Etmen.Domain.Entities;
using Etmen.Domain.Enums;
using Etmen.Domain.ValueObjects;
using Etmen.Infrastructure.AI.Models;
using Microsoft.Extensions.Configuration;

namespace Etmen.Infrastructure.AI;

// ════════════════════════════════════════════════════════════════════════════
// MLModelService  — implements IRiskCalculationService (Option A: ML.NET)
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Loads and runs the ONNX / ML.NET trained binary-classification model.
/// Registered as: services.AddScoped&lt;IRiskCalculationService, MLModelService&gt;()
/// </summary>
public class MLModelService : IRiskCalculationService
{
    private readonly IConfiguration _config;

    public MLModelService(IConfiguration config)
    {
        _config = config;
        // TODO: Load ML.NET model from config["AIModel:ModelPath"] in constructor
    }

    /// <summary>Runs feature vector through the loaded ONNX model and returns a probability score.</summary>
    public Task<RiskPredictionResult> CalculateRiskAsync(RiskFeatures features)
    {
        throw new NotImplementedException();
    }

    /// <summary>Bucketing: score &lt; 0.35 → Low, &lt; 0.70 → Medium, else High.</summary>
    public RiskLevel ClassifyRisk(double probabilityScore)
    {
        throw new NotImplementedException();
    }

    /// <summary>Returns required lab tests based on risk level and which features are elevated.</summary>
    public List<RequiredAnalysis> GetRequiredAnalyses(RiskLevel level, RiskFeatures features)
    {
        throw new NotImplementedException();
    }

    /// <summary>Maps risk level to a priority-ordered list of recommended specialities.</summary>
    public List<RecommendedDoctor> GetRecommendedDoctors(RiskLevel level)
    {
        throw new NotImplementedException();
    }

    /// <summary>Generates personalised Arabic/English lifestyle recommendations.</summary>
    public List<string> GetRecommendations(RiskLevel level, RiskFeatures features)
    {
        throw new NotImplementedException();
    }
}

// ════════════════════════════════════════════════════════════════════════════
// PythonAIBridgeService  — Option B: delegates to FastAPI microservice
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// HTTP bridge to an external Python FastAPI AI microservice.
/// Use when the full Python ML ecosystem (scikit-learn, PyTorch) is required.
/// Swap in via DI in place of MLModelService.
/// </summary>
public class PythonAIBridgeService : IRiskCalculationService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public PythonAIBridgeService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public Task<RiskPredictionResult> CalculateRiskAsync(RiskFeatures features)
    {
        throw new NotImplementedException();
    }

    public RiskLevel ClassifyRisk(double probabilityScore)
    {
        throw new NotImplementedException();
    }

    public List<RequiredAnalysis> GetRequiredAnalyses(RiskLevel level, RiskFeatures features)
    {
        throw new NotImplementedException();
    }

    public List<RecommendedDoctor> GetRecommendedDoctors(RiskLevel level)
    {
        throw new NotImplementedException();
    }

    public List<string> GetRecommendations(RiskLevel level, RiskFeatures features)
    {
        throw new NotImplementedException();
    }
}

// ════════════════════════════════════════════════════════════════════════════
// OcrService  (NEW — v3.0) — implements IOcrService
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Extracts structured lab values from uploaded images/PDFs.
/// Provider is configurable: "Tesseract" (local) or "AzureComputerVision" (cloud).
/// Configured via appSettings: Ocr:Provider, Ocr:ApiEndpoint, Ocr:ApiKey.
/// </summary>
public class OcrService : IOcrService
{
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;

    public OcrService(IConfiguration config, HttpClient httpClient)
    {
        _config = config;
        _httpClient = httpClient;
    }

    /// <summary>
    /// Pipeline: 1) Route to Tesseract or Azure CV based on config
    ///           2) Extract raw text  3) Parse key values (HbA1c, glucose, lipids, kidney)
    ///           4) Return OcrResult with both raw text and structured fields
    /// </summary>
    public Task<OcrResult> ExtractLabValuesAsync(Stream fileStream, string fileName, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}

// ════════════════════════════════════════════════════════════════════════════
// LlmPatientChatService  (NEW — v3.0) — implements ILlmPatientChatService
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Sends patient questions to the configured LLM (Anthropic Claude, OpenAI, or Azure OpenAI).
/// Injects patient health context into the system prompt for personalised, data-aware responses.
/// Configured via appSettings: LlmChat:Provider, LlmChat:ApiKey, LlmChat:Model, LlmChat:MaxTokens.
/// </summary>
public class LlmPatientChatService : ILlmPatientChatService
{
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;

    public LlmPatientChatService(IConfiguration config, HttpClient httpClient)
    {
        _config = config;
        _httpClient = httpClient;
    }

    /// <summary>
    /// Constructs a context-aware system prompt then calls the LLM API.
    /// Sets SuggestDoctorChat=true for clinical decision questions.
    /// Sets DetectedCrisis=true if message contains crisis/self-harm language.
    /// </summary>
    public Task<PatientChatResponse> AskAsync(
        string patientMessage,
        PatientContext context,
        ChatHistory history,
        CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>Builds the personalised system prompt injecting the patient's live health data.</summary>
    private string BuildSystemPrompt(PatientContext ctx)
    {
        throw new NotImplementedException();
    }
}

// ════════════════════════════════════════════════════════════════════════════
namespace Etmen.Infrastructure.AI.Models;

// ML.NET Model Input / Output DTOs
// ════════════════════════════════════════════════════════════════════════════

/// <summary>Input schema that the ML.NET pipeline expects. Maps to RiskFeatures.</summary>
public class RiskModelInput
{
    public float Age { get; set; }
    public float BMI { get; set; }
    public float BloodSugar { get; set; }
    public float BloodPressureSystolic { get; set; }
    public float BloodPressureDiastolic { get; set; }
    public float IsSmoker { get; set; }
    public float FamilyHistory { get; set; }
    public float ActivityLevel { get; set; }
}

/// <summary>Output schema returned by the ML.NET binary classification model.</summary>
public class RiskModelOutput
{
    /// <summary>Predicted label (true = high risk).</summary>
    public bool PredictedLabel { get; set; }

    /// <summary>Probability score [0, 1] for the positive class (high risk).</summary>
    public float Probability { get; set; }

    public float Score { get; set; }
}
