using as_webApp_helloWorld.DataModels;
using as_webApp_helloWorld.Services.Interface;
using Azure.Storage.Queues;
using Newtonsoft.Json;

namespace as_webApp_helloWorld.Services
{
    public class QueueStorageService : IQueueStorageService
    {
        private readonly IConfiguration _configuration;
        private string queueName = "as-webapp-helloworld";
        public QueueStorageService(IConfiguration configuration)
        {
           this._configuration = configuration;
        }

        public async Task SendMessage(EmailMessage emailMessage)
        {
            QueueClient queueServiceClient = new QueueClient(_configuration["StorageConnectionString"],
                queueName,
                new QueueClientOptions { MessageEncoding = QueueMessageEncoding.Base64 });
            await queueServiceClient.CreateIfNotExistsAsync();
            string messsage = JsonConvert.SerializeObject(emailMessage);
            await queueServiceClient.SendMessageAsync(messsage);
        }
    }
}
