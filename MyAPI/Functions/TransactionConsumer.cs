using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Configuration; // Namespace for ConfigurationManager
using System.Threading.Tasks; // Namespace for Task
using Azure.Identity;
using Azure.Storage.Queues; // Namespace for Queue storage types
using Azure.Storage.Queues.Models; // Namespace for PeekedMessage

namespace MyAPI.Functions
{
    public class TransactionConsumer
    {
		[FunctionName("TransactionConsumer")]
        public void Run([QueueTrigger("transaction-queue",
            Connection = "QueueConnectionString")] string myQueueItem, ILogger log)
        {
            log.LogWarning($"***************************** Consumer *****************************");
            log.LogWarning($"New item in queue!!!!!");
            log.LogWarning($"C# Holy monster Queue trigger function processed: {myQueueItem}");
        }
    }
}
