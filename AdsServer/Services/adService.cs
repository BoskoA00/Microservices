using AdsServer.Data;
using AdsServer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdsServer.Services
{
    public class adService : IAd
    {
        private readonly DatabaseContext databaseContext;
        public adService(DatabaseContext db)
        {
            this.databaseContext = db;  
        }

        public async Task<Ad?> CreateAd(Ad ad)
        {
            databaseContext.Ads.Add(ad);
            await databaseContext.SaveChangesAsync();
            return ad;
        }

        public async Task<Ad?> DeleteAd(int id)
        {
            Ad? a = databaseContext.Ads.Where( a => a.Id == id).FirstOrDefault();
            if(a != null)
            {

            databaseContext.Ads.Remove(a);
            await databaseContext.SaveChangesAsync();
            return a;
            }
            return null;
        }

        public async Task<List<Ad>?> DeleteAdsByUser(int userId)
        {
            List<Ad>? ads = await databaseContext.Ads.Where(a => a.userId == userId).ToListAsync() ;
            foreach (Ad ad in ads)
            {
                databaseContext.Ads.Remove(ad);
            }
            await databaseContext.SaveChangesAsync();
            return ads;
        }

        public async Task<Ad?> GetAdById(int id)
        {
           return await databaseContext.Ads.Where(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Ad>?> GetAdsByUser(int userId)
        {
            return await databaseContext.Ads.Where(a => a.userId ==userId).ToListAsync();   
        }

        public async Task<List<Ad>?> GetAllAds()
        {
            return await databaseContext.Ads.ToListAsync();
        }

        public async Task<Ad?> UpdateAd(Ad ad)
        {
            if (ad != null)
            {
                databaseContext.Ads.Update(ad);
                await databaseContext.SaveChangesAsync();
                return ad;
            }
            else
                return null;
        }
    }
}
