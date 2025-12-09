using Qdrant.Client.Grpc;

public class DummyQdrantService : IQdrantServiceSearch
{
    public Task<IReadOnlyList<ScoredPoint>> SearchForPoints(ulong maxNumber, float[] inputVector)
    {
        var scoredPoints = new List<ScoredPoint>
        {
            new ScoredPoint
            {
                Payload = { ["content"] = new Value { StringValue = "chunk1" } }
            },
            new ScoredPoint
            {
                Payload = { ["content"] = new Value { StringValue = "chunk2" } }
            },
            new ScoredPoint
            {
                Payload = { ["content"] = new Value { StringValue = "chunk3" } }
            }
        };
        return Task.FromResult<IReadOnlyList<ScoredPoint>>(scoredPoints);
    }
}
