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
            //app.MapGet("/addresses/", GetAddresses);
            //app.MapGet("/skills/{userId}", GetSkills);

            return app;
        }

        public static async Task<IEnumerable<User>> GetUsers(AzureBlobService azureBlobService)
        {
            var result = await azureBlobService.GetUsersAsync();

            return result;
        }

        //public static async Task<IEnumerable<Address>> GetAddresses(AzureBlobService azureBlobService, string userId)
        //{
        //    var result = await azureBlobService.GetAddressesAsync();

        //    return result;
        //}
        
        //public static async Task<IEnumerable<string>> GetSkills(AzureBlobService azureBlobService, string userId)
        //{
        //    var result = await azureBlobService.GetSkillsAsync(userId);

        //    return result;
        //}
        
        //public static async Task<IEnumerable<string>> GetColleagues(AzureBlobService azureBlobService, string userId)
        //{
        //    var result = await azureBlobService.GetColleaguesAsync(userId);

        //    return result;
        //}
    }
}
