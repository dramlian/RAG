public interface ISemanticVectorService
{
    public Task<QdrantDto> GetSemanticVectors();
}