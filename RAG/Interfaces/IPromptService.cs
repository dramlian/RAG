public interface IPromptService
{
    public Task<string> CreateCustomQuery(string query);
}