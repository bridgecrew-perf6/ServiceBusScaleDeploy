using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace ServiceBusScaleDeploy
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([ServiceBusTrigger("%ServiceBusQueueName%", Connection = "ServiceBusConnection")]string myQueueItem,
            [ServiceBus("%ServiceBusQueueName%", Connection = "ServiceBusConnection")] IAsyncCollector<string> outputEvents,
            ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");

            if (Environment.GetEnvironmentVariable("AddServiceBusQueueMessages") == "true")
            {
                for (int x = 0; x < Convert.ToInt32(Environment.GetEnvironmentVariable("LoopCount")); x++)
                {
                    outputEvents.AddAsync("Testing");
                }
            }
        }
    }
}
