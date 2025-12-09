public class PromptServiceTests
{
    private IPromptService _promptService;
    public PromptServiceTests()
    {
        _promptService = new PromptService(new DummyQdrantService(), new DummySemanticVectorService());
    }

    [Fact]
    public async Task ResponseStructureTest()
    {
        var result = await _promptService.CreateCustomQuery("What is machine learning?");
        Assert.NotNull(result);
        Assert.Contains("You must answer only using the context below", result);
        Assert.Contains("Context:", result);
        Assert.Contains("Question: What is machine learning?", result);
        Assert.Contains("chunk1", result);
        Assert.Contains("chunk2", result);
        Assert.Contains("chunk3", result);
    }
}

