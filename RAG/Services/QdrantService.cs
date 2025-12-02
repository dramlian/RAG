using Qdrant.Client;
using Qdrant.Client.Grpc;
public class QdrantService : IQdrantService
{
    private QdrantClient _qdrantClient;
    private readonly string _qdrantHost;
    private readonly string _collectionName;
    private ulong _vectorSize;

    public QdrantService(string collectionName, ulong vectorSize)
    {
        _qdrantHost = "localhost";
        _qdrantClient = new QdrantClient(_qdrantHost);
        _collectionName = collectionName;
        _vectorSize = vectorSize;
    }

    public async Task CreateCollection()
    {
        await _qdrantClient.CreateCollectionAsync(_collectionName, new VectorParams
        { Size = _vectorSize, Distance = Distance.Cosine });
    }

    public async Task<UpdateResult> UpdateInsertPoints(IReadOnlyList<PointStruct> points)
    {
        return await _qdrantClient.UpsertAsync(_collectionName, points);
    }

    public async Task<IReadOnlyList<ScoredPoint>> SearchForPoints(ulong maxNumber, float[] inputVector)
    {
        return await _qdrantClient.SearchAsync(_collectionName,
        inputVector, limit: maxNumber);
    }

    public async Task DeleteCollection()
    {
        await _qdrantClient.DeleteCollectionAsync(_collectionName);
    }

}