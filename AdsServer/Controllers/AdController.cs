using AdsServer.Data;
using AdsServer.DataSerivces;
using AdsServer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdsServer.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class AdController : ControllerBase
    {
        private readonly IAd adService;
        private readonly IUserCommandHttp userCommandHttp;
        public AdController(IAd a, IUserCommandHttp userCH)
        {
            this.adService = a;
            this.userCommandHttp = userCH;

        }
        [HttpGet]
        public async Task<IActionResult> GetAllAds()
        {
            List<Ad>? ads =await this.adService.GetAllAds(); 
            return Ok(ads);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdById(int id)
        {
            Ad? ad =await this.adService.GetAdById(id);
            if(ad==null) { return BadRequest("Nepostojeci oglas"); }
            try
            {
                var owner = await userCommandHttp.getUserById(ad.userId) ;
                if(owner != null)
                {

                return Ok(new {ad,owner});
                }
                else
                {
                    return Ok(ad);
                }
            }          
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Ok(ad);
            }

        }
        [HttpPost]
        public async Task<IActionResult> CreateAd(Ad ad)
        {
            if (ad == null)
            {
                return BadRequest();
            }
            var a =this.adService.CreateAd(ad);
            return Ok(a);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdById(int id)
        {
            Ad? a =await this.adService.DeleteAd(id);
            return Ok(a);
        }
        [HttpDelete("/byUser/{userId}")]
        public async Task<IActionResult> DeleteAdsById(int userId)
        {
            List<Ad>? ads =await this.adService.DeleteAdsByUser(userId);

            return Ok(ads);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAd(Ad ad)
        {
            
            Ad? a=await this.adService.UpdateAd(ad);
            return Ok(a);
        }
        [HttpGet("/byUser/{userId}")]
        public async Task<IActionResult> GetAdsByUser(int userId)
        {
            List<Ad>? ads =await this.adService.GetAdsByUser(userId);
            return Ok(ads);
        }
    }
}
