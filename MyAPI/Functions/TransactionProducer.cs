using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MyAPI.Services.Interfaces;
using MyAPI.Models;

namespace MyAPI.Functions
{
    public class TransactionProducer
    {
		private readonly IQueueService queueService;

        public TransactionProducer(IQueueService queueService)
        {
            this.queueService = queueService ?? throw new ArgumentNullException(nameof(queueService));
        }

		[FunctionName("transaction-producer")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<TransactionRequest>(requestBody);

            await this.queueService.Enqueue("transaction-queue", data);

			return new OkObjectResult("Oh yeah");
        }
    }
}
