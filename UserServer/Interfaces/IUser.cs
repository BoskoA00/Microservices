using UserServer.Data;

namespace UserServer.Interfaces
{
    public interface IUser
    {
        public Task<List<User>?> GetUsers();
        public Task<User?> GetUserById(int id);
        public Task<User?> AddUser(User user);
        public Task<User?> DeleteUser(int id);
        public Task<User?> UpdateUser(User user);
    }
}
