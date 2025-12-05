public interface IOllamaService
{
    public Task<string> GetAnswer(string ragedQuestion);
}