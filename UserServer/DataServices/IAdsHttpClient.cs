using UserServer.Data;

namespace UserServer.DataServices
{
    public interface IAdsHttpClient
    {
        public Task<List<Ad>?> GetAdsByUser(int userId);
        public Task<List<Ad>?> DeleteAdsWithUser(int userId);
    }
}
