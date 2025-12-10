public class OverLayChunksServiceTests
{
    private IOverLayChunksService _overLayChunksService;
    public OverLayChunksServiceTests()
    {
        string fileText = "This is the first sentence. This is the second sentence. This is the third sentence. This is the fourth sentence. This is the fifth sentence. This is the sixth sentence.";
        _overLayChunksService = new OverLayChunksService(fileText);
    }

    [Fact]
    public void GetOverLayedChunksTest()
    {
        var result = _overLayChunksService.GetOverLayedChunks(2).ToList();

        Assert.Equal(3, result.Count);
        Assert.Equal("This is the first sentence. This is the second sentence", result[0]);
        Assert.Equal("This is the second sentence. This is the third sentence. This is the fourth sentence", result[1]);
        Assert.Equal("This is the fourth sentence. This is the fifth sentence. This is the sixth sentence", result[2]);
    }
}