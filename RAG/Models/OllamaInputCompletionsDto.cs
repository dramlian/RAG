public class Message
{
    public string? role { get; set; }
    public string? content { get; set; }
}

public class OllamaInputCompletionsDto
{
    public string? model { get; set; }
    public List<Message>? messages { get; set; }

    public OllamaInputCompletionsDto(string model, string message)
    {
        this.messages = new List<Message>() { new Message { role = "user", content = message } };
        this.model = model;
    }
}
