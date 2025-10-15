using Azure.Storage;
using Azure.Storage.Blobs;
using BlatchAPI.Entities;

namespace BlatchAPI
{
    public class AzureBlobService
    {
        readonly BlobClient _blobClient;
            
        public AzureBlobService()
        {
            string sasUrl = "https://blatchfordinterview.blob.core.windows.net/source-data/contacts.json?sp=r&st=2025-10-13T10:26:21Z&se=2025-12-31T23:59:59Z&spr=https&sv=2024-11-04&sr=b&sig=IPcCEW7%2BDR2l809lKZxTfn93OWxiJEIJICFWIcs%2Bu3E%3D";
            _blobClient = new BlobClient(new Uri(sasUrl.TrimEnd()));
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var response = await _blobClient.DownloadAsync();
            using var streamReader = new StreamReader(response.Value.Content);
            string jsonContent = await streamReader.ReadToEndAsync();
            var users = System.Text.Json.JsonSerializer.Deserialize<List<User>>(jsonContent);

            return users ?? new List<User>();
        }

        public async Task<List<Address>> GetAddressesAsync()
        {
            var response = await GetUsersAsync();
            if(response == null || !response.Any())
            {
                return new List<Address>();
            }
            var addresses = response.Select(u => u.Address).ToList();
            return addresses ?? new List<Address>();
        }
    }
}
