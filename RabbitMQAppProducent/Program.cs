using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        var factory = new ConnectionFactory()
        {
            Uri = new Uri("amqp://guest:guest@localhost:5672")
        };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.ExchangeDeclare("notifications_exchange", "direct");
        var messageFrequency = TimeSpan.FromSeconds(1);

        while (true)
        {
            var messageType = GetRandomMessageType();
            var messageBody = GetMessageBody(messageType);
            var bodyEnc = Encoding.UTF8.GetBytes(messageBody);
            var GUID = Guid.NewGuid();

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            properties.MessageId = GUID.ToString();

            channel.BasicPublish(
                exchange: "notifications_exchange",
                routingKey: messageType,
                basicProperties: properties,
                body: bodyEnc);

            Console.WriteLine($"Wygenerowano zgłoszenie typu {messageType} o identyfikatorze {GUID}");
            Thread.Sleep(messageFrequency);
        }
    }

    static string GetRandomMessageType()
    {
        var random = new Random();
        var messageType = random.Next(1, 4);

        switch (messageType)
        {
            case 1:
                return "energy_measurement_requests";
            case 2:
                return "invoice_requests";
            default:
                return "fault_notifications";
        }
    }

    static string GetMessageBody(string messageType)
    {
        switch (messageType)
        {
            case "energy_measurement_requests":
                return "Prośba o pomiar energii elektrycznej";
            case "invoice_requests":
                return "Prośba o fakturę";
            default:
                return "Zgłoszenie o awarii";
        }
    }
}