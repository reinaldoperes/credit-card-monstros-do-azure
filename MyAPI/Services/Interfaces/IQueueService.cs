using System.Threading.Tasks;

namespace MyAPI.Services.Interfaces;

public interface IQueueService
{
	Task Enqueue(string queue, object data);
}
