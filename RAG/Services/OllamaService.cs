public class OllamaService : IOllamaService
{
    private readonly IHttpClientService _httpClientService;
    private readonly string _ollamaServiceUrl;
    private readonly string _ollamaModel;

    public OllamaService(IHttpClientService httpClientService)
    {
        _httpClientService = httpClientService;
        _ollamaServiceUrl = "http://localhost:11434/v1/chat/completions";
        _ollamaModel = "smollm";
    }

    public async Task<string> GetAnswer(string ragedQuestion)
    {
        var payload = new OllamaInputCompletionsDto(_ollamaModel, ragedQuestion);
        var output = await _httpClientService.PostAsync<OllamaOutputCompletionsDto>(_ollamaServiceUrl, payload);
        return output?.choices?.FirstOrDefault()?.message?.content ?? string.Empty;
    }

}