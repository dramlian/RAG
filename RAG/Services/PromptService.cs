using Qdrant.Client.Grpc;

public class PromptService : IPromptService
{
    private readonly IQdrantService _qdrantService;
    private readonly ISemanticVectorService _semanticVectorService;

    public PromptService(IQdrantService qdrantService, ISemanticVectorService semanticVectorService)
    {
        _qdrantService = qdrantService;
        _semanticVectorService = semanticVectorService;
    }

    public async Task<string> CreateCustomQuery(string query)
    {
        var vector = await _semanticVectorService.GetSemanticVector(query);
        IEnumerable<ScoredPoint> returned = await _qdrantService.SearchForPoints(2, vector);
        var returnedText = String.Join(",", returned.SelectMany(x => x.Payload.Values.Select(y => y.StringValue)));
        return $"{query} these are you data related hints from which you shall answer: ' {returnedText}'";
    }

}