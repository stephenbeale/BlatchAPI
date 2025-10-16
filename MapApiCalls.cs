using Azure.Storage.Blobs;
using BlatchAPI.Database;
using BlatchAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Runtime.CompilerServices;

namespace BlatchAPI
{
    public static class ApiCalls
    {
        public static IApplicationBuilder MapApiCalls(this WebApplication app)
        {
            app.MapGet("/users", GetUsers);
            app.MapPost("/users", CreateUsers);
            app.MapPut("/users/{id}", UpdateUserById);
            app.MapDelete("/users/{id}", DeleteUserById);

            return app;
        }

        public static async Task<IEnumerable<User>> GetUsers(AzureBlobService azureBlobService)
        {
            var result = await azureBlobService.GetUsersAsync();

            return result;
        }

        public static async Task<Ok<string>> CreateUsers(AzureBlobService azureBlobService, IDataAccess dataAccess)
        {
            var users = await azureBlobService.GetUsersAsync();
            await dataAccess.CreateUsers(users);
            return TypedResults.Ok("Users created");
        }

        public static async Task<Ok<string>> UpdateUserById(AzureBlobService azureBlobService, IDataAccess dataAccess, Guid userId)
        {
            var users = await azureBlobService.GetUsersAsync();
            await dataAccess.UpdateUserById(userId);
            return TypedResults.Ok("Users updated");
        }

        private static async Task DeleteUserById(Guid userId)
        {
            throw new NotImplementedException();
        }

        private static async Task UpdateUsers(HttpContext context)
        {
            throw new NotImplementedException();
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
