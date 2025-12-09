public class DummySemanticVectorService : ISemanticVectorService
{
    public Task<QdrantDto> GetSemanticVectors(IEnumerable<string> chunksPlainText)
    {
        throw new NotImplementedException();
    }

    public Task<float[]> GetSemanticVector(string input)
    {
        return Task.FromResult(new float[] { 0.1f, 0.2f, 0.3f });
    }
}