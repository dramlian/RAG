public class PromptService : IPromptService
{
    private readonly IQdrantServiceSearch _qdrantService;
    private readonly ISemanticVectorService _semanticVectorService;

    public PromptService(IQdrantServiceSearch qdrantService, ISemanticVectorService semanticVectorService)
    {
        _qdrantService = qdrantService;
        _semanticVectorService = semanticVectorService;
    }

    public async Task<string> CreateCustomQuery(string query)
    {
        var vector = await _semanticVectorService.GetSemanticVector(query);
        var returned = await _qdrantService.SearchForPoints(3, vector);
        var foundChunks = returned.SelectMany(x => x.Payload.Values.Select(y => y.StringValue)).ToArray();
        return $"You must answer only using the context below. If the context does not contain the answer, say “The context does not provide this information.” Context: <<<{foundChunks[0]} {foundChunks[1]} {foundChunks[2]}>>> Question: {query}";
    }

}