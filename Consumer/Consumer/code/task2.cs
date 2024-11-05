//using System.Text;
//using RabbitMQ.Client;
//using RabbitMQ.Client.Events;
//using Newtonsoft.Json;

//namespace Consumer
//{
//    internal class task2
//    {
//        static void Main(string[] args)
//        {
//            Console.Write("Region: ");
//            string region = Console.ReadLine() ?? "";

//            var factory = new ConnectionFactory { HostName = "localhost" };
//            using var connection = factory.CreateConnection();
//            using var channel = connection.CreateModel();

//            channel.QueueDeclare(queue: region,
//                                 durable: false,
//                                 exclusive: false,
//                                 autoDelete: false,
//                                 arguments: null);

            

//            var consumer = new EventingBasicConsumer(channel);
//            consumer.Received += (model, ea) =>
//            {

//                Console.WriteLine($" Registration Received");
//                var body = ea.Body.ToArray();
//                var message = Encoding.UTF8.GetString(body);
//                string[] registrationData = message.Split(", ");

//                var user = new { 
//                    email = registrationData[0],
//                    password = registrationData[1],
//                    region = registrationData[2]
          
//                };


//                // API endpoint URL
//                string url = $"http://localhost:8081/route/{region}";

//                var jsonData = JsonConvert.SerializeObject(user);

//                // Call the synchronous method to send the POST request
//                SendPostRequest(url, jsonData);


//            };
//            channel.BasicConsume(queue: region,
//                                 autoAck: true,
//                                 consumer: consumer);

//            Console.WriteLine(" Press [enter] to exit.");
//            Console.ReadLine();
//        }




//        static void SendPostRequest(string url, string jsonData)
//        {
//            using (HttpClient client = new HttpClient())
//            {
//                // Set the content type to application/json
//                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

//                // Send POST request
//                HttpResponseMessage response = client.PostAsync(url, content).Result;

//                // Check the response status
//                if (response.IsSuccessStatusCode)
//                {
//                    // Read and print the response body
//                    string responseBody = response.Content.ReadAsStringAsync().Result;
//                    dynamic jsonObject = JsonConvert.DeserializeObject(responseBody);
//                    Console.WriteLine($"\n{jsonObject["message"]}");
//                    Console.WriteLine($"\tEmail: {jsonObject["data"]["email"]}");
//                    Console.WriteLine($"\tPassword: {jsonObject["data"]["password"]}");
//                    Console.WriteLine($"\tRegion: {jsonObject["data"]["region"]}");
//                }
//                else
//                {
//                    Console.WriteLine($"Error: {response.StatusCode}");
//                }
//            }
//        }
//    }
//}

