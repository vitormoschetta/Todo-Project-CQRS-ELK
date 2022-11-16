namespace Todo.Domain.Settings;

public class AppSettings
{
    public QueueSettings QueueSettings { get; set; } = new QueueSettings();
}

public class QueueSettings
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Queue { get; set; } = string.Empty;
}