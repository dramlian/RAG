public class OllamaServiceTests
{
    private IOllamaService _ollamaService;
    private TestDockerChecker _dockerChecker;

    public OllamaServiceTests()
    {
        _ollamaService = new OllamaService( new HttpClientService(new HttpClient()));
        _dockerChecker = new TestDockerChecker();
    }

    [Fact]
    public async Task OllamaService_DockerRunningTest()
    {
        bool isRunning = await _dockerChecker.IsDockerRunning() && await _dockerChecker.IsContainerRunning("ollama");
        Assert.True(isRunning, "Ollama Docker container is not running. Please ensure it is up before running tests.");
    }

    [Fact]
    public async Task GetOllamaResponse_BasicTest()
    {
        string prompt = "What is the capital of France?";
        var response = await _ollamaService.GetAnswer(prompt);
        Assert.NotNull(response);
        Assert.Contains("Paris", response);
    }
}