public class OllamaInputEmbeddingsDto
{
    public string model { get; }
    public string input { get; }

    public OllamaInputEmbeddingsDto(string model, string input)
    {
        this.model = model;
        this.input = input;
    }
}