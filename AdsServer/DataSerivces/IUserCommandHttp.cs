using AdsServer.Data;

namespace AdsServer.DataSerivces
{
    public interface IUserCommandHttp
    {
        public Task<User?> getUserById(int id);
    }
}
