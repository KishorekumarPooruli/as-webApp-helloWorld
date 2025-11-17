using Azure.Storage.Blobs;

namespace as_webApp_helloWorld.Services.Interface
{
    public interface IBlobStorageService
    {
        Task<BlobContainerClient> GetBlobContainerClient();

        Task<string> UploadBlobFile(IFormFile formFile, string imageName);

        Task<string> GetBlobUrl(string imageName);

        Task RemoveBlob(string imageName);
    }
}
