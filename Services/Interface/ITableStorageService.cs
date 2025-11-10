using as_webApp_helloWorld.DataModels;

namespace as_webApp_helloWorld.Services.Interface
{
    public interface ITableStorageService
    {
        Task<AttendeeEntity> GetAttendeeEntity(string partitionKey, string rowKey);
        Task<List<AttendeeEntity>> GetAttendeeEntitys();
        Task UpdateAttendee(AttendeeEntity attendeeEntity);
        Task DeleteAttendee(string partitionKey, string rowKey);
    }
}
