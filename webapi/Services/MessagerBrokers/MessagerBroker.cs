using System;
using System.Text;
using System.Text.Json;
using Domain.Models.SenderEntities;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace webapi.Services.MessagerBrokers
{
    public class MessagerBroker : IMessagerBroker
    {
        private readonly IConfiguration _configuration;
        private static readonly string _urlRabbitConnection = "amqps://snyrhojh:oDLO59ZkdvrV1GUxBmxflwGiuZeK9zL7@eagle.rmq.cloudamqp.com/snyrhojh";

        public MessagerBroker(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEntityToNotify(SenderEntity sendObject)
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri(_urlRabbitConnection)
            };
            
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var exchange = _configuration.GetValue<string>("RabbitMQSettings:ExchangeDeclare");

                channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout);

                string message = JsonSerializer.Serialize<SenderEntity>(sendObject);
                Console.WriteLine($"Estou aqui: {message}");
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: exchange,
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);

            }
        }
    }
}