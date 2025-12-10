using Qdrant.Client.Grpc;

public class QdrantServiceTests
{
    private QdrantService _qdrantService;
    private TestDockerChecker _dockerChecker;


    public QdrantServiceTests()
    {
        _qdrantService = new QdrantService("test_collection", 3);
        _dockerChecker = new TestDockerChecker();
    }

    [Fact]
    public async Task QdrantServiceDockerRunningTest()
    {
        bool isRunning = await _dockerChecker.IsDockerRunning() && await _dockerChecker.IsContainerRunning("ollama");
        Assert.True(isRunning, "Ollama Docker container is not running. Please ensure it is up before running tests.");
    }

    [Fact]
    public async Task SimpleQdrantWorkflowTest()
    {
        await _qdrantService.CreateCollection();

        var points = new List<PointStruct>
        {
            new PointStruct
            {
                Id = 1,
                Vectors = new float[] { 0.1f, 0.2f, 0.3f },
                Payload =
                {
                    ["text"] = "chunk1"
                }
            },
            new PointStruct
            {
                Id = 2,
                Vectors = new float[] { 0.4f, 0.5f, 0.6f },
                Payload =
                {
                    ["text"] = "chunk2"
                }
            },
            new PointStruct
            {
                Id = 3,
                Vectors = new float[] { 0.7f, 0.8f, 0.9f },
                Payload =
                {
                    ["text"] = "chunk3"
                }
            }
        };

        var updateResult = await _qdrantService.UpdateInsertPoints(points);
        Assert.NotNull(updateResult);

        var searchVector = new float[] { 0.1f, 0.2f, 0.3f };
        var searchResults = await _qdrantService.SearchForPoints(2, searchVector);
        Assert.NotNull(searchResults);
        Assert.Equal(2, searchResults.Count);
        Assert.Equal(1UL, searchResults[0].Id);

        await _qdrantService.DeleteCollection();
        //TODO : Verify deletion
    }
}