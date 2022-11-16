using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Todo.Domain.Contracts.Services;
using Todo.Domain.Settings;

namespace Todo.Api.Services
{
    public class MessageService : IMessageService
    {
        private readonly IConnection _connection;
        private readonly string _queueName;
        private readonly ILogger<MessageService> _logger;

        public MessageService(IOptions<AppSettings> appSettings, ILogger<MessageService> logger)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = appSettings.Value.QueueSettings.Host,
                Port = appSettings.Value.QueueSettings.Port,
                UserName = appSettings.Value.QueueSettings.Username,
                Password = appSettings.Value.QueueSettings.Password
            };
            _connection = connectionFactory.CreateConnection();
            _queueName = appSettings.Value.QueueSettings.Queue;
            _logger = logger;
        }

        public void Send(string message)
        {
            _logger.LogInformation($"Enviando mensagem para a fila {_queueName}: {message}");

            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: _queueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var body = System.Text.Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: _queueName,
                    basicProperties: null,
                    body: body);

                channel.Close();
            }
        }
    }
}