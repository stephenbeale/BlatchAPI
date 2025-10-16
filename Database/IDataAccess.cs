using BlatchAPI.Entities;

namespace BlatchAPI.Database
{
    public interface IDataAccess
    {
        Task CreateUsers(List<User> users);
    }
}