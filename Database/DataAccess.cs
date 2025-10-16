using BlatchAPI.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Text.Json;

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

            var userDbHelpers = await connection.QueryAsync<UserDbHelper, Address, UserDbHelper>(
                query,
                (userDb, address) => {
                    userDb.Address = address;
                    return userDb;
                },
                splitOn: "ID"
            );

            var users = userDbHelpers.Select(userDb => new User
            {
                ID = userDb.ID,
                FirstName = userDb.FirstName,
                LastName = userDb.LastName,
                Email = userDb.Email,
                Phone = userDb.Phone,
                Age = userDb.Age,
                Gender = userDb.Gender,
                Company = userDb.Company,
                Department = userDb.Department,
                HeadshotImage = userDb.HeadshotImage,
                Longitude = userDb.Longitude,
                Latitude = userDb.Latitude,
                EmploymentStart = userDb.EmploymentStart,
                EmploymentEnd = userDb.EmploymentEnd,
                FullName = userDb.FullName,
                Address = userDb.Address,
                Skills = string.IsNullOrEmpty(userDb.Skills)
        ? null
        : JsonSerializer.Deserialize<List<string>>(userDb.Skills),
                Colleagues = string.IsNullOrEmpty(userDb.Colleagues)
        ? null
        : JsonSerializer.Deserialize<List<string>>(userDb.Colleagues)
            });

            return users;
        }

        public async Task DeleteAllUsers()
        {
            string deleteUsersQuery = "DELETE FROM Users";
            using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync(deleteUsersQuery);
        }

        public async Task DeleteAllAddresses()
        {
            string deleteAddressesQuery = "DELETE FROM Addresses";
            using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync(deleteAddressesQuery);
        }

        public async Task CreateUsers(List<User> users)
        {
            string query = @"INSERT INTO Users 
                            (
                                [FirstName], [LastName], [Email], [Phone], [AddressID], [Age], [Gender], [Company], [Department], [HeadshotImage], [Longitude], [Latitude], [Skills], [Colleagues], [EmploymentStart], [EmploymentEnd], [FullName]
                            ) 
                            VALUES
                            (
                                @FirstName, @LastName, @Email, @Phone, @AddressID, @Age, @Gender, @Company, @Department, @HeadshotImage, @Longitude, @Latitude, @Skills, @Colleagues, @EmploymentStart, @EmploymentEnd, @FullName 
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
                        Skills = user.Skills == null ? null : JsonSerializer.Serialize(user.Skills),
                        Colleagues = user.Colleagues == null ? null : JsonSerializer.Serialize(user.Colleagues),
                        user.EmploymentStart,
                        user.EmploymentEnd,
                        user.FullName
                    });
            }
        }

        private class UserDbHelper
        {
            public Guid ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public Address Address { get; set; }
            public int? Age { get; set; }
            public string Gender { get; set; }
            public string Company { get; set; }
            public string Department { get; set; }
            public string HeadshotImage { get; set; }
            public double? Longitude { get; set; }
            public double? Latitude { get; set; }
            public string Skills { get; set; }  // String from DB
            public string Colleagues { get; set; }  // String from DB
            public DateTimeOffset? EmploymentStart { get; set; }
            public DateTimeOffset? EmploymentEnd { get; set; }
            public string FullName { get; set; }
        }
    }
}
