using BlatchAPI.Entities;
using System.Runtime.CompilerServices;

namespace BlatchAPI
{
    public static class ApiCalls
    {
        public static IApplicationBuilder MapApiCalls()
        {
            app.MapGet("/users", GetUsers).WithName(nameof("GetUsers"));
            return app;
        }

        public static async Task<IEnumerable<User>> GetUsers()
        {
            // Simulate fetching users from a database or other source
            await Task.Delay(100); // Simulate async work
            return new List<User>
        {
            new User { Id = 1, Name = "Alice" },
            new User { Id = 2, Name = "Bob" }
        };
        }
    }
}
