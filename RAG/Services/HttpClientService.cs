using System.Text;
using System.Text.Json;

public class HttpClientService : IDisposable, IHttpClientService
{
    private readonly HttpClient _httpClient;

    public HttpClientService()
    {
        _httpClient = new HttpClient();
    }

    public HttpClientService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<T?> GetAsync<T>(string url)
    {
        try
        {
            var json = await GetAsync(url);
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException($"Failed to deserialize response: {ex.Message}", ex);
        }
    }

    private async Task<string> GetAsync(string url)
    {
        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"HTTP request failed: {ex.Message}", ex);
        }
    }

    public async Task<T?> PostAsync<T>(string url, object data)
    {
        try
        {
            var responseJson = await PostAsync(url, data);
            return JsonSerializer.Deserialize<T>(responseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException($"Failed to deserialize response: {ex.Message}", ex);
        }
    }

    private async Task<string> PostAsync(string url, object data)
    {
        try
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"HTTP POST request failed: {ex.Message}", ex);
        }

    }

    public async Task<T?> PutAsync<T>(string url, object data)
    {
        try
        {
            var responseJson = await PutAsync(url, data);
            return JsonSerializer.Deserialize<T>(responseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException($"Failed to deserialize response: {ex.Message}", ex);
        }
    }

    private async Task<string> PutAsync(string url, object data)
    {
        try
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"HTTP PUT request failed: {ex.Message}", ex);
        }
    }

    public async Task<string> DeleteAsync(string url)
    {
        try
        {
            var response = await _httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
            throw new InvalidOperationException($"HTTP DELETE request failed: {ex.Message}", ex);
        }
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}