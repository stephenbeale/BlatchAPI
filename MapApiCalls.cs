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
            app.MapGet("/users", GetUsers);
            app.MapGet("/addresses", GetAddresses);

            return app;
        }

        public static async Task<IEnumerable<User>> GetUsers(AzureBlobService azureBlobService)
        {
            var result = await azureBlobService.GetUsersAsync();

            return result;
        }

        public static async Task<IEnumerable<Address>> GetAddresses(AzureBlobService azureBlobService)
        {
            var result = await azureBlobService.GetAddressesAsync();

            return result;
        }
    }
}
