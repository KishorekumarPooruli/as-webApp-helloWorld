using as_webApp_helloWorld.DataModels;

namespace as_webApp_helloWorld.Services.Interface
{
    public interface IQueueStorageService
    {
        Task SendMessage(EmailMessage emailMessage);
    }
}
