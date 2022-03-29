using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQChat
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory(); //1
            factory.Uri = new Uri("amqp://chat:chat@localhost:5672");

            var connection = factory.CreateConnection(); //2
            var channel = connection.CreateModel();

            var exchangeName = "chat"; //4
            var queueName = Guid.NewGuid().ToString(); //nazwa kolejki musi być unikalna

            channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout); //5
            channel.QueueDeclare(queueName, true, true, true);
            channel.QueueBind(queueName, exchangeName, ""); //"" to Rooting Key

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, eventArgs) =>
            {
                var text = Encoding.UTF8.GetString(eventArgs.Body.ToArray()); //wiadomość w surowych bajtach
                Console.WriteLine(text);
            };

            channel.BasicConsume(queueName, true, consumer);

            var input = Console.ReadLine();
            while (input != "")
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                channel.BasicPublish(exchangeName, "", null, bytes);
                input = Console.ReadLine();
            }

            channel.Close(); //3
            connection.Close(); //połączenie i kanał zawsze trzeba na koniec zamknąć

        }
    }
}
