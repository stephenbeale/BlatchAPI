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
            string sasUrl = "REMOVED FOR SECURE SHARING";
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

        public async Task<List<Address>> GetAddressesAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("Invalid userId.", nameof(userId));
            }

            var response = await GetUsersAsync();
            if(response == null || !response.Any())
            {
                return new List<Address>();
            }
            var addresses = response.Select(u => u.Address).ToList();
            return addresses ?? new List<Address>();
        }

        //public async Task<List<string>> GetUserSkillsAsync(string userId)
        //{
        //    if (string.IsNullOrEmpty(userId))
        //    {
        //        throw new ArgumentException("Invalid userId.", nameof(userId));
        //    }

        //    var response = await GetUsersAsync();
        //    if(response == null || !response.Any())
        //    {
        //        return new List<string>();
        //    }
        //    var skills = response.Where(u => u.Id == userId).Select(u => u.Skills).FirstOrDefault();
            
        //    return skills ?? new List<string>();
        //}
        
        //public async Task<List<string>> GetUserColleaguesAsync(string userId)
        //{
        //    if (string.IsNullOrEmpty(userId))
        //    {
        //        throw new ArgumentException("Invalid userId.", nameof(userId));
        //    }

        //    var response = await GetUsersAsync();
            
        //    if(response == null || !response.Any())
        //    {
        //        return new List<string>();
        //    }

        //    var colleagues = response.Select(u => u.Colleagues).ToList();
        //    return colleagues ?? new List<string>();
        //}
    }
}
