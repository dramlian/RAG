using Qdrant.Client.Grpc;

public interface IQdrantServiceSearch
{
    public Task<IReadOnlyList<ScoredPoint>> SearchForPoints(ulong maxNumber, float[] inputVector);
}