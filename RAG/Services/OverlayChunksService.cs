public class OverLayChunksService : IOverLayChunksService
{
    private readonly string _documentContent;

    public OverLayChunksService(string fileText)
    {
        _documentContent = fileText ?? "";
    }

    public IEnumerable<string> GetOverLayedChunks(int numberOfSentencesInChunk)
    {
        var sentences = _documentContent
            .Split('.', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Trim())
            .ToArray();

        List<IEnumerable<string>> chunks = new List<IEnumerable<string>>();
        List<string> currentChunk = new List<string>();

        for (int i = 0; i < sentences.Length; i++)
        {
            currentChunk.Add(sentences[i]);
            if ((i + 1) % numberOfSentencesInChunk == 0)
            {
                chunks.Add(currentChunk);
                currentChunk = new List<string>();
            }
        }

        if (currentChunk.Any())
        {
            chunks.Add(currentChunk);
        }

        string migratingSentence = string.Empty;

        for (int i = 0; i < chunks.Count; i++)
        {
            if (!string.IsNullOrEmpty(migratingSentence))
            {
                chunks[i] = chunks[i].Prepend(migratingSentence);
            }
            migratingSentence = chunks[i].Last();
        }

        return chunks.Select(c => string.Join(". ", c).Trim());
    }
}
