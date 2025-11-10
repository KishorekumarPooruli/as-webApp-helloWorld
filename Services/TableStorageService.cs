namespace as_webApp_helloWorld.Services
{
    using as_webApp_helloWorld.DataModels;
    using as_webApp_helloWorld.Services.Interface;
    using Azure;
    using Azure.Data.Tables;

    public class TableStorageService : ITableStorageService
    {
        private const string TableName = "Attendees";
        private readonly IConfiguration _configuration;
        public TableStorageService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        #region "CRUD"
        public async Task<AttendeeEntity> GetAttendeeEntity(string partitionKey, string rowKey)
        {
            var instance = await this.GetAzureTableInstace();
            return await instance.GetEntityAsync<AttendeeEntity>(partitionKey, rowKey);
        }

        public async Task<List<AttendeeEntity>> GetAttendeeEntitys()
        {
            var instance = await this.GetAzureTableInstace();
            Pageable<AttendeeEntity> tableInstances = instance.Query<AttendeeEntity>();
            return new List<AttendeeEntity>(tableInstances);
        }

        public async Task UpdateAttendee(AttendeeEntity attendeeEntity)
        {
            var instance = await this.GetAzureTableInstace();
            await instance.UpsertEntityAsync(attendeeEntity);
        }

        public async Task DeleteAttendee(string partitionKey, string rowKey) 
        {
            var instance = await this.GetAzureTableInstace();
            await instance.DeleteEntityAsync(partitionKey, rowKey);
        }
        #endregion

        #region "ConnectionService"
        private async Task<TableClient> GetAzureTableInstace()
        {
            var serviceClient = new TableServiceClient(_configuration["StorageConnectionString"]);
            var tableClient = serviceClient.GetTableClient(TableName);
            await tableClient.CreateIfNotExistsAsync();
            return tableClient;
        }
        #endregion
    }
}
