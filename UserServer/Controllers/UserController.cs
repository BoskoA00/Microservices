using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserServer.Data;
using UserServer.DataServices;
using UserServer.Interfaces;

namespace UserServer.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IAdsHttpClient _adsHttpClient;
        private readonly IUser userService;
        public UserController(IUser us,IAdsHttpClient adsHttp)
        {
            this.userService = us;
            this._adsHttpClient = adsHttp;
        }

        [HttpGet]
        public  async Task<IActionResult> GetUsers()
        {
            List<User>? users =await  userService.GetUsers();

            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            User? user =await userService.GetUserById(id);
            try
            {
                List<Ad>? ads = await _adsHttpClient.GetAdsByUser(id);
                Console.WriteLine(Environment.NewLine + "4:" + ads);
                return Ok(new {user,ads});

            }
            catch (Exception ex)
            {
                Console.WriteLine(Environment.NewLine+"3:" + ex.Message);
            }
            if(user == null)
            {
                return BadRequest("Nepostojeci korisnik");
            }
            else
                return Ok(user);

        }
        [HttpGet("/userPickUpById/{id}")]
        public async Task<IActionResult> pickUpUserById(int id)
        {
            User? user = await userService.GetUserById(id);
            return Ok(user);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            User? user =await userService.DeleteUser(id);
            var ads=await _adsHttpClient.DeleteAdsWithUser(id);
            return Ok(new {user,ads});
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(User user)
        {
            if (user == null)
            {
                return BadRequest("Morate uneti podatke");
            }
            else
            {
                User? u =await userService.UpdateUser(user);
                return Ok(u);
            }
        }

    }
}
