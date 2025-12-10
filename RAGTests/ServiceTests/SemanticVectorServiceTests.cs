public class SemanticVectorServiceTests
{
    private ISemanticVectorService _semanticVectorService;
    private TestDockerChecker _dockerChecker;

    public SemanticVectorServiceTests()
    {
        _semanticVectorService = new SemanticVectorService(new HttpClientService());
        _dockerChecker = new TestDockerChecker();
    }

    [Fact]
    public async Task OllamaServiceDockerRunningTest()
    {
        bool isRunning = await _dockerChecker.IsDockerRunning() && await _dockerChecker.IsContainerRunning("ollama");
        Assert.True(isRunning, "Ollama Docker container is not running. Please ensure it is up before running tests.");
    }

    [Fact]
    public async Task GetSemanticVectorTest()
    {
        string testInput = "This is a test sentence.";
        var vector = await _semanticVectorService.GetSemanticVector(testInput);
        
        Assert.NotNull(vector);
        Assert.NotEmpty(vector);
        Assert.All(vector, v => Assert.True(v >= -1.0f && v <= 1.0f || v == 0.0f, "Vector values should be normalized or zero"));
    }

    [Fact]
    public async Task GetSemanticVectorsTest()
    {
        var testInputs = new List<string>
        {
            "First test chunk.",
            "Second test chunk.",
            "Third test chunk."
        };
        
        var qdrantDto = await _semanticVectorService.GetSemanticVectors(testInputs);
        
        Assert.NotNull(qdrantDto);
        Assert.NotNull(qdrantDto.points);
        Assert.Equal(3, qdrantDto.points.Count);
        
        for (int i = 0; i < qdrantDto.points.Count; i++)
        {
            var point = qdrantDto.points[i];
            Assert.Equal((ulong)i, point.Id);
            Assert.NotNull(point.Vectors);
            Assert.True(point.Payload.ContainsKey("text"));
        }
    }
    
}