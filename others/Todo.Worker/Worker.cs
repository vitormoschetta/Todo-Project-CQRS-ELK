using System.Net.Http.Json;

namespace Todo.Worker;

public class Worker : BackgroundService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger, HttpClient httpClient, IConfiguration configuration)
    {
        _logger = logger;
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(configuration["TodoApi:BaseUrl"]);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var i = 0;

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            var todo = new TodoItem
            {
                Title = "Test " + i,
                Done = false
            };

            var response = await _httpClient.PostAsJsonAsync("/api/todoitem", todo);
            response.EnsureSuccessStatusCode();

            await Task.Delay(1000, stoppingToken);

            i++;
        }
    }
}
