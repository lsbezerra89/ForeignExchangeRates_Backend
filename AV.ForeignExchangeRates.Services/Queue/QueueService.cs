using AV.ForeignExchangeRates.Domain.Configuration;
using AV.ForeignExchangeRates.Domain.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace AV.ForeignExchangeRates.Services.Queue;

public class QueueService : IQueueService
{
    public CloudQueueClient QueueClient { get; }

    public QueueService(IOptions<AppSettings> settings)
    {
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(settings.Value.QueueConnectionString);
        QueueClient = storageAccount.CreateCloudQueueClient();
    }

    public async Task SendMessage(string queueName, string message)
    {
        var queueMessage = new CloudQueueMessage(message);

        await QueueClient.GetQueueReference(queueName).AddMessageAsync(queueMessage);
    }
}
