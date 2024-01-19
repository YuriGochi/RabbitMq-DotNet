
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Rabbit.Models.Entities;
using System.Text.Json;

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "admin",
    Password = "123456"
};
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "rabbitMensagesQueue",
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var json = Encoding.UTF8.GetString(body);

        RabbitMessage mensagem = JsonSerializer.Deserialize<RabbitMessage>(json);

        System.Threading.Thread.Sleep(1000);

        Console.WriteLine($"Titulo: {mensagem.Titulo}; Texto={mensagem.Texto}; Id={mensagem.Id}");
    };
    channel.BasicConsume(queue: "rabbitMensagesQueue",
                         autoAck: true,
                         consumer: consumer);

    Console.WriteLine(" Press [enter] to exit.");
    Console.ReadLine();
}