using AdsServer.Data;

namespace AdsServer.Interfaces
{
    public interface IAd
    {
        public Task<Ad?> GetAdById(int id);
        public Task<List<Ad>?> GetAllAds();
        public Task<List<Ad>?> GetAdsByUser(int userId);
        public Task<Ad?> CreateAd(Ad ad);
        public Task<Ad?> UpdateAd(Ad ad);
        public Task<Ad?> DeleteAd(int id);
        public Task<List<Ad>?> DeleteAdsByUser(int userId);
    }
}
