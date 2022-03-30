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
            Console.WriteLine("Please specify a chat room name:");
            var chatRoomName = Console.ReadLine();

            var exchangeName = "chat2"; 

            // Create unique queue name for this instance
            var queueName = Guid.NewGuid().ToString(); //nazwa kolejki musi być unikalna

            // Connect to RabbitMQ
            var factory = new ConnectionFactory(); 
            factory.Uri = new Uri("amqp://chat:chat@localhost:5672");
            var connection = factory.CreateConnection(); 
            var channel = connection.CreateModel();

            // Declare exchange and queue
            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct); 
            channel.QueueDeclare(queueName, true, true, true);
            channel.QueueBind(queueName, exchangeName, chatRoomName); //chatRoomName to RootingKey

            // Subscribe to incoming messages
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, eventArgs) =>
            {
                var text = Encoding.UTF8.GetString(eventArgs.Body.ToArray()); //wiadomość w surowych bajtach
                Console.WriteLine(text);
            };

            channel.BasicConsume(queueName, true, consumer);

            // Read input
            var input = Console.ReadLine();
            while (input != "")
            {
                // Remove line we just typed or we would see it twice
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                ClearCurrentConsoleLine();

                // Send outgoing message
                var bytes = Encoding.UTF8.GetBytes(input);
                channel.BasicPublish(exchangeName, chatRoomName, null, bytes);
                input = Console.ReadLine();
            }

            // Disconnect
            channel.Close();
            connection.Close();
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
