using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        var messageType = "invoice_requests";
        var queueName = "invoice_requests";
        var messageProcessingTime = TimeSpan.FromSeconds(2);
        var factory = new ConnectionFactory()
        {
            //HostName = "localhost" 
            Uri = new Uri("amqp://guest:guest@localhost:5672")
        };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.ExchangeDeclare("notifications_exchange", "direct");
            channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind(queueName, "notifications_exchange", messageType, null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var deliveryTag = ea.DeliveryTag;

                Console.WriteLine("Uruchomiono obsługę zgłoszenia");
                Thread.Sleep(messageProcessingTime);
                channel.BasicAck(deliveryTag, false);
                Console.WriteLine($"Obsłużono zgłoszenie typu {messageType} o identyfikatorze: {ea.BasicProperties.MessageId}");
            };
            channel.BasicConsume(queueName, autoAck: false, consumer: consumer);

            Console.ReadLine();
        }
    }
}