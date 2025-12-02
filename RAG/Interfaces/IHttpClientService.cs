public interface IHttpClientService
{
    public Task<T?> GetAsync<T>(string url);
    public Task<T?> PostAsync<T>(string url, object data);
    public Task<T?> PutAsync<T>(string url, object data);
    public Task<string> DeleteAsync(string url);
}