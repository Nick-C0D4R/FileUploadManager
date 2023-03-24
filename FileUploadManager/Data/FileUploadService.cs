using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Components.Forms;

namespace FileUploadManager.Data
{
    public class FileUploadService
    {
        private BlobContainerClient _client;

        public FileUploadService(BlobContainerClient client)
        {
            _client = client;
        }

        public async Task<bool> Load(string fileName, IBrowserFile file)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string connectionString = config.GetSection("AzureSettings")["ConnectionString"];
            string storageName = config.GetSection("AzureSettings")["BlobName"];
            BlobClient blobClient = _client.GetBlobClient(fileName);
            if(!await blobClient.ExistsAsync())
            {
                await blobClient.UploadAsync(file.OpenReadStream());
            }
            return true;
        }
    }
}
