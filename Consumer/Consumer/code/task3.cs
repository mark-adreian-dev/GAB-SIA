//using System.Text;
//using RabbitMQ.Client;
//using RabbitMQ.Client.Events;
//using Newtonsoft.Json;

//namespace Consumer
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {

//            var factory = new ConnectionFactory { HostName = "localhost" };
//            using var connection = factory.CreateConnection();
//            using var channel = connection.CreateModel();

//            channel.QueueDeclare(queue: "aggregate",
//                                 durable: false,
//                                 exclusive: false,
//                                 autoDelete: false,
//                                 arguments: null);



//            var consumer = new EventingBasicConsumer(channel);
//            int messageCount = 0;
//            string aggregatedMessage = string.Empty;
//            consumer.Received += (model, ea) =>
//            {

//                Console.WriteLine($" Registration Received");
//                var body = ea.Body.ToArray();
//                var message = Encoding.UTF8.GetString(body);
//                string[] messageData = message.Split(", ");
//                string msg = $"\n\nEmail: {messageData[0]}\nPassword:{messageData[1]}\nRegion: {messageData[2]}";

//                // Aggregate messages
//                aggregatedMessage += msg + " ";

//                messageCount++;

//                // If two messages have been received, display the aggregated message
//                if (messageCount == 2)
//                {
//                    Console.WriteLine($"Aggregated Message: \n{aggregatedMessage.Trim()}");
//                    // Reset for the next aggregation if needed
//                    messageCount = 0;
//                    aggregatedMessage = string.Empty;
//                }



//            };
//            channel.BasicConsume(queue: "aggregate",
//                                 autoAck: true,
//                                 consumer: consumer);

//            Console.WriteLine(" Press [enter] to exit.");
//            Console.ReadLine();
//        }





//    }
//}

