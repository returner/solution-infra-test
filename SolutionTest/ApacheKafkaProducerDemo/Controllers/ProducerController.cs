using ApacheKafkaProducerDemo.Models;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace ApacheKafkaProducerDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly string _boostrapServers = "localhost:9092";
        private readonly string _topic = "test";

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]OrderRequest orderRequest)
        {
            var message = JsonSerializer.Serialize(orderRequest);
            return Ok(await SendOrderRequestAsync(_topic, message));
        }

        private async Task<bool> SendOrderRequestAsync(string topic, string message)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _boostrapServers,
                ClientId = Dns.GetHostName()
            };

            try
            {
                using(var producer = new ProducerBuilder<Null, string>(config).Build())
                {
                    var result = await producer.ProduceAsync(topic, new Message<Null, string>
                    {
                        Value = message
                    });

                    Debug.WriteLine($"Delivery Timestamp:{result.Timestamp.UtcDateTime}");

                    return await Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
            }

            return await Task.FromResult(false);
        }
    }
}
