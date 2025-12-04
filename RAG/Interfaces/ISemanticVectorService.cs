public interface ISemanticVectorService
{
    public Task<QdrantDto> GetSemanticVectors(IEnumerable<string> chunksPlainText);
    public Task<(string, float[])> GetSemanticVectors(string input);
}