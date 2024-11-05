using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;


var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "registration",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

Console.WriteLine(" [*] Waiting for messages.");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += static (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine($" [x] Received {message}");

    // Define the API endpoint
            string url = "https://jsonplaceholder.typicode.com/posts/1";

            // Create an instance of HttpClient
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Send a GET request to the specified URL
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    response.EnsureSuccessStatusCode();
                    string jsonResponse = response.Content.ReadAsStringAsync().Result;

                    // Check if the request was successful
                    response.EnsureSuccessStatusCode();

                    // Read the response content as a string
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    Response responseData = JsonSerializer.Deserialize<Response>(responseBody);
                    // Print the response to the console
    

                    Console.WriteLine(jsonResponse);
                }
                catch (HttpRequestException e)
                {
                    // Catch any errors and print the error message
                    Console.WriteLine($"Request error: {e.Message}");
                }
            };
    
};
channel.BasicConsume(queue: "registration",
                     autoAck: true,
                     consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();


public class Response
{
    public String userId { set; get; }
    public String id { set; get; }
    public String title { set; get; }
    public String body { set; get; }


}