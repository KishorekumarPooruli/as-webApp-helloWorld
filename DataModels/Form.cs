namespace as_webApp_helloWorld.DataModels
{
    using Azure;
    using Azure.Data.Tables;
    using System;

    public class AttendeeEntity : ITableEntity
    {
        #region "Default Columns"
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        #endregion

        #region "Custom Columns"
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        #endregion

    }
}
