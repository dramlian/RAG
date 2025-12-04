public class SemanticVectorService : ISemanticVectorService
{
    private readonly IHttpClientService _httpClientService;
    private readonly IEnumerable<string> _chunksPlainText;
    private readonly string _ollamaServiceUrl;
    private readonly string _ollamaModel;

    public SemanticVectorService(IHttpClientService httpCLientService, IEnumerable<string> chunksPlainText)
    {
        _httpClientService = httpCLientService;
        _chunksPlainText = chunksPlainText;
        _ollamaServiceUrl = "http://localhost:11434/v1/embeddings";
        _ollamaModel = "smollm";
    }

    public async Task<QdrantDto> GetSemanticVectors()
    {
        var textToVectors = new List<(string, float[])>();
        foreach (var inputChunk in _chunksPlainText)
        {
            var payload = new OllamaInputDto(_ollamaModel, inputChunk);
            var output = await _httpClientService.PostAsync<OllamaOutputDto>(_ollamaServiceUrl, payload);
            var numbers = output?.data?.FirstOrDefault()?.embedding ?? Array.Empty<float>();
            textToVectors.Add((inputChunk, numbers));
        }
        return new QdrantDto(textToVectors);
    }
}