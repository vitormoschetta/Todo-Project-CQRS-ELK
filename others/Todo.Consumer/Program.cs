using System.Text;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Todo.Consumer.Data;
using Todo.Consumer.Models;
using Todo.Shared.Utils;

var services = new ServiceCollection();
services.AddSingleton<DataFake>();

var dataFake = services.BuildServiceProvider().GetService<DataFake>();

if (dataFake == null)
    throw new Exception("DataFake is null");

ConsumeQueue();


void ConsumeQueue()
{
    var factory = new ConnectionFactory
    {
        HostName = "localhost",
        Port = 5672,
        UserName = "todouser",
        Password = "123456"
    };

    using (var connection = factory.CreateConnection())

    using (var channel = connection.CreateModel())
    {
        channel.QueueDeclare(queue: "TodoQueue",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"Mensagem recebida: {message}");

            HandlerMessage(message);
        };

        channel.BasicConsume(queue: "TodoQueue", autoAck: false, consumer: consumer);

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }
}


void HandlerMessage(string message)
{
    var response = JsonManagerSerialize.Deserialize<ResponseQueue>(message);

    switch (response)
    {
        case { Type: "Created" }:
            dataFake?.Add(response.Data);
            break;

        case { Type: "Updated" }:
            dataFake?.Update(response.Data);
            break;

        case { Type: "Deleted" }:
            dataFake?.Delete(response.Data.Id);
            break;

        default:
            break;
    }

    dataFake?.PrintAll();
}