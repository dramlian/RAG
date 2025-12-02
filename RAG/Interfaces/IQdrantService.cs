using Qdrant.Client.Grpc;

public interface IQdrantService
{
    public Task CreateCollection();
    public Task<UpdateResult> UpdateInsertPoints(IReadOnlyList<PointStruct> points);
    public Task<IReadOnlyList<ScoredPoint>> SearchForPoints(ulong maxNumber, float[] inputVector);
    public Task DeleteCollection();
}