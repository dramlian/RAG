using Docker.DotNet;
using Docker.DotNet.Models;
using System.Runtime.InteropServices;

public class TestDockerChecker
{
    private readonly DockerClient _client;

    public TestDockerChecker()
    {
        _client = new DockerClientConfiguration(new Uri(GetDockerUri()))
            .CreateClient();
    }

    private string GetDockerUri()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return "npipe://./pipe/docker_engine";

        // Linux / macOS
        return "unix:///var/run/docker.sock";
    }

    public async Task<bool> IsDockerRunning()
    {
        try
        {
            await _client.System.PingAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> IsContainerRunning(string name)
    {
        try
        {
            var list = await _client.Containers.ListContainersAsync(
                new ContainersListParameters() { All = true }
            );

            var container = list.FirstOrDefault(
                c => c.Names.Any(n => n.Contains(name, StringComparison.OrdinalIgnoreCase))
            );

            return container != null && container.State == "running";
        }
        catch
        {
            return false;
        }
    }
}
