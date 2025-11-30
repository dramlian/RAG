public class OllamaInputDto
{
    public string model { get; }
    public string input { get; }

    public OllamaInputDto(string model, string input)
    {
        this.model = model;
        this.input = input;
    }
}