namespace as_webApp_helloWorld.DataModels
{
    using Azure;
    using System;

    public class AttendeeEntity : Azure.Data.Tables.ITableEntity
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

        public string ProfileImage { get; set; }
        #endregion

    }
}
