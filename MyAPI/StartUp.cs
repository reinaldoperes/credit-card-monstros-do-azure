using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using MyAPI.Services.Interfaces;
using MyAPI.Services;
using MyAPI;
using MyAPI.Models;

[assembly: FunctionsStartup(typeof(StartUp))]
namespace MyAPI;

public class StartUp : FunctionsStartup
{
	public override void Configure(IFunctionsHostBuilder builder)
	{
		builder.Services.AddSingleton<IQueueService, AzureQueueService>();
	}
}
