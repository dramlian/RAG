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
        IEnumerable<ScoredPoint> returned = await _qdrantService.SearchForPoints(5, vector);
        var returnedText = String.Join(", ", returned.SelectMany(x => x.Payload.Values.Select(y => y.StringValue)));
        return $"ANSWER THIS QUESTION  regarding parsed monopoly text'{query}' . I am providing you hints from which you shall get the needed data to answer it properly, here they are : '{returnedText}' . YOUR ANSWER NEEDS TO BE MAXIMUM OF 1 SENTENCE LONG";
    }

}