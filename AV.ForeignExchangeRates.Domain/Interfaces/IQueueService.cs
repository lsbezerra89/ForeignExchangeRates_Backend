using Microsoft.WindowsAzure.Storage.Queue;

namespace AV.ForeignExchangeRates.Domain.Interfaces;

public interface IQueueService
{
    CloudQueueClient QueueClient { get; }

    Task SendMessage(string queueName, string message);
}
