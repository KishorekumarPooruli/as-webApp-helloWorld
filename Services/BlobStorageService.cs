

namespace as_webApp_helloWorld.Services
{
    using as_webApp_helloWorld.Services.Interface;
    using Azure.Storage.Blobs;
    using Azure.Storage.Blobs.Models;
    using Azure.Storage.Sas;

    public class BlobStorageService : IBlobStorageService
    {
        private readonly IConfiguration configuration;
        private string containerName = "as-webapp-helloworld";
        
        public BlobStorageService(IConfiguration configuration)
        {
           this.configuration = configuration;
        }

        public async Task<BlobContainerClient> GetBlobContainerClient()
        {
            BlobContainerClient containerClient = new 
                BlobContainerClient(configuration["StorageConnectionString"], containerName);
            await containerClient.CreateIfNotExistsAsync();
            return containerClient;
        }

        public async Task<string> UploadBlobFile(IFormFile formFile, string imageName)
        {
            string blobName = $"{imageName}{Path.GetExtension(formFile.FileName)}";
            var blobContainerClient = await GetBlobContainerClient();
            var memmoryStream = new MemoryStream();
            formFile.CopyTo(memmoryStream);
            memmoryStream.Position = 0;
            var client = await blobContainerClient.UploadBlobAsync(blobName, memmoryStream);
            return blobName;
        }

        public async Task<string> GetBlobUrl(string imageName)
        {
            var blobContainerClient = await GetBlobContainerClient();
            var blob = blobContainerClient.GetBlobClient(imageName);

            BlobSasBuilder blobSasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = blob.BlobContainerName,
                BlobName = blob.Name,
                ExpiresOn = DateTime.UtcNow.AddMilliseconds(500),
                Protocol = SasProtocol.Https,
                Resource = "b"
            };

            blobSasBuilder.SetPermissions(BlobAccountSasPermissions.Read);

            return blob.GenerateSasUri(blobSasBuilder).ToString();
        }


        public async Task RemoveBlob(string imageName)
        {
            var blobContainerClient = await GetBlobContainerClient();
            var blob = blobContainerClient.GetBlobClient(imageName);
            await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
        }


    }
}
