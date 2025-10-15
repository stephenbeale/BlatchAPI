using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BlatchAPI.Database
{
    public class DataAccess
    {
        private readonly IConfiguration _configuration;

        public DataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task InsertUsers()
        {
            AzureBlobService azureBlobService = new AzureBlobService();

            var users = await azureBlobService.GetUsersAsync();

            //Insert statement dapper
            //How to handle addresses, skills, colleagues?

            using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));

            await connection.ExecuteAsync(users);
        }
    }
}
