public interface ISemanticVectorService
{
    public Task<QdrantDto> GetSemanticVectors(IEnumerable<string> chunksPlainText);
    public Task<float[]> GetSemanticVector(string input);
}