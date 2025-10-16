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

        public async Task CreateUserAddresses(List<User> users)
        {
            string query = @"
                            INSERT INTO Addresses ([StreetNumber], [StreetName], [City], [StateOrCounty], [PostalCode], [Country])
                            OUTPUT INSERTED.ID
                            VALUES (@StreetNumber, @StreetName, @City, @StateOrCounty, @PostalCode, @Country
                            )";

            using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            foreach (User user in users)
            {
                if(user.Address == null)
                {
                    continue;
                }
                
                var result = await connection.ExecuteScalarAsync<Guid>(query,
                    new 
                    {
                        user.Address.StreetNumber,
                        user.Address.StreetName,
                        user.Address.City,
                        user.Address.StateOrCounty,
                        user.Address.PostalCode,
                        user.Address.Country
                    }
                );

                user.Address.ID = result;
            }
        }

        public async Task<IEnumerable<Address>> GetAddresses()
        {
            string query = @"SELECT * FROM Addresses";

            using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            IEnumerable<Address> addresses = await connection.QueryAsync<Address>(query);

            return addresses;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            string query = @"SELECT u.*, a.* 
                     FROM Users u
                     LEFT JOIN Addresses a ON u.AddressID = a.ID";

            using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var users = await connection.QueryAsync<User, Address, User>(
                query,
                (user, address) => {
                    user.Address = address;
                    return user;
                },
                splitOn: "ID");
            return users;
        }

        public async Task CreateUsers(List<User> users)
        {
            string query = @"INSERT INTO Users 
                            (
                                [FirstName], [LastName], [Email], [Phone], [AddressID], [Age], [Gender], [Company], [Department], [HeadshotImage], [Longitude], [Latitude], [EmploymentStart], [EmploymentEnd], [FullName]
                            ) 
                            VALUES
                            (
                                @FirstName, @LastName, @Email, @Phone, @AddressID, @Age, @Gender, @Company, @Department, @HeadshotImage, @Longitude, @Latitude, @EmploymentStart, @EmploymentEnd, @FullName 
                            )";

            using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            foreach (User user in users)
            {
                await connection.ExecuteAsync(query,
                    new
                    {
                        user.FirstName,
                        user.LastName,
                        user.Email,
                        user.Phone,
                        AddressId = user.Address?.ID,
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
