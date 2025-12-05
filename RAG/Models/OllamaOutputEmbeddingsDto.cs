public class Datum
{
    public float[]? embedding { get; set; }
}

public class OllamaOutputEmbeddingsDto
{
    public List<Datum>? data { get; set; }
}