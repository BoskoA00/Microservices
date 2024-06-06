using Microsoft.EntityFrameworkCore;
using UserServer.Data;
using UserServer.Interfaces;

namespace UserServer.Services
{
    public class userService : IUser
    {
        private readonly DatabaseContext databaseContext;

        public userService(DatabaseContext db)
        {
            databaseContext= db;
        }


        public  async Task<User?> AddUser(User user)
        {
            if (user != null)
            {

                await databaseContext.Users.AddAsync(user);
                return user;
            }
            else
                return null;

        }

        public async Task<User?> DeleteUser(int id)
        {

            User? u = await databaseContext.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (u != null)
            {
                databaseContext.Users.Remove(u);
                await databaseContext.SaveChangesAsync();
                return u;
            }
            else
                return null;
            

        }

        public async Task<User?> GetUserById(int id)
        {
            return await databaseContext.Users.Where(u=>u.Id== id).FirstOrDefaultAsync();
        }

        public async Task<List<User>?> GetUsers()
        {
            return await databaseContext.Users.ToListAsync();
        }

        public async Task<User?> UpdateUser(User user)
        {
             databaseContext.Users.Update(user);
            await databaseContext.SaveChangesAsync();
            return user;
        }
    }
}
