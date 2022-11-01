using Azure.Storage.Queues;
using Microsoft.Extensions.Options;
using MyAPI.Services.Interfaces;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using MyAPI.Models;

namespace MyAPI.Services;

/// <summary>
/// Class to process all the queue operations
/// </summary>
public class AzureQueueService : IQueueService
{
	private readonly StorageConfiguration config;

	/// <summary>
	/// Default constructor injecting the parameters
	/// </summary>
	public AzureQueueService()
	{
		this.config = new StorageConfiguration { AccessKey = Environment.GetEnvironmentVariable("QueueConnectionString") };

		// Validate if the configuration was provided
		if (string.IsNullOrWhiteSpace(this.config.AccessKey))
			throw new ArgumentNullException("Azure Storage account Key not provided.");
	}


	/// <summary>
	/// Internal method to create the queue reference to manage items
	/// </summary>
	/// <returns></returns>
	private QueueClient QueueReference(string queue)
	{
		// Connect to the storage account's blob endpoint 
		var serviceClient = new QueueServiceClient(this.config.AccessKey);

		// Create the blob storage container
		var queueClient = serviceClient.GetQueueClient(queue);
		queueClient.CreateIfNotExists();

		return queueClient;
	}

	/// <summary>
	/// Method to add a new message to the queue service
	/// </summary>
	/// <param name="queue">Queue name to be added</param>
	/// <param name="data">Data to be added in the queue</param>
	/// <returns></returns>
	public async Task Enqueue(string queue, object data)
	{
		// Get queue reference
		var client = QueueReference(queue);

		// Serialize the data object to be added in the queue
		var serialized = JsonSerializer.Serialize(data);

		// Send the message to the queue
		await client.SendMessageAsync(serialized);
	}
}
