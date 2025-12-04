public class SemanticVectorService : ISemanticVectorService
{
    private readonly IHttpClientService _httpClientService;
    private readonly string _ollamaServiceUrl;
    private readonly string _ollamaModel;

    public SemanticVectorService(IHttpClientService httpCLientService)
    {
        _httpClientService = httpCLientService;
        _ollamaServiceUrl = "http://localhost:11434/v1/embeddings";
        _ollamaModel = "smollm";
    }

    public async Task<QdrantDto> GetSemanticVectors(IEnumerable<string> chunksPlainText)
    {
        var textToVectors = new List<(string, float[])>();
        foreach (var inputChunk in chunksPlainText)
        {
            var payload = new OllamaInputDto(_ollamaModel, inputChunk);
            var output = await _httpClientService.PostAsync<OllamaOutputDto>(_ollamaServiceUrl, payload);
            var numbers = output?.data?.FirstOrDefault()?.embedding ?? Array.Empty<float>();
            textToVectors.Add((inputChunk, numbers));
        }
        return new QdrantDto(textToVectors);
    }

    public async Task<(string, float[])> GetSemanticVectors(string input)
    {
        var payload = new OllamaInputDto(_ollamaModel, input);
        var output = await _httpClientService.PostAsync<OllamaOutputDto>(_ollamaServiceUrl, payload);
        var numbers = output?.data?.FirstOrDefault()?.embedding ?? Array.Empty<float>();
        return (input, numbers);
    }
}