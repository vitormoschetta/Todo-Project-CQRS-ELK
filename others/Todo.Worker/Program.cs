using Todo.Worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHttpClient<Worker>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
