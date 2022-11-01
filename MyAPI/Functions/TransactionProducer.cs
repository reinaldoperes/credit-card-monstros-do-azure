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
            var name = data?.Name;
			var cardNumber = data?.CardNumber;
			var expirationDate = data?.ExpirationDate;
			var securityCode = data?.SecurityCode;

            var responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

			await this.queueService.Enqueue("transaction-queue", new
			{
				Name = name,
				CardNumber = cardNumber,
				ExpirationDate = expirationDate,
				SecurityCode = securityCode
			});

			return new OkObjectResult(responseMessage);
        }
    }
}
