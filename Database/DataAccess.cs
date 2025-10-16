using BlatchAPI.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace BlatchAPI.Database
{
    public class DataAccess : IDataAccess
    {
        private readonly IConfiguration _configuration;

        public DataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CreateUserAddresses(List<Address> addresses)
        {
            string query = @"
                            INSERT INTO Addresses ([StreetNumber], [StreetName], [City], [StateOrCounty], [PostalCode], [Country])
                            OUTPUT INSERTED.ID
                            VALUES (@StreetNumber, @StreetName, @City, @StateOrCounty, @PostalCode, @Country)
                            )";

            using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            foreach (Address address in addresses)
            {
                await connection.ExecuteScalarAsync<Guid>(query,
                    new 
                    {
                        address.StreetNumber,
                        address.StreetName,
                        address.City,
                        address.StateOrCounty,
                        address.PostalCode,
                        address.Country
                    });
            }
        }

        public async Task CreateUsers(List<User> users)
        {
            string query = @"INSERT INTO Users 
                            (
                            [ID], [FirstName], [LastName], [Email], [Phone], [Address], [Age], [Gender], [Company], [Department], [HeadshotImage], [Longitude], [Latitude], [EmploymentStart], [EmploymentEnd], [FullName],
                            ) 
                            VALUES
                            (
                            @ID, @FirstName, @LastName, @Email, @Phone, @Address, @Age, @Gender, @Company, @Department, @HeadshotImage, @Longitude, @Latitude, @EmploymentStart, @EmploymentEnd, @FullName, 
                            )";

            using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            foreach (User user in users)
            {
                await connection.ExecuteScalarAsync<Guid>(query,
                    new
                    {
                        user.Id,
                        user.FirstName,
                        user.LastName,
                        user.Email,
                        user.Phone,
                        user.Address,
                        user.Age,
                        user.Gender,
                        user.Company,
                        user.Department,
                        user.HeadshotImage,
                        user.Longitude,
                        user.Latitude,
                        user.EmploymentStart,
                        user.EmploymentEnd,
                        user.FullName
                    });
            }
        }
    }
}
