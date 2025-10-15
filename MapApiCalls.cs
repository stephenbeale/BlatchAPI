using Azure.Storage.Blobs;
using BlatchAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Runtime.CompilerServices;

namespace BlatchAPI
{
    public static class ApiCalls
    {
        public static IApplicationBuilder MapApiCalls(this WebApplication app)
        {
            app.MapGet("/", GetBlobContainers);
            return app;
        }

        [Authorize]
        public static async Task<IEnumerable<User>> GetBlobContainers(AzureBlobService azureBlobService)
        {
            // Simulate fetching users from a database or other source
            await Task.Delay(100); // Simulate async work

            var blobClient = azureBlobService.ReadBlobAsync();

            return new List<User>
            {
                new User { Id = "1", FirstName = "Alice" },
                new User { Id = "2", FirstName = "Bob" }
            };
        }
    }
}
